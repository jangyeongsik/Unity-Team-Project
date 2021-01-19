using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Portal : MonoBehaviour
{
    public GameObject p_1;
    public GameObject p_2;

    public int talk_id;
    bool isTalk;
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

    private void OnTriggerEnter(Collider other)
    {
        if (!isTalk&& other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameEventToUI.Instance.OnEvent_TalkBox(talk_id);
            isTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            isTalk = false;
        }
    }

}
