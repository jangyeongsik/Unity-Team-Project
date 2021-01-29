using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatFSM : MonoBehaviour
{
    private Monster bat;
    private GameObject target;
    public GameObject AttackNotice;

    bool running = false;
    private bool attacking;
    private bool dead = false;
    private bool targeting = false;

    private int count;

    float attackTime;
    float attckCountMin = 0.2f;
    float attckCountMax = 0.6f;

    private void Start()
    {
        bat = GetComponent<Monster>();
        bat.position = transform;
        bat.monsterKind = State.MonsterKind.M_Bat;
        bat.EnemyHitEvent += AttackHit;
        BatsSetting();
        target = GameData.Instance.player.position.gameObject;
    }

    private void EnemyBatsSet(int attack)
    {
        bat.monsterState = State.MonsterState.M_Idle;
        bat.navigation = GetComponent<NavMeshAgent>();
        bat.animator = GetComponent<Animator>();
        bat.rigid = GetComponent<Rigidbody>();
        bat.movespeed = 8.0f;
        bat.attack_aware_distance = 4.0f;
        bat.damage = attack;
    }

    private void BatsSetting()
    {
        if (gameObject.CompareTag("EnemyBat"))
        {
            EnemyBatsSet(1);
        }
    }

    private void OnDestroy()
    {
        if(bat != null)
            bat.EnemyHitEvent -= AttackHit;
    }

    private void Update()
    {
        if (!dead)
        {
            if (count >= 4)
            {
                bat.navigation.enabled = false;

                dead = true;
                running = false;
                bat.counterjudgement = false;
                bat.animator.SetBool("isWalk", false);
                bat.animator.SetBool("isAttack", false);
                bat.animator.SetBool("isDead", true);
                bat.monsterState = State.MonsterState.M_Dead;

                GameEventToUI.Instance.OnAttactReset();
                GameEventToUI.Instance.OnPlayerCylinderGauge(50);
            }
            switch (bat.monsterState)
            {
                case State.MonsterState.M_Idle:
                    Idle();
                    break;
                case State.MonsterState.M_Move:
                    Move();
                    break;
                case State.MonsterState.M_Dead:
                    break;
                case State.MonsterState.M_Groar:
                    break;
                case State.MonsterState.M_Attack:
                    Attack();
                    attackCount();
                    break;
                case State.MonsterState.M_Return:
                    break;
                case State.MonsterState.M_Damage:
                    GetDamage();
                    break;
            }
        }
        if (bat.monsterState != State.MonsterState.M_Attack)
        {
            AttackNotice.SetActive(false);
            attackTime = 0;
            bat.counterjudgement = false;
        }
    }

    private void GetDamage()
    {
        
    }

    private void Attack()
    {
        if (bat.DistacneWithTarget() > bat.navigation.stoppingDistance)
        {
            bat.monsterState = State.MonsterState.M_Move;
            bat.animator.SetBool("isAttack", false);
        }
    }

    void attackCount()
    {
        attackTime += Time.deltaTime;

        if (attackTime > attckCountMin && attackTime < attckCountMax)
        {
            bat.counterjudgement = true;
        }
        else
        {
            bat.counterjudgement = false;
        }
        AttackNotice.SetActive(bat.counterjudgement);
    }

    private void Idle()
    {
        if (bat.DistacneWithTarget() < bat.attack_aware_distance)
        {
            bat.monsterState = State.MonsterState.M_Move;
            bat.animator.SetBool("isWalk", true);
        }
        else
        {
            bat.animator.SetBool("isWalk", false);
        }
        if (targeting)
        {
            bat.monsterState = State.MonsterState.M_Move;
            bat.animator.SetBool("isWalk", true);
        }
    }

    public void OnTargetingEvent()
    {
        targeting = true;
    }

    private void Move()
    {
        if (!running)
        {
            StartCoroutine(navigationSet());
        }

        if (bat.DistacneWithTarget() < bat.navigation.stoppingDistance)
        {
            bat.monsterState = State.MonsterState.M_Attack;
            bat.animator.SetBool("isAttack", true);
        }
    }

    IEnumerator navigationSet()
    {
        running = true;
        yield return new WaitForSecondsRealtime(0.25f);
        if (!dead)
            bat.navigation.SetDestination(target.transform.position);
        running = false;
    }

    public void AttackHit(int damage)
    {
        if (!dead)
        {
            count+= damage;

            bat.animator.SetBool("isWalk", false);
            bat.animator.SetBool("isAttack", false);
            bat.animator.SetTrigger("isHit");
            bat.monsterState = State.MonsterState.M_Damage;
            if (GameEventToUI.Instance.Attack_SuccessEvent())
            {
                GameEventToUI.Instance.OnAttactReset();
            }
        }
    }

    public void AttackSetting()
    {
        attackTime = 0;
    }
}
