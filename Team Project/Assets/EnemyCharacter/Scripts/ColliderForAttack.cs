using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForAttack : MonoBehaviour
{
    bool isNoticed = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {
            isNoticed = true;
        }
    } 
}
