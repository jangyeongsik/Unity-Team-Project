using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenCheck : MonoBehaviour
{
    public GameObject box;
    public int talk_id;
    private bool isTalk;
    bool check;

    private void OnTriggerEnter(Collider other)
    {
        if(!check && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameEventToUI.Instance.OnEvent_TalkBox(talk_id);
            isTalk = true;
        }
    }



}
