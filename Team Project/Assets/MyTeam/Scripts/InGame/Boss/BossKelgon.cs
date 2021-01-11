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

    public GameObject ChargeCircle1;
    public GameObject ChargeCircle2;
    public GameObject ChargeCircle3;
    public GameObject AttackNotice;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monster.EnemyHitEvent += KelgonHitEvent;
        kelgon = GetComponent<BossData>();
        kelgon.navigation = GetComponent<NavMeshAgent>();
        kelgon.animator = transform.GetChild(0).GetComponent<Animator>();
        kelgon.position = transform;
    }

    private void OnDestroy()
    {
        monster.EnemyHitEvent -= KelgonHitEvent;
    }


    void KelgonHitEvent()
    {
        AttackNotice.SetActive(false);
        kelgon.animator.SetTrigger("Hit");
    }

    public void SetTarget(Transform T)
    {
        target = T;
        kelgon.target = T;
        monster.position = T;
    }
}