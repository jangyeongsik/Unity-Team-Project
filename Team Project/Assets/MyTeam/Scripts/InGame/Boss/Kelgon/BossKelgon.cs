using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossKelgon : MonoBehaviour
{
    public Transform target;

    BossData kelgon;
    Monster monster;

    
     public int hp = 20;

    public GameObject ChargeCircle1;
    public GameObject ChargeCircle1_1;
    public GameObject ChargeCircle2;
    public GameObject ChargeCircle3;
    public GameObject paticle3;
    public GameObject AttackNotice;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monster.EnemyHitEvent += KelgonHitEvent;
        monster.position = transform;
        kelgon = GetComponent<BossData>();
        kelgon.navigation = GetComponent<NavMeshAgent>();
        kelgon.animator = transform.GetChild(0).GetComponent<Animator>();
        kelgon.position = transform;
    }

    private void OnDestroy()
    {
        monster.EnemyHitEvent -= KelgonHitEvent;
    }


    void KelgonHitEvent(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            GameEventToUI.Instance.OnEvent_TalkBox(8015);
            kelgon.animator.SetTrigger("Dead");
            kelgon.bossState = State.BossState.B_Dead;
            monster.monsterState = State.MonsterState.M_Dead;
            GameEventToUI.Instance.OnEvent_TalkBox(8005);
            if(ChargeCircle1.activeSelf)
                ChargeCircle1.SetActive(false);
            if(ChargeCircle2.activeSelf)
                ChargeCircle2.SetActive(false);
            if(ChargeCircle3.activeSelf)
                ChargeCircle3.SetActive(false);
            if(AttackNotice.activeSelf)
                AttackNotice.SetActive(false);
            if (paticle3.activeSelf)
                paticle3.SetActive(false);
            GameData.Instance.player.bossClear = true;
        }
    }

    public void SetTarget(Transform T)
    {
        target = T;
        kelgon.target = T;
    }

    
}