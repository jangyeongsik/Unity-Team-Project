using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public LayerMask targetlayer;
    public  GameObject target;
    private NavMeshAgent navigation;

    private Monster enemy;
    private bool targeting;


    //에너미 어택 이벤트 관련
    //================================

    public bool counterjudgement;

    float attackTime;

    float attckCountMin = 0.7f;
    float attckCountMax = 1.5f;

    //================================

   
    private void Awake()
    {
        stateEventManager.Instance.Player_Attack = Player_AttackEvent;
        monsterSetting();
        targeting = false;

    }


    private void FixedUpdate()
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.m_state = State.MonsterState.M_Move;
        }

    }
    private void UpdatePath()
    {

        if (navigation.destination != target.transform.position)
        {
            navigation.SetDestination(target.transform.position);
        }
        else
        {
            navigation.SetDestination(transform.position);
        }
    }


    private bool hasTarget
    {
        get
        {
            if (target != null)
            {
                return true;
            }

            return false;
        }
    }

    private void monsterSetting()
    {
        navigation = GetComponent<NavMeshAgent>();
        enemy = new Monster(gameObject.transform);
        enemy.hp = 10;
        enemy.damage = 10.0f;
        enemy.movespeed = 7.0f;
        enemy.m_state = State.MonsterState.M_Idle;
        navigation.speed = enemy.movespeed;

    }


    

    void attackCount()
    {
        attackTime += Time.deltaTime;

        if (attackTime > attckCountMin && attackTime < attckCountMax)
        {
            counterjudgement = true;
        }
        else
        {
            counterjudgement = false;
        }
    }


    private bool Player_AttackEvent()
    {
        return counterjudgement;
    }


}
