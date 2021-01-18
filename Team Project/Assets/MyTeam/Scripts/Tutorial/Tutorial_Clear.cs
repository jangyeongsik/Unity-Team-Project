using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Clear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameData.Instance.player.tutorial = true;
        }
    }
}
