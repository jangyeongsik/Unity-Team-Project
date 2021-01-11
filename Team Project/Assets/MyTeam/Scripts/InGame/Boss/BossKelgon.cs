using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossKelgon : MonoBehaviour
{
    public Transform target;

    BossData kelgon;

    public GameObject ChargeCircle1;
    public GameObject ChargeCircle2;
    public GameObject ChargeCircle3;

    private void Start()
    {
        kelgon = GetComponent<BossData>();
        kelgon.navigation = GetComponent<NavMeshAgent>();
        kelgon.animator = transform.GetChild(0).GetComponent<Animator>();
        kelgon.position = transform;
    }

    private void Update()
    {
        kelgon.target = target;
        switch (kelgon.bossState)
        {
            case State.BossState.B_Idle:
                break;
            case State.BossState.B_Move:
                break;
            case State.BossState.B_Attack:
                break;
            case State.BossState.B_SkillChargeOne:
                break;
            case State.BossState.B_SkillChargeTwo:
                break;
            case State.BossState.B_SkillChargeThree:
                break;
            case State.BossState.B_SkillOne:
                break;
            case State.BossState.B_SkillTwo:
                break;
            case State.BossState.B_SkillThree:
                break;
            case State.BossState.B_Hit:
                break;
            case State.BossState.B_Dead:
                break;
            case State.BossState.B_AttackTwo:
                break;
        }
    }
}