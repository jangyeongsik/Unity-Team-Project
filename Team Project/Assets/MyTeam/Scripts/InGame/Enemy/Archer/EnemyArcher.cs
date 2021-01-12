using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArcher : MonoBehaviour
{
    private Monster monster;

    private GameObject target;
    public Transform arrowTarget;

    public Transform arrowFirePoint;
    private Vector3 shotDirection;

    public GameObject arrowPrefab;

    bool running = false;

    Vector3 tPos;

    bool counterjudgement;
    private bool attacking;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monster.position = transform;
        monster.monsterKind = State.MonsterKind.M_Archer;
        monster.EnemyHitEvent += OnDeadEvent;
        target = GameData.Instance.player.position.gameObject;
        RemoteEnemySetting();
    }

    private void EnemySet(int attack)
    {
        monster.monsterState = State.MonsterState.M_Idle;
        monster.navigation = GetComponent<NavMeshAgent>();
        monster.animator = GetComponent<Animator>();
        monster.movespeed = 8.0f;
        monster.damage = attack;
    }

    private void RemoteEnemySetting()
    {
        if(gameObject.name == "EnemyArcher")
        {
            EnemySet(3);
            monster.attack_aware_distance = 6.0f;
        }
        //else if(gameObject.name == "EnemyArcher")
        //{
        //    EnemySet(1);
        //    monster.attack_aware_distance = 6.0f;
        //}
        //else
        //{
        //    EnemySet(1);
        //    monster.attack_aware_distance = 6.0f;
        //}
        else if(gameObject.CompareTag("EnemyVishop"))
        {
            EnemySet(1);
            monster.attack_aware_distance = 6.0f;
        }
    }

    private void OnDestroy()
    {
        monster.EnemyHitEvent -= OnDeadEvent;
    }
    private void Update()
    {
        switch (monster.monsterState)
        {
            case State.MonsterState.M_None:
                break;
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
            default:
                break;
        }
    }

    private void Attack()
    {
        float distanceToTarget = (transform.position - target.transform.position).magnitude;
        if (distanceToTarget > monster.navigation.stoppingDistance)
        {
            monster.monsterState = State.MonsterState.M_Move;
            monster.animator.SetBool("isAttack", false);
        }
    }

    private void Idle()
    {
        float distanceToTarget = (transform.position - target.transform.position).magnitude;
        if (distanceToTarget < monster.attack_aware_distance)
        {
            monster.monsterState = State.MonsterState.M_Move;
            monster.animator.SetBool("isRun",true);
        }
        else
        {
            monster.animator.SetBool("isRun",false);
        }
    }

    private void Move()
    {
        if (!running)
        { 
            StartCoroutine(navigationSet());
        }

        float distanceToTarget = (transform.position - target.transform.position).magnitude;
        if (distanceToTarget < monster.navigation.stoppingDistance)
        {
            transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
            monster.monsterState = State.MonsterState.M_Attack;

            monster.animator.SetBool("isAttack", true);
        }
    }

    IEnumerator navigationSet()
    {
        running = true;
        yield return new WaitForSecondsRealtime(0.25f);
        monster.navigation.SetDestination(target.transform.position);
        running = false;
    }
    public void OnDeadEvent()
    {
        monster.animator.SetBool("isDead", true);
        monster.monsterState = State.MonsterState.M_Dead;
        //플레이어 실린더 게이지 추가
        GameEventToUI.Instance.OnPlayerCylinderGauge(10);
    }

    public void OnTargetingEvent()
    {
        
    }

    public void ArrowFire()
    {
        if(gameObject.CompareTag("EnemyArcher"))
        {
            GameObject arrow = ObjectPoolManager.GetInstance().objectPool.PopObject();
            Vector3 dir = tPos - arrowFirePoint.position;
            dir.y = 0;
            arrow.transform.position = arrowFirePoint.position;
            arrow.transform.LookAt(arrow.transform.position + dir);
            arrow.GetComponent<Arrow>().EnemyTranform = transform;
        }
        else if (gameObject.CompareTag("EnemyVishop"))
        {
            GameObject vishopArrow = ObjectPoolManager.GetInstance().objectPool2.PopObject();
            Vector3 dir = tPos - arrowFirePoint.position;
            dir.y = 0;
            vishopArrow.transform.position = arrowFirePoint.position;
            vishopArrow.transform.LookAt(vishopArrow.transform.position + dir);
            vishopArrow.GetComponent<VishopArrow>().EnemyTranform = transform;
        }
    }

    public void PlayerLookAt()
    {
        transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
    }

    public void SavePos()
    {
        tPos = target.transform.position;
    }

    public void Attacking()
    {
        attacking = !attacking;
    }
}
