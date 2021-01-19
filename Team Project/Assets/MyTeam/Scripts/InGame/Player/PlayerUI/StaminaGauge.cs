using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum STAMINAGAUGE
{
    RESTORE,DECREASE,NONE
}

public class StaminaGauge : MonoBehaviour
{
    private Slider slider;

    float curStamina;
    float targetStamina;
    bool isRestore;
    STAMINAGAUGE state;

    private void Awake()
    {
        slider = transform.GetChild(0).GetComponent<Slider>();
    }

    private void Start()
    {
        curStamina = GameData.Instance.player.stamina;
        targetStamina = curStamina;

        GameEventToUI.Instance.staminaRestore += setIsRestore;
        GameEventToUI.Instance.AddStamina += AddPlayerMaxStamina;

        //시작 스테미나 슬라이더 크기 설정
        Vector2 size = slider.GetComponent<RectTransform>().sizeDelta;
        size.x = GameData.Instance.player.stamina * 3;
        slider.GetComponent<RectTransform>().sizeDelta = size;
        slider.maxValue = GameData.Instance.player.stamina;

        GameData.Instance.player.isDashPossible = true;
    }

    private void Update()
    {
        //대쉬 가능상태 변경
        if(!GameData.Instance.player.isDashPossible && curStamina >= 15)
            GameData.Instance.player.isDashPossible = true;
        else if(GameData.Instance.player.isDashPossible && curStamina < 15)
            GameData.Instance.player.isDashPossible = false;

        switch (state)
        {
            case STAMINAGAUGE.RESTORE:
                curStamina += 10 * Time.deltaTime;
                if (curStamina >= slider.maxValue)
                {
                    curStamina = slider.maxValue;
                    state = STAMINAGAUGE.NONE;
                }
                targetStamina = curStamina;
                slider.value = curStamina;
                break;
            case STAMINAGAUGE.DECREASE:
                --curStamina;
                if (curStamina <= targetStamina)
                {
                    curStamina = targetStamina;
                    state = STAMINAGAUGE.NONE;
                }
                slider.value = curStamina;
                break;
            case STAMINAGAUGE.NONE:
                slider.value = curStamina;
                break;
        }

    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.staminaRestore -= setIsRestore;
        GameEventToUI.Instance.AddStamina -= AddPlayerMaxStamina;
    }

    public bool UseStamina(float percent)
    {
        if (targetStamina <= 0)
            return false;
        state = STAMINAGAUGE.DECREASE;
        targetStamina -= percent;
        return true;
    }

    void setIsRestore(STAMINAGAUGE _state, float percent)
    {
        if (_state == STAMINAGAUGE.RESTORE)
        {
            StopCoroutine(IsRestoreTrue(_state));
            StartCoroutine(IsRestoreTrue(_state));
        }
        else if(_state == STAMINAGAUGE.DECREASE)
        {
            UseStamina(percent);
        }
    }

    IEnumerator IsRestoreTrue(STAMINAGAUGE _state)
    {
        yield return new WaitForSeconds(1.5f);
        state = _state;
    }

    void AddPlayerMaxStamina(int add)
    {
        if (GameData.Instance.player.stamina >= 90) return;
        GameData.Instance.player.stamina += add;
        Vector2 size = slider.GetComponent<RectTransform>().sizeDelta;
        size.x += add * 3;
        slider.GetComponent<RectTransform>().sizeDelta = size;
        slider.maxValue += add;

        curStamina += add;
        targetStamina = curStamina;
    }
}
