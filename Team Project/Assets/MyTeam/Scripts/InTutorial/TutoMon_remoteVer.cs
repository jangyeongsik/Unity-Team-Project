using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoMon_remoteVer : MonoBehaviour
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
        monster.attack_aware_distance = 10.0f;
        EnemySet();
    }


    private void EnemySet()
    {
        monster.monsterState = State.MonsterState.M_Idle;
        monster.animator = GetComponent<Animator>();
        //monster.rigid = GetComponent<Rigidbody>();
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
                Move();
                break;
            case State.MonsterState.M_Move:
                break;
            case State.MonsterState.M_Dead:
                break;
            case State.MonsterState.M_Groar:
                break;
            case State.MonsterState.M_Attack:
                break;
            case State.MonsterState.M_Return:
                break;
            case State.MonsterState.M_Damage:
                break;
            default:
                break;
        }
    }

    private void Move()
    {
        float distanceToTarget = (transform.position - target.transform.position).magnitude;
        if (distanceToTarget <  10.0f)
        {
            transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
            monster.animator.SetBool("isAttack", true);
        }
        else
        {
            monster.animator.SetBool("isAttack", false);
        }
    }

    public void OnDeadEvent()
    {
        //플레이어 실린더 게이지 추가
        //GameEventToUI.Instance.OnPlayerCylinderGauge(10);
        //SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Bone, "Hit");
    }

    public void ArrowFire()
    {
        if (gameObject.CompareTag("EnemyVishop"))
        {
            GameObject vishopArrow = ObjectPoolManager.GetInstance().objectPool2.PopObject();
            Vector3 dir = tPos - arrowFirePoint.position;
            dir.y = 0;
            vishopArrow.transform.position = arrowFirePoint.position;
            vishopArrow.transform.LookAt(vishopArrow.transform.position + dir);
            vishopArrow.GetComponent<VishopArrow>().EnemyTranform = transform;
            SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Bone, "Arrow");
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
