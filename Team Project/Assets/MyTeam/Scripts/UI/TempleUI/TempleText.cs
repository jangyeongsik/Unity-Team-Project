using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempleText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;

    private void Awake()
    {
        text2.text = text + "";
    }

    private void Start()
    {
        text2.text = "에 공물을 바칠까요?";

        text.color = Color.red;
        text2.color = Color.green;

        text.fontSize = 30;
        text2.fontSize = 20;
    }
}
