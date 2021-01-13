using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntry : MonoBehaviour
{
    public BossKelgon kelgon;
    public BossTartos tartos;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //kelgon.target = other.gameObject;
            if(kelgon != null)
                kelgon.SetTarget(other.transform);
            if (tartos != null)
                tartos.SetTarget(other.transform);
        }
    }

}
