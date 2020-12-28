
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using System;

public class EnemyMove : MonoBehaviour
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
    float attckCountMax = 0.5f;


    //================================

    //private bool hasTarget
    //{
    //    get
    //    {
    //        if (target != null)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //}
    

    #region 에너미 필요 변수 
    private float afterGroaringTime = 3.0f;
    private float afterGroaringDelay = 0.0f;
    private float attackDistance = 2.0f;
    private bool goBack = false;

    Vector3 startPos;
    #endregion

    private void monsterSetting()
    {
        enemyData = GetComponent<EnemyData>();
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
    }

    private void Update()
    {
        Debug.Log(attackTime);
        switch (enemyData.monsterState)
        {
            case State.MonsterState.M_Idle:
                break;
            case State.MonsterState.M_Move:
                UpdatePath();
                TryingtoATTACK();
                break;
            case State.MonsterState.M_Attack:
                ATTACK();
                attackCount();
                break;
            case State.MonsterState.M_Groar:
                Groar();
                break;
        }
    }

    private void ATTACK()
    {
        if (GetDistanceFromPlayer() > 4.0f)
        {
            enemyData.animator.SetBool("isReadyToPunch", false);
            enemyData.monsterState = State.MonsterState.M_Move;
        }
    }
    private void Groar()
    {
        afterGroaringDelay += Time.deltaTime;
        if (afterGroaringDelay > afterGroaringTime)
        {
            enemyData.monsterState = State.MonsterState.M_Move;
            enemyData.animator.SetTrigger("isRun");

        }
    }

    private void UpdatePath()
    {
        if (enemyData.navigation.destination != target.transform.position)
        {
            enemyData.navigation.SetDestination(target.transform.position);
        }
        else
        {
            enemyData.navigation.SetDestination(transform.position);
            targeting = false;
        }
    }

    public void TryingtoATTACK()
    {
        if (GetDistanceFromPlayer() < 3.0f)
        {
            enemyData.animator.SetBool("isReadyToPunch", true);
            enemyData.monsterState = State.MonsterState.M_Attack;
        }
    }

    private void Return()
    {
        enemyData.navigation.SetDestination(startPos);
        if (Vector3.Distance(startPos, transform.position) < 0.1f)
        {
            enemyData.animator.SetTrigger("RuntoIdle");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targeting = true;
            enemyData.animator.SetTrigger("isMeet");
            enemyData.monsterState = State.MonsterState.M_Groar;
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
}