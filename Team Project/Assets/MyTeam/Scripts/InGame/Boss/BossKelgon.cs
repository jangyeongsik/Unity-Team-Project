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


    void KelgonHitEvent()
    {
        Debug.Log("damage");
    }

    public void SetTarget(Transform T)
    {
        target = T;
        kelgon.target = T;
    }
}