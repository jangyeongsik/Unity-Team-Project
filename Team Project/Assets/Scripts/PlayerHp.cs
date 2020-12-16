using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    private Slider slider;             //슬라이더
    public int maxHp;           //최대 체력
    public int currentHp;       //현재 체력

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = (float)currentHp / (float)maxHp;
    }
}
