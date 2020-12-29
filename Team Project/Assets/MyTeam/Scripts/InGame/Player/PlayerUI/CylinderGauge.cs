using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CylinderGauge : MonoBehaviour
{
    float maxGauge = 100;
    float curGauge;
    float targetGauge;
    bool isAdd;

    [SerializeField]
    Slider slider;
    [SerializeField]
    Text countTxt;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            AddCylinderGauge(15);

        if(isAdd)
        {
            curGauge += 1;
            if(curGauge >= maxGauge)
            {
                curGauge -= maxGauge;
                targetGauge -= maxGauge;
            }
            if (curGauge >= targetGauge)
            {
                curGauge = targetGauge;
                isAdd = false;
            }
            slider.value = curGauge;
        }
    }

    public void AddCylinderGauge(float percent)
    {
        isAdd = true;
        targetGauge += percent;
    }

}
