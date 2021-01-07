using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CylinderGauge : MonoBehaviour
{
    int maxGauge = 100;
    int curGauge;
    int targetGauge;
    bool isAdd;

    [SerializeField]
    Slider slider;
    [SerializeField]
    Text countTxt;

    private void Start()
    {
        GameEventToUI.Instance.PlayerCylinderGauge += AddCylinderGauge;
        countTxt.text = GameData.Instance.player.cylinderCounter.ToString();
        curGauge = GameData.Instance.player.cylinderPercent;
        targetGauge = curGauge;
        slider.value = curGauge;
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.PlayerCylinderGauge -= AddCylinderGauge;
    }

    private void Update()
    {
        if(isAdd)
        {
            curGauge += 1;
            if(curGauge >= maxGauge)
            {
                curGauge -= maxGauge;
                targetGauge -= maxGauge;
                ++GameData.Instance.player.cylinderCounter;
            }
            if (curGauge >= targetGauge)
            {
                curGauge = targetGauge;
                isAdd = false;
            }
        }
        slider.value = curGauge;
        GameData.Instance.player.cylinderPercent = curGauge;
        countTxt.text = GameData.Instance.player.cylinderCounter.ToString();
    }

    public void AddCylinderGauge(int percent)
    {
        isAdd = true;
        targetGauge += percent;
    }

}
