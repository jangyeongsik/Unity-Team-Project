using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public LayerMask targetlayer;

    public  GameObject target;
    private NavMeshAgent navigation;

    private Monster enemy;
    private bool targeting;

    private bool hasTarget
    {
        get
        {
            if(target != null)
            {
                return true;
            }
            return false;
        }
    }

    private void  monsterSetting()
    {
        navigation = GetComponent<NavMeshAgent>();
        enemy = new Monster(gameObject.transform);
        enemy.hp = 10;
        enemy.damage = 10.0f;
        enemy.movespeed = 7.0f;
        enemy.m_state = State.MonsterState.M_Idle;
        navigation.speed = enemy.movespeed;

    }
    private void Awake()
    {
        monsterSetting();
        targeting = false;
    }

    private void Update()
    {
        
        switch (enemy.m_state)
        {
            case State.MonsterState.M_None:
                break;
            case State.MonsterState.M_Idle:
                break;
            case State.MonsterState.M_Dead:
                break;
            case State.MonsterState.M_Move:
                UpdatePath();
                break;
            default:
                break;
        }
    }

    private void UpdatePath()
    {

        if (targeting)
        {
            navigation.isStopped = false;
            navigation.SetDestination(target.transform.position);

        }
        else
        {
            navigation.isStopped = true;

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targeting = true;
            enemy.m_state = State.MonsterState.M_Move;
        }
    }
}
