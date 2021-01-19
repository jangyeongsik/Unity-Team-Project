using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattom_2_Collider : MonoBehaviour
{
    BossData bossData;
    bool isTrigger;

    private void Start()
    {
        bossData = transform.parent.parent.parent.GetComponent<BossData>();
    }

    public void PlayerHit()
    {
        if (isTrigger)
        {
            GameEventToUI.Instance.OnPlayerBossHit(bossData.position, 3, bossData.bossState);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
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
