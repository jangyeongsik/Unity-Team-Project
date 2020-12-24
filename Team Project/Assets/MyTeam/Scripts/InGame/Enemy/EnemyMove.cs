using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    CharacterController controller;

    public GameObject target;
    public LayerMask targetlayer;
    private NavMeshAgent navigation;

    public GameObject attackRange;

    private Monster enemy;

    private bool targeting;

    ColliderForAttack checkAttack;
    public Animator animator;
    
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

    #region 에너미 필요 변수 
    private float afterGroaringTime = 3.0f;
    private float afterGroaringDelay = 0.0f;
    #endregion

    private void monsterSetting()
    {
        navigation = GetComponent<NavMeshAgent>();
        enemy = new Monster(gameObject.transform);
        enemy.hp = 10;
        enemy.damage = 7.0f;
        enemy.movespeed = 10.0f;
        enemy.m_state = State.MonsterState.M_Idle;
        navigation.speed = enemy.movespeed;
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        monsterSetting();
        targeting = false;
    }

    private void Update()
    {
        switch (enemy.m_state)
        {
            case State.MonsterState.M_Idle:
                break;
            case State.MonsterState.M_Move:
                UpdatePath();
                break;
            case State.MonsterState.M_Attack:
                ATTACK();
                break;
            case State.MonsterState.M_Groar:
                Groar();
                break;
        }
    }

    private void ATTACK()
    {

    }

    private void Groar()
    {
        enemy.m_state = State.MonsterState.M_Move;
    }

    private void UpdatePath()
    {
        afterGroaringDelay += Time.deltaTime;
        if (afterGroaringDelay > afterGroaringTime)
        {
            if (navigation.destination != target.transform.position)
            {
                navigation.SetDestination(target.transform.position);
            }
            else
            {
                navigation.SetDestination(transform.position);
                targeting = false;
            }
            //afterGroaringDelay = 0.0f;
        }
    
    }


    public void TryingtoATTACK()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targeting = true;
            animator.SetBool("isMeet", true);
            enemy.m_state = State.MonsterState.M_Groar;
        }
    }
}
