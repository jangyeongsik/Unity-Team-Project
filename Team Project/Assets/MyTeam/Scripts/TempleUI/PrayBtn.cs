using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrayBtn : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        text.text = "기도 횟수를 선택하십시오.";

        text.color = Color.red;

        text.fontSize = 30;
    }

    public void Prees2Pray()
    {
        text.text = "2회";
    }

    public void Prees5Pray()
    {
        text.text = "5회";
    }

    public void Prees10Pray()
    {
        text.text = "10회";
    }
}
