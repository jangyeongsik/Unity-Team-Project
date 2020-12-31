using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArcher : MonoBehaviour
{
    private Monster monster;

    public GameObject target;
    public Transform arrowTarget;

    public Transform arrowFirePoint;
    private Vector3 shotDirection;

    public GameObject arrowPrefab;

    Vector3 tPos;

    private int count;

    private void Start()
    {
        monster = new Monster();
        setting();
    }

    private void setting()
    {
        monster.monsterState = State.MonsterState.M_Idle;
        monster.navigation = GetComponent<NavMeshAgent>();
        monster.animator = GetComponent<Animator>();
        monster.movespeed = 8.0f;
        monster.attack_aware_distance = 10.0f;
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
        OnDeadEvent();
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
        

        monster.navigation.SetDestination(target.transform.position);

        float distanceToTarget = (transform.position - target.transform.position).magnitude;
        if (distanceToTarget < monster.navigation.stoppingDistance)
        {
            transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
            monster.monsterState = State.MonsterState.M_Attack;

            monster.animator.SetBool("isAttack", true);
        }
    }

    public void OnDeadEvent()
    {
        //if(Input.GetKeyDown(KeyCode.T))
        //monster.animator.SetBool("isDead", true);
    }

    public void OnTargetingEvent()
    {
        
    }

    public void ArrowFire()
    {
        //GameObject arrow = Instantiate(arrowPrefab, arrowFirePoint.position, Quaternion.identity);                                       
        //arrow.GetComponent<Arrow>().Fire(arrowTarget);
        GameObject arrow = Instantiate(arrowPrefab);
        Vector3 dir = tPos - arrowFirePoint.position;
        dir.y = 0;
        arrow.transform.position = arrowFirePoint.position;
        arrow.transform.LookAt(arrow.transform.position + dir);

        arrow.GetComponent<Arrow>().EnemyTranform = transform;
    }

    public void PlayerLookAt()
    {
        transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
    }

    public void SavePos()
    {
        tPos = target.transform.position;
    }
}
