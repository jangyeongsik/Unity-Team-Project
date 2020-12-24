using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyData : Monster
{
   
    private void Awake()
    {
        monsterState = State.MonsterState.M_Idle;
        navigation = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
}