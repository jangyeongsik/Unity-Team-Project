using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VishopFSM : MonoBehaviour
{
    private Monster vishop;
    private GameObject target;
    public GameObject AttackNotice;

    bool isRunning = false;
    bool counterjudgement;

    private bool attacking;
    private bool dead = false;
    private bool targeting = false;

    private int count;

    float attackTime;
    float attackCountMin = 0.2f;
    float attackCountMax = 0.6f;


    private void Start()
    {
        vishop = GetComponent<Monster>();
        vishop.position = transform;
        vishop.monsterKind = State.MonsterKind.M_Vishop;
        vishop.EnemyHitEvent += OnDeadEvent;
        VishopSetting();
        target = GameData.Instance.player.position.gameObject;
    }

    private void VishopSetting()
    {

    }

    private void OnDestroy()
    {
        vishop.EnemyHitEvent -= OnDeadEvent;
    }

    public void OnDeadEvent()
    {
        vishop.animator.SetBool("isDead", true);
        vishop.monsterState = State.MonsterState.M_Dead;
        //플레이어 실린더 게이지 추가
        GameEventToUI.Instance.OnPlayerCylinderGauge(10);
    }

    private void Update()
    {
        if(!dead)
        {

        }
    }
}
