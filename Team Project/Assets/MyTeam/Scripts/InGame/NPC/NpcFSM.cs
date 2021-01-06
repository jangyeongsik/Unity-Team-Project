using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcFSM : MonoBehaviour
{
    private Npc npc;
    public GameObject target;
    Vector3 originRotation;
    
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                transform.LookAt(target.transform);
            }
        }
    }
}
