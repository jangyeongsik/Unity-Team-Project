using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkullKingFSM : MonoBehaviour
{
    private Monster skullKing;
    private bool targeting = false;

    private GameObject target;
    Vector3 offset;

    public GameObject AttackNotice; //그 에너미용 느낌표 

    float attackTime;
    float attackCountMin = 0.2f;
    float attackCountMax = 0.6f;

    private int count;

    bool isAppear = false;          //해골왕 처음에 등장하는지.
    bool isRunning = false;         //해골왕 움직이는지.

    private bool dead = false;

    private void Start()
    {
        skullKing = GetComponent<Monster>();

        target = GameData.Instance.player.position.gameObject;
        
        skullKing.EnemyHitEvent += AttackHit;

        KingSetting();
    }

    private void KingSetting()
    {
        skullKing.position = transform;
        skullKing.monsterKind = State.MonsterKind.M_SkullKing;
        skullKing.monsterState = State.MonsterState.M_None;

        skullKing.navigation = GetComponent<NavMeshAgent>();
        skullKing.navigation.enabled = false;
        skullKing.animator = GetComponent<Animator>();
        skullKing.rigid = GetComponent<Rigidbody>();
        skullKing.movespeed = 11.0f;
        skullKing.attack_aware_distance = 1.9f;
        skullKing.navigation.enabled = true;
    }

    private void OnDestroy()
    {
        if(skullKing != null)
            skullKing.EnemyHitEvent -= AttackHit;

    }

    private void Update()
    {
        if (!targeting)
        {
            if(skullKing.DistacneWithTarget() < 10.0f)
            {
                targeting = true;
            }
        }
        if(targeting)
        {
            if (!dead)
            {
                if (count >= 10)
                {
                    skullKing.navigation.enabled = false;
                    dead = true;
                    isRunning = false;
                    skullKing.counterjudgement = false;
                    skullKing.animator.SetBool("isWalk", false);
                    skullKing.animator.SetBool("isAttack", false);
                    skullKing.animator.SetBool("isDead",true);
                    skullKing.monsterState = State.MonsterState.M_Dead;
                    GameEventToUI.Instance.OnAttactReset();
                    GameEventToUI.Instance.OnPlayerCylinderGauge(20);
                }
                switch (skullKing.monsterState)
                {
                    case State.MonsterState.M_None:

                        break;
                    case State.MonsterState.M_Idle:
                        AfterAppear();
                        break;
                    case State.MonsterState.M_Move:
                        KingMove();
                        break;
                    case State.MonsterState.M_Dead:
                        break;
                    case State.MonsterState.M_Groar:
                        break;
                    case State.MonsterState.M_Attack:
                        KingAttack();
                        //AttackCount();
                        break;
                    case State.MonsterState.M_Return:
                        break;
                    case State.MonsterState.M_Damage:
                        GotDamage();
                        break;
                    case State.MonsterState.M_Dash:
                        break;
                    default:
                        break;
                }
            }
            if (skullKing.monsterState != State.MonsterState.M_Attack)
            {
                AttackNotice.SetActive(false);
                attackTime = 0;
                skullKing.counterjudgement = false;
            }
        }
    }

    private void GotDamage()
    {
        
    }

    IEnumerator navigationSet()
    {
        isRunning = true;
        yield return new WaitForSecondsRealtime(0.25f);
        if(!dead)
        {
            skullKing.navigation.SetDestination(target.transform.position);

        }
        isRunning = false;
    }

    //해골왕 등장 후 메서드.
    private void AfterAppear()
    {
        if(skullKing.DistacneWithTarget() > skullKing.attack_aware_distance)
        {
            skullKing.monsterState = State.MonsterState.M_Move;
            skullKing.animator.SetBool("isWalk", true);
        }
        else
        {
            skullKing.monsterState = State.MonsterState.M_Attack;
            skullKing.animator.SetBool("isWalk", true);
            skullKing.animator.SetBool("isAttack", true);
        }
        skullKing.monsterState = State.MonsterState.M_Move;
        skullKing.animator.SetBool("isWalk", true);
    }

    //해골왕 등장 후 -> 움직이는 메서드 
    private void KingMove()
    {
        if (!isRunning)
        {
            StartCoroutine(navigationSet());
        }
        if (skullKing.DistacneWithTarget() < skullKing.attack_aware_distance)
        {
            skullKing.monsterState = State.MonsterState.M_Attack;
            skullKing.animator.SetBool("isAttack", true);
        }
    }

    //해골왕 움직이고, 타겟 거리 도달 하면 공격하는 메서드 
    private void KingAttack()
    {
        AttackNotice.SetActive(skullKing.counterjudgement);
        if (skullKing.DistacneWithTarget() > skullKing.attack_aware_distance)
        {
            skullKing.monsterState = State.MonsterState.M_Move;
            skullKing.animator.SetBool("isAttack", false);
        }
    }


    //거의 대부분의 에너미에 공통적으로 들어가는 메서드. 
    public void OnTargetingEvent()
    {
        targeting = true;
    }

    public void AttackSetting()
    {
        attackTime = 0;
    }

    public void AttackHit(int damage)
    {
        count+= damage;
        skullKing.animator.SetBool("isWalk", false);
        skullKing.animator.SetBool("isAttack", false);
        skullKing.animator.SetTrigger("isHit");
        skullKing.monsterState = State.MonsterState.M_Damage;
        if(GameEventToUI.Instance.OnAttack_SuccessEvent())
        {
            GameEventToUI.Instance.OnAttactReset();
        }
    }

    public void StateChange()
    {
        skullKing.monsterState = State.MonsterState.M_Idle;
    }

}
