using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWolf : MonoBehaviour
{
    private Monster monster;
    private GameObject target;
    public GameObject AttackNocice;             

    bool wolfRunning = false;
    private bool targeting = false;
    private bool attacking;
    private bool dead = false;
    public bool counterjudgement;

    private Vector3 lookDirection;
    Vector3 offset;

    float attackTime;
    private int count;

    float attckCountMin = 0.2f;
    float attckCountMax = 0.6f;

    private void Start()
    {
        GameEventToUI.Instance.Player_Attack += Player_AttackEvent;
        monster = GetComponent<Monster>();
        WolfSetting();
        target = GameData.Instance.player.position.gameObject; 
    }

    //private void OnDisable()
    //{
    //    GameEventToUI.Instance.Player_Attack -= Player_AttackEvent;
    //}

    private void WolfSetting()
    {
        monster.monsterState = State.MonsterState.M_Idle;
        monster.navigation = GetComponent<NavMeshAgent>();
        monster.animator = GetComponent<Animator>();
        monster.movespeed = 10.0f;
        monster.attack_aware_distance = 3.0f;
        monster.target_notice_distance = 10.0f;
        monster.navigation.enabled = true;
    }

    private void Update()
    {
        if(!dead)
        {
            //if(GameEventToUI.Instance.Attack_SuccessEvent())
            //{
            //    count++;
            //    monster.animator.SetBool("wolfDash", false);
            //    monster.animator.SetBool("wolfAttack", false);
            //    monster.animator.SetTrigger("wolfHit");
            //    monster.monsterState = State.MonsterState.M_Damage;
            //    GameEventToUI.Instance.OnAttactReset();
            //}
            if(count >= 5)
            {
                monster.navigation.enabled = false;
                dead = true;
                wolfRunning = false;
                counterjudgement = false;
                monster.animator.SetBool("wolfDash", false);
                monster.animator.SetBool("wolfAttack", false);
                monster.animator.SetTrigger("wolfDead");
                monster.monsterState = State.MonsterState.M_Dead;
                GameEventToUI.Instance.OnAttactReset();
            }
            switch (monster.monsterState)
            {
                case State.MonsterState.M_Idle:
                    Idle();
                    break;
                case State.MonsterState.M_Dash:
                    break;
                case State.MonsterState.M_Move:
                    StartRun();
                    break;
                case State.MonsterState.M_Dead:
                    break;
                case State.MonsterState.M_Groar:
                    break;
                case State.MonsterState.M_Attack:
                    attackCount();
                    StartAttack();
                    break;
                case State.MonsterState.M_Return:
                    break;
                case State.MonsterState.M_Damage:
                    break;
            }
        }
        if(monster.monsterState != State.MonsterState.M_Attack)
        {
            AttackNocice.SetActive(false);
            attackTime = 0;
            counterjudgement = false;
        }
    }

    private KeyValuePair<bool, Transform> Player_AttackEvent()
    {
        return new KeyValuePair<bool, Transform>(counterjudgement, transform);
    }

    private void Idle()
    {
        if(targetDistance() < monster.target_notice_distance)
        {
            monster.monsterState = State.MonsterState.M_Move;
            monster.animator.SetBool("wolfDash", true);
        }
        else if(targetDistance() < monster.attack_aware_distance)
        {
            monster.monsterState = State.MonsterState.M_Attack;
            monster.animator.SetBool("wolfDash", true);
            monster.animator.SetBool("wolfAttack", true);
        }
        if(targeting)
        {
            monster.monsterState = State.MonsterState.M_Move;
            monster.animator.SetBool("wolfDash", true);
        }
    }

    public void OnTargetingEvent()
    {
        targeting = true;
    }

    private void StartRun()
    {
        if (!wolfRunning)
        {
            StartCoroutine(navigationSet());
        }
        if (targetDistance() < monster.attack_aware_distance)
        {
            monster.monsterState = State.MonsterState.M_Attack;
            monster.animator.SetBool("wolfAttack", true);
        }
    }

    IEnumerator navigationSet()
    {
        wolfRunning = true;
        yield return new WaitForSecondsRealtime(0.3f);

        monster.navigation.SetDestination(target.transform.position);
        wolfRunning = false;
    }

    private void StartAttack()
    {
        
        if (targetDistance() > monster.attack_aware_distance)
        {
            monster.monsterState = State.MonsterState.M_Move;
            monster.animator.SetBool("wolfAttack", false);
        }
    }

    private float targetDistance()
    {
        offset = transform.position - target.transform.position;
        float distance = offset.magnitude;
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

    public void AttackSetting()
    {
        attackTime = 0;
    }
}
