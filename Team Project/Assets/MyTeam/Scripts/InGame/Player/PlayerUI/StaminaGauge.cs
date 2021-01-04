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
    }

    private void Update()
    {
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
                slider.value = curStamina/GameData.Instance.player.stamina;
                break;
            case STAMINAGAUGE.DECREASE:
                --curStamina;
                if (curStamina <= targetStamina)
                {
                    curStamina = targetStamina;
                    state = STAMINAGAUGE.NONE;
                }
                slider.value = curStamina / GameData.Instance.player.stamina;
                break;
            case STAMINAGAUGE.NONE:
                slider.value = curStamina / GameData.Instance.player.stamina;
                break;
        }

        if (Input.GetMouseButtonDown(0))
            AddPlayerMaxStamina(10);

    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.staminaRestore -= setIsRestore;
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
        yield return new WaitForSeconds(1);
        state = _state;
    }

    void AddPlayerMaxStamina(int add)
    {
        GameData.Instance.player.stamina += add;
        Vector2 size = slider.GetComponent<RectTransform>().sizeDelta;
        size.x += add * 3;
        slider.GetComponent<RectTransform>().sizeDelta = size;
    }
}
