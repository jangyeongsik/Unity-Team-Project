using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattom_2_Collider : MonoBehaviour
{
    bool isTrigger;

    public void PlayerHit()
    {
        if (isTrigger)
        {
            PlayerHit();
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("이 싯팔");
            isTrigger = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isTrigger = false;
        }
    }
}
