using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossTartos : MonoBehaviour
{
    public Transform target;

    BossData tartos;
    Monster monster;

    int hp = 20;

    public GameObject tartosPatton1_1;
    public GameObject tartosPatton1_2;
    public GameObject tartosPatton1_3;
    public GameObject tartosPatton1_4;
    public GameObject tartosPatton1_5;

    public GameObject tartosPatton2_1;
    public GameObject tartosPatton2_2;
    public GameObject tartosPatton2_3;
    public GameObject tartosPatton2_4;
    public GameObject tartosPatton2_5;
    public GameObject tartosPatton2_6;
    public GameObject tartosPatton2_7;
    public GameObject tartosPatton2_8;

    public GameObject AttackNotice;

    public Transform pattern2Point;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monster.position = transform;
        monster.EnemyHitEvent += TartosHitEvent;
        tartos = GetComponent<BossData>();
        tartos.navigation = GetComponent<NavMeshAgent>();
        tartos.animator = transform.GetChild(0).GetComponent<Animator>();
        tartos.position = transform;
        tartos.target = target;
    }

    private void OnDestroy()
    {
        monster.EnemyHitEvent -= TartosHitEvent;
    }

    void TartosHitEvent()
    {
        --hp;

        if (hp <= 0)
        {
            tartos.animator.SetTrigger("Dead");
            tartos.bossState = State.BossState.B_Dead;
            monster.monsterState = State.MonsterState.M_Dead;

            if (tartosPatton1_1.activeSelf)
                tartosPatton1_1.SetActive(false);
            if (tartosPatton1_2.activeSelf)
                tartosPatton1_2.SetActive(false);
            if (tartosPatton1_3.activeSelf)
                tartosPatton1_3.SetActive(false);
            if (tartosPatton1_4.activeSelf)
                tartosPatton1_4.SetActive(false);
            if (tartosPatton1_5.activeSelf)
                tartosPatton1_5.SetActive(false);
            if (tartosPatton2_1.activeSelf)
                tartosPatton2_1.SetActive(false);
            if (tartosPatton2_2.activeSelf)
                tartosPatton2_2.SetActive(false);
            if (tartosPatton2_3.activeSelf)
                tartosPatton2_3.SetActive(false);
            if (tartosPatton2_4.activeSelf)
                tartosPatton2_4.SetActive(false);
            if (tartosPatton2_5.activeSelf)
                tartosPatton2_5.SetActive(false);
            if (tartosPatton2_6.activeSelf)
                tartosPatton2_6.SetActive(false);
            if (tartosPatton2_7.activeSelf)
                tartosPatton2_7.SetActive(false);
            if (tartosPatton2_8.activeSelf)
                tartosPatton2_8.SetActive(false);
            if (AttackNotice.activeSelf)
                AttackNotice.SetActive(false);
        }
    }

    public void SetTarget(Transform T)
    {
        target = T;
        tartos.target = T;
    }
}

