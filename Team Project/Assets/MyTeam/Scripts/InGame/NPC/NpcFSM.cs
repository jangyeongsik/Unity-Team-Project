using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcFSM : MonoBehaviour
{
    private Npc npc;
    Vector3 originPos;

    private bool trigger =false;

    void Start()
    {
        npc = GetComponent<Npc>();
       // NpcSetting();
        GameEventToUI.Instance.player_Trigger += isTrigger;
    }
/*
    private void NpcSetting()
    {
        npc.npcState = State.NpcState.N_Idle;
        npc.animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (npc.npcState)
        {
            case State.NpcState.N_Idle:
                Idle();
                break;
        }
    }

    private void Idle()
    {

    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.transform.LookAt(other.gameObject.transform);
            GameEventToUI.Instance.OnEventTalkBtn(true);
            trigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.transform.LookAt(other.gameObject.transform);
            GameEventToUI.Instance.OnEventTalkBtn(false);
            trigger = false;
        }
    }

    public bool isTrigger()
    {
        return trigger;
    }
}
