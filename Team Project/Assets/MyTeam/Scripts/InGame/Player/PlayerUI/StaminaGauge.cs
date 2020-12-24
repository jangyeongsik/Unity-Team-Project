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
    [SerializeField]
    Slider slider;

    float curStamina;
    float targetStamina;
    float staminaGap = 20;
    bool isRestore;
    STAMINAGAUGE state;

    private void Start()
    {
        curStamina = slider.value;
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

}
