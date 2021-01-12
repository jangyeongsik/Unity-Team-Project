using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntry : MonoBehaviour
{
    public BossKelgon kelgon;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //kelgon.target = other.gameObject;
            kelgon.SetTarget(other.transform);
        }
    }

}
