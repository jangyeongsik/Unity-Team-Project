using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyMent : MonoBehaviour
{
    TextMeshProUGUI resourceText;
    private string resource;

    void Start()
    {
        resourceText = GetComponent<TextMeshProUGUI>();
        resource = "1개를 획득했습니다!";
        resourceText.text = resource.ToString();
    }
}
