using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    Image LessHp;
    Image Hit;

    private void Awake()
    {
        LessHp = transform.GetChild(0).GetComponent<Image>();
        Hit = transform.GetChild(1).GetComponent<Image>();
    }

    private void Start()
    {
        
    }

    void LessHPActive()
    {

    }
}
