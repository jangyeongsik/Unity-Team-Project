using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcFSM : MonoBehaviour
{
    private Npc npc;
    Vector3 originPos;

    void Start()
    {
        npc = GetComponent<Npc>();
        NpcSetting();
    }

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

    }

    
}
