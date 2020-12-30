using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using System;

public class EnemyFSM : MonoBehaviour
{
    EnemyData enemyData;
    public GameObject target;
    public LayerMask targetlayer;

    public GameObject attackRange;

    private bool targeting;

    ColliderForAttack checkAttack;


    //에너미 어택 이벤트 관련
    //================================
    public GameObject AttackNocice;


    public bool counterjudgement;

    float attackTime;

    float attckCountMin = 0.3f;
    float attckCountMax = 0.7f;

    #region 에너미 필요 변수 
    //private float afterGroaringTime = 3.0f;
    //private float afterGroaringDelay = 0.0f;
    //private float attackDistance = 4.0f;
    private float findRange = 6.0f;
    private float canAttackRange = 3.0f;
    //private bool goBack = false;
    float timer = 2.0f;
    float atkTime = 0.0f;

    Vector3 startPos;
    #endregion

    private void monsterSetting()
    {
        enemyData = GetComponent<EnemyData>();
        enemyData.navigation.enabled = false;
        float monsterSpeed = 10.0f;
        enemyData.navigation.speed = monsterSpeed;
    }

    private void Awake()
    {
        startPos = transform.position;
    }
    private void Start()
    {
        monsterSetting();
        targeting = false;
        EnemyEvent.Instance.EnemyResetTime += AttackSetting;
        stateEventManager.Instance.Player_Attack += Player_AttackEvent;
    }

    private void Update()
    {
        if (stateEventManager.Instance.OnAttack_SuccessEvent())
        {
        }
        switch (enemyData.monsterState)
        {
            case State.MonsterState.M_Idle:
                Idle();
                break;
            case State.MonsterState.M_Move:
                UpdatePath();
                break;
            case State.MonsterState.M_Attack:
                ATTACK();
                attackCount();
                break;
            case State.MonsterState.M_Groar:
                Groar();
                break;
            case State.MonsterState.M_Return:
                Return();
                break;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < findRange)
        {
            enemyData.monsterState = State.MonsterState.M_Move;
            enemyData.animator.SetTrigger("Run");
        }
    }

    private void Idle()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < findRange)
        {

        }
    }

    private void ATTACK()
    {
        enemyData.navigation.enabled = false;
        if(Vector3.Distance(transform.position, target.transform.position) < canAttackRange)
        {
            timer += Time.deltaTime;
            if(timer > atkTime)
            {
                timer = 0.0f;
                enemyData.animator.SetTrigger("Attack");
            }
        }
        else
        {
            enemyData.monsterState = State.MonsterState.M_Move;
            enemyData.animator.SetTrigger("Run");
            timer = 0.0f;
        }
    }
    private void Groar()
    {
        enemyData.monsterState = State.MonsterState.M_Groar;
        enemyData.animator.SetTrigger("Groar");
    }

    private void UpdatePath()
    {
        if(!enemyData.navigation.enabled) enemyData.navigation.enabled = true;

        float followRange = Vector3.Distance(transform.position, target.transform.position);

        if(followRange > 15.0f)
        {
            enemyData.monsterState = State.MonsterState.M_Return;
            enemyData.animator.SetTrigger("Return");
        }
        else if(Vector3.Distance(transform.position, target.transform.position) > canAttackRange)
        {
            enemyData.navigation.SetDestination(target.transform.position);
        }
        else
        {
            enemyData.monsterState = State.MonsterState.M_Attack;
            enemyData.animator.SetTrigger("Attack");
        }
    }

    private void Return()
    {
        if(Vector3.Distance(transform.position, startPos) > 0.1f)
        {
            enemyData.navigation.SetDestination(startPos);
        }
        else
        {
            transform.position = startPos;
            transform.rotation = Quaternion.identity;
            enemyData.monsterState = State.MonsterState.M_Idle;
            enemyData.animator.SetTrigger("Idle");
            enemyData.navigation.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyData.monsterState = State.MonsterState.M_Groar;
            enemyData.animator.SetTrigger("Groar");
            //Groar();
        }
    }

    float GetDistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance;
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
        AttackNocice.SetActive(counterjudgement);
    }

    private bool Player_AttackEvent()
    {
        return counterjudgement;
    }

    public void AttackSetting()
    {
        attackTime = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRange);
    }
}