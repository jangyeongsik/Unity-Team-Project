using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlaTipOpen : MonoBehaviour
{
    bool isTalk;
    public GameObject tip;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTalk && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            tip.SetActive(true);
             isTalk = true;
        }
    }

    public void Tip_off()
    {
        tip.SetActive(false);
    }
}
