using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Portal : MonoBehaviour
{
    public GameObject p_1;
    public GameObject p_2;

    BoxCollider collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (p_1.activeSelf && p_2.activeSelf)
        {
            collider.isTrigger = true;
        }
    }  

}
