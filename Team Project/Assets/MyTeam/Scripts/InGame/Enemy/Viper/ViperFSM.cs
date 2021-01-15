using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ViperFSM : MonoBehaviour
{
    private Monster viper;
    private GameObject target;
    public GameObject AttackNocice;

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
        viper = GetComponent<Monster>();
        viper.position = transform;
        viper.monsterKind = State.MonsterKind.M_Viper;
        viper.EnemyHitEvent += AttackHit;
        VipersSetting();
        target = GameData.Instance.player.position.gameObject;
    }

    private void EnemyVipersSet(int attack)
    {
        viper.monsterState = State.MonsterState.M_Idle;
        viper.navigation = GetComponent<NavMeshAgent>();
        viper.animator = GetComponent<Animator>();
        viper.rigid = GetComponent<Rigidbody>();
        viper.movespeed = 8.0f;
        viper.attack_aware_distance = 10.0f;
        viper.damage = attack;
    }

    private void VipersSetting()
    {
        if (gameObject.name == "EnemyViper")
        {
            EnemyVipersSet(1);
        }
        else
        {
            EnemyVipersSet(2);
        }
    }

    private void OnDestroy()
    {
        viper.EnemyHitEvent -= AttackHit;
    }

    private void Update()
    {
        if(!dead)
        {
            if (count >= 7)
            {
                viper.navigation.enabled = false;

                dead = true;
                running = false;
                viper.counterjudgement = false;
                viper.animator.SetBool("viperWalk", false);
                viper.animator.SetBool("viperAttack", false);
                viper.animator.SetBool("viperDead", true);
                viper.monsterState = State.MonsterState.M_Dead;
            
                GameEventToUI.Instance.OnAttactReset();
                GameEventToUI.Instance.OnPlayerCylinderGauge(50);
            }
            switch (viper.monsterState)
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
                    break;
                case State.MonsterState.M_Return:
                    break;
                case State.MonsterState.M_Damage:
                    break;
            }
        }
        if (viper.monsterState != State.MonsterState.M_Attack)
        {
            AttackNocice.SetActive(false);
            attackTime = 0;
            viper.counterjudgement = false;
        }
    }

    //private KeyValuePair<bool, Transform> Player_AttackViperEvent()
    //{
    //    return new KeyValuePair<bool, Transform>(viper.counterjudgement, transform);
    //}

    private void Attack()
    {
        AttackNocice.SetActive(viper.counterjudgement);
        if (distance() > viper.navigation.stoppingDistance)
        {
            viper.monsterState = State.MonsterState.M_Move;
            viper.animator.SetBool("viperAttack", false);
        }
    }

    private void Idle()
    {
        if (distance() < viper.attack_aware_distance)
        {
            viper.monsterState = State.MonsterState.M_Move;
            viper.animator.SetBool("viperWalk", true);
        }
        else
        {
            viper.animator.SetBool("viperWalk", false);
        }
        if (targeting)
        {
            viper.monsterState = State.MonsterState.M_Move;
            viper.animator.SetBool("viperWalk", true);
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

        if (distance() < viper.navigation.stoppingDistance)
        {
            viper.monsterState = State.MonsterState.M_Attack;
            viper.animator.SetBool("viperAttack", true);
        }
    }

    IEnumerator navigationSet()
    {
        running = true;
        yield return new WaitForSecondsRealtime(0.25f);
        if(!dead)
            viper.navigation.SetDestination(target.transform.position);
        running = false;
    }

    public void PlayerLookAt()
    {   
        if(!dead)
        transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
    }

    public void AttackHit()
    {
        if (!dead)
        {
            count++;

            viper.animator.SetBool("viperWalk", false);
            viper.animator.SetBool("viperAttack", false);
            viper.animator.SetTrigger("viperHit");
            viper.monsterState = State.MonsterState.M_Damage;
            if (GameEventToUI.Instance.Attack_SuccessEvent())
            {
                GameEventToUI.Instance.OnAttactReset();
            }
        }
}

    public void ExitHit()
    {
        if (!dead)
        {
            if (viper.monsterState == State.MonsterState.M_Damage)
                viper.monsterState = State.MonsterState.M_Move;
            viper.animator.SetBool("viperWalk", true);
        }
    }

    public void AttackSetting()
    {
        attackTime = 0;
    }

    private float distance()
    {
        float distance = (transform.position - target.transform.position).magnitude;
        return distance;
    }


  

}
