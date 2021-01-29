using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyWarrior : MonoBehaviour
{
    private Monster e_Warrior;                  //몬스터 클래스
    private bool targeting = false;             //타겟 조준

    private GameObject target;
    Vector3 offset;


    public GameObject AttackNocice;

    float attackTime;

    float attckCountMin = 0.2f;
    float attckCountMax = 0.6f;

    private int count;
    bool running = false;

    private bool dead;
    private void Start()
    {
        e_Warrior = GetComponent<Monster>();

        setting();
        target = GameData.Instance.player.position.gameObject;
        e_Warrior.EnemyHitEvent += AttackHit;
       
    }

    private void setting()
    {
        e_Warrior.position = transform;
        e_Warrior.monsterKind = State.MonsterKind.M_Warrier;
        e_Warrior.monsterState = State.MonsterState.M_Idle;

        e_Warrior.navigation = GetComponent<NavMeshAgent>();
        e_Warrior.navigation.enabled = false;
        e_Warrior.animator = GetComponent<Animator>();
        e_Warrior.rigid = GetComponent<Rigidbody>();
        e_Warrior.movespeed = 8.0f;
        e_Warrior.attack_aware_distance = 2.0f;
        e_Warrior.navigation.enabled = true;
        e_Warrior.damage = 1;

    }

    private void OnDestroy()
    {
        if(e_Warrior != null)
            e_Warrior.EnemyHitEvent -= AttackHit;
    }

    private void Update()
    {
        if (!targeting)
        {
            if (e_Warrior.DistacneWithTarget() < 10)
            {
                targeting = true;
            }
        }
        if (targeting)
        {
            if (!dead)
            {
                if (count >= 3)
                {
                    e_Warrior.navigation.enabled = false;

                    dead = true;
                    running = false;
                    e_Warrior.counterjudgement = false;
                    e_Warrior.animator.SetBool("IsRun", false);
                    e_Warrior.animator.SetBool("IsAttack", false);
                    e_Warrior.animator.SetTrigger("isDead");
                    e_Warrior.monsterState = State.MonsterState.M_Dead;
                    GameEventToUI.Instance.OnAttactReset();
                    GameEventToUI.Instance.OnPlayerCylinderGauge(20);
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
                        M_Attack();
                        break;
                    case State.MonsterState.M_Return:
                        break;
                    case State.MonsterState.M_Damage:
                        M_Damage();
                        break;
                }
            }
            if (e_Warrior.monsterState != State.MonsterState.M_Attack)
            {
                AttackNocice.SetActive(false);
                attackTime = 0;
                e_Warrior.counterjudgement = false;
            }
        }
    }

    private void M_Damage()
    {

    }

    private void M_Attack()
    {
        AttackNocice.SetActive(e_Warrior.counterjudgement);
        if (e_Warrior.DistacneWithTarget() > e_Warrior.attack_aware_distance)
        {
            e_Warrior.monsterState = State.MonsterState.M_Move;
            e_Warrior.animator.SetBool("IsAttack", false);
        }
    }

    private void M_Move()
    {
        if (!running)
        {
            StartCoroutine(navigationSet());
        }

        if (e_Warrior.DistacneWithTarget() < e_Warrior.attack_aware_distance)
        {
            e_Warrior.monsterState = State.MonsterState.M_Attack;
            e_Warrior.animator.SetBool("IsAttack", true);
        } 
    }

    IEnumerator navigationSet()
    {
        running = true;
        yield return new WaitForSecondsRealtime(0.25f);
        if(!dead)
            e_Warrior.navigation.SetDestination(target.transform.position);

        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Bone, "Warrior Move");
        running = false;
    }

    private void M_Idle()
    {
        if (targeting)
        {
            if (e_Warrior.DistacneWithTarget() > e_Warrior.attack_aware_distance)
            {
                e_Warrior.monsterState = State.MonsterState.M_Move;
                e_Warrior.animator.SetBool("IsRun", true);
            }
            else
            {
                e_Warrior.monsterState = State.MonsterState.M_Attack;
                e_Warrior.animator.SetBool("IsRun", true);
                e_Warrior.animator.SetBool("IsAttack", true);
            }
        }
        if (targeting)
        {
            e_Warrior.monsterState = State.MonsterState.M_Move;
            e_Warrior.animator.SetBool("IsRun", true);
        }
    }

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
        count += damage;
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Bone, "Hit");
        e_Warrior.animator.SetBool("IsRun", false);
        e_Warrior.animator.SetBool("IsAttack", false);
        e_Warrior.animator.SetTrigger("Hit");
        e_Warrior.monsterState = State.MonsterState.M_Damage;
        if (GameEventToUI.Instance.Attack_SuccessEvent())
        {
            GameEventToUI.Instance.OnAttactReset();
        }
    }

    private void AttackSound()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Bone, "Sword Attack");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) { 
        }
    }
}
