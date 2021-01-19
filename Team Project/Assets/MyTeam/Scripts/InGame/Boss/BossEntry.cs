using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntry : MonoBehaviour
{
    public BossKelgon kelgon;
    public BossTartos tartos;
    public GameObject collider;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //kelgon.target = other.gameObject;
            if (kelgon != null)
            {
                kelgon.SetTarget(other.transform);
                collider.SetActive(true);
            }
            if (tartos != null)
            {
                tartos.SetTarget(other.transform);
                collider.SetActive(true);
            }
        }
    }

    private void Update()
    { 
        
        if(GameObject.FindObjectOfType<Monster>() == null)
        {
            collider.SetActive(false);
        }
        
    }

}
