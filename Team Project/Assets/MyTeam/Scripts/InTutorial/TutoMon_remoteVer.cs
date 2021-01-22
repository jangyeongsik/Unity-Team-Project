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

    bool running = false;

    Vector3 tPos;

    private bool attacking;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monster.position = transform;
        monster.monsterKind = State.MonsterKind.M_Archer;
        target = GameData.Instance.player.position.gameObject;
        monster.damage = 0;
        EnemySet();
    }


    private void EnemySet()
    {
        monster.monsterState = State.MonsterState.M_Idle;
        monster.animator = GetComponent<Animator>();
        monster.rigid = GetComponent<Rigidbody>();
        monster.EnemyHitEvent += OnDeadEvent;
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
                GotDamage();
                break;
            default:
                break;
        } 
    }

    private void GotDamage()
    {
        
    }

    private void Move()
    {
        if (monster.DistacneWithTarget() < 8.0f)
        {
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
            vishopArrow.GetComponent<VishopArrow>().damage = monster.damage;
            vishopArrow.transform.position = arrowFirePoint.position;
            vishopArrow.transform.LookAt(vishopArrow.transform.position + dir);
            vishopArrow.GetComponent<VishopArrow>().EnemyTranform = transform;
            SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Bone, "Arrow");
        }
    }

    public void OnDeadEvent(int damage)
    {
        GameEventToUI.Instance.OnEventTutoAttack();
        monster.monsterState = State.MonsterState.M_Damage;
        monster.animator.SetTrigger("isHit");
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Bone, "Hit");
    }

    public void PlayerLookAt()
    {
        transform.LookAt(target.transform);
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
