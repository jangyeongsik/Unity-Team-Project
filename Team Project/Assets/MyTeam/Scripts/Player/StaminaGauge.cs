using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaGauge : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    float curStamina;
    float targetStamina;
    float staminaGap = 20;
    bool isAdd;

    private void Start()
    {
        curStamina = slider.value;
        targetStamina = curStamina;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            UseStamina(20);
        if(isAdd)
        {
            --curStamina;
            if(curStamina <= targetStamina)
            {
                curStamina = targetStamina;
                isAdd = false;
            }
            slider.value = curStamina;
        }
    }

    public bool UseStamina(float percent)
    {
        if(targetStamina <= 0)
            return false;
        isAdd = true;
        targetStamina -= percent;
        return true;
    }


}
