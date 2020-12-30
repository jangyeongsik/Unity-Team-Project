using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyWarrior : MonoBehaviour
{
    private Monster e_Warrior;          //몬스터 클래스
    private bool targeting;             //타겟 조준

    public Transform target;


    private void Start()
    {
       
        e_Warrior = new Monster();
        setting();
    }

    private void setting()
    {
        e_Warrior.monsterState = State.MonsterState.M_Idle;
        e_Warrior.navigation = GetComponent<NavMeshAgent>();
        e_Warrior.animator = GetComponent<Animator>();
        e_Warrior.movespeed = 8.0f;
        e_Warrior.attack_aware_distance = 4.0f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            e_Warrior.monsterState = State.MonsterState.M_Move;
        }
        switch (e_Warrior.monsterState)
        {
            case State.MonsterState.M_Idle:
                M_Idle();
                break;
            case State.MonsterState.M_Move:
                M_Move();
                break;
            case State.MonsterState.M_Dead:
                break;
            case State.MonsterState.M_Groar:
                break;
            case State.MonsterState.M_Attack:
                break;
            case State.MonsterState.M_Return:
                break;
            case State.MonsterState.M_Damage:
                break;
        }
    }

    private void M_Move()
    {
        e_Warrior.animator.SetTrigger("Run");
        
    }

    private void M_Idle()
    {
        e_Warrior.animator.SetTrigger("Idle");
    }

    public void OnTargetingEvent()
    {

    }
}
