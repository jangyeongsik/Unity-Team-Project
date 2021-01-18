using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrayBtn : MonoBehaviour
{
    public Button prayTwice;
    public Button prayFiveTimes;
    public Button prayTenTimes;
    public TMP_Text keyCount;
    public void SetPrayCount(int count)
    {
        keyCount.text = count.ToString();
    }
}
