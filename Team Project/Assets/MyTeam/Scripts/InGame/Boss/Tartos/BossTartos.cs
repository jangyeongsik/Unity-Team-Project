using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossTartos : MonoBehaviour
{
    public Transform target;
    public Transform pattonTarget;

    BossData tartos;
    Monster monster;

    int hp = 20;

    public GameObject tartosPatton1_1;
    public GameObject tartosPatton1_2;
    public GameObject tartosPatton1_3;
    public GameObject tartosPatton1_4;
    public GameObject tartosPatton1_5;

    public GameObject tartosPattonPaticle1_1;
    public GameObject tartosPattonPaticle1_2;
    public GameObject tartosPattonPaticle1_3;
    public GameObject tartosPattonPaticle1_4;
    public GameObject tartosPattonPaticle1_5;

    public GameObject tartosPatton2_1;
    public GameObject tartosPatton2_2;
    public GameObject tartosPatton2_3;
    public GameObject tartosPatton2_4;
    public GameObject tartosPatton2_5;
    public GameObject tartosPatton2_6;
    public GameObject tartosPatton2_7;
    public GameObject tartosPatton2_8;

    public GameObject tartosPattonPaticle2_1;
    public GameObject tartosPattonPaticle2_2;
    public GameObject tartosPattonPaticle2_3;
    public GameObject tartosPattonPaticle2_4;
    public GameObject tartosPattonPaticle2_5;
    public GameObject tartosPattonPaticle2_6;
    public GameObject tartosPattonPaticle2_7;
    public GameObject tartosPattonPaticle2_8;

    public GameObject tartosPatton2_Center;

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

    void TartosHitEvent(int damage)
    {
        hp -= damage;

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

            if (tartosPattonPaticle1_1.activeSelf)
                tartosPattonPaticle1_1.SetActive(false);
            if (tartosPattonPaticle1_2.activeSelf)
                tartosPattonPaticle1_2.SetActive(false);
            if (tartosPattonPaticle1_3.activeSelf)
                tartosPattonPaticle1_3.SetActive(false);
            if (tartosPattonPaticle1_4.activeSelf)
                tartosPattonPaticle1_4.SetActive(false);
            if (tartosPattonPaticle1_5.activeSelf)
                tartosPattonPaticle1_5.SetActive(false);

            if (tartosPattonPaticle2_1.activeSelf)
                tartosPattonPaticle2_1.SetActive(false);
            if (tartosPattonPaticle2_2.activeSelf)
                tartosPattonPaticle2_2.SetActive(false);
            if (tartosPattonPaticle2_3.activeSelf)
                tartosPattonPaticle2_3.SetActive(false);
            if (tartosPattonPaticle2_4.activeSelf)
                tartosPattonPaticle2_4.SetActive(false);
            if (tartosPattonPaticle2_5.activeSelf)
                tartosPattonPaticle2_5.SetActive(false);
            if (tartosPattonPaticle2_6.activeSelf)
                tartosPattonPaticle2_6.SetActive(false);
            if (tartosPattonPaticle2_7.activeSelf)
                tartosPattonPaticle2_7.SetActive(false);
            if (tartosPattonPaticle2_8.activeSelf)
                tartosPattonPaticle2_8.SetActive(false);
        }
    }

    public void SetTarget(Transform T)
    {
        target = T;
        tartos.target = T;
    }

    #region 패턴1 파티클
    public void PaticleOn1_1()
    {
        tartosPattonPaticle1_3.transform.position = tartosPatton1_3.transform.position;
        tartosPattonPaticle1_3.transform.rotation = tartosPatton1_3.transform.rotation;
        tartosPattonPaticle1_3.SetActive(true);
    }

    public void PaticleOn1_2()
    {      
        tartosPattonPaticle1_2.transform.position = tartosPatton1_2.transform.position;
        tartosPattonPaticle1_2.transform.rotation = tartosPatton1_2.transform.rotation;
        tartosPattonPaticle1_2.SetActive(true);
        tartosPattonPaticle1_4.transform.position = tartosPatton1_4.transform.position;
        tartosPattonPaticle1_4.transform.rotation = tartosPatton1_4.transform.rotation;
        tartosPattonPaticle1_4.SetActive(true);
    }
    public void PaticleOn1_3()
    {
        tartosPattonPaticle1_1.transform.position = tartosPatton1_1.transform.position;
        tartosPattonPaticle1_3.transform.position = tartosPatton1_3.transform.position;
        tartosPattonPaticle1_5.transform.position = tartosPatton1_5.transform.position;
 
        tartosPattonPaticle1_1.transform.rotation = tartosPatton1_1.transform.rotation;
        tartosPattonPaticle1_3.transform.rotation = tartosPatton1_3.transform.rotation;
        tartosPattonPaticle1_5.transform.rotation = tartosPatton1_5.transform.rotation;
        tartosPattonPaticle1_1.SetActive(true);
        tartosPattonPaticle1_3.SetActive(true);
        tartosPattonPaticle1_5.SetActive(true);
    }
    public void PaticleOff1_1()
    {
        tartosPattonPaticle1_3.SetActive(false);
    }
    public void PaticleOff1_2()
    {
        tartosPattonPaticle1_2.SetActive(false);
        tartosPattonPaticle1_4.SetActive(false);
    }
    public void PaticleOff1_3()
    {
        tartosPattonPaticle1_1.SetActive(false);
        tartosPattonPaticle1_3.SetActive(false);
        tartosPattonPaticle1_5.SetActive(false);
    }
    #endregion
    #region 패턴2_1 파티클
    public void PaticleOn2_1_1()
    {
        tartosPattonPaticle2_1.SetActive(true);
        tartosPattonPaticle2_2.SetActive(true);
        tartosPattonPaticle2_3.SetActive(true);
        tartosPattonPaticle2_4.SetActive(true);
    }
    public void PaticleOn2_1_2()
    {
        tartosPattonPaticle2_5.SetActive(true);
        tartosPattonPaticle2_6.SetActive(true);
        tartosPattonPaticle2_7.SetActive(true);
        tartosPattonPaticle2_8.SetActive(true);
    }
    public void PaticleOff2_1_1()
    {
        tartosPattonPaticle2_1.SetActive(false);
        tartosPattonPaticle2_2.SetActive(false);
        tartosPattonPaticle2_3.SetActive(false);
        tartosPattonPaticle2_4.SetActive(false);
    }
    public void PaticleOff2_1_2()
    {
        tartosPattonPaticle2_5.SetActive(false);
        tartosPattonPaticle2_6.SetActive(false);
        tartosPattonPaticle2_7.SetActive(false);
        tartosPattonPaticle2_8.SetActive(false);
    }
    #endregion
    #region 패턴 2_2파티클

    public void PaticleOn2_2_1()
    {
        tartosPattonPaticle2_1.SetActive(true);
        tartosPattonPaticle2_3.SetActive(true);
        tartosPattonPaticle2_6.SetActive(true);
        tartosPattonPaticle2_8.SetActive(true);
    }

    public void PaticleOn2_2_2()
    {
        tartosPattonPaticle2_2.SetActive(true);
        tartosPattonPaticle2_4.SetActive(true);
        tartosPattonPaticle2_5.SetActive(true);
        tartosPattonPaticle2_7.SetActive(true);
    }

    public void PaticleOff2_2_1()
    {
        tartosPattonPaticle2_1.SetActive(false);
        tartosPattonPaticle2_3.SetActive(false);
        tartosPattonPaticle2_6.SetActive(false);
        tartosPattonPaticle2_8.SetActive(false);
    }

    public void PaticleOff2_2_2()
    {
        tartosPattonPaticle2_2.SetActive(false);
        tartosPattonPaticle2_4.SetActive(false);
        tartosPattonPaticle2_5.SetActive(false);
        tartosPattonPaticle2_7.SetActive(false);
    }
    #endregion
    #region 패턴2_3파티클

    public void PaticleOn2_3_1()
    {
        tartosPattonPaticle2_1.SetActive(true);
        tartosPattonPaticle2_2.SetActive(true);
    }

    public void PaticleOn2_3_2()
    {
        tartosPattonPaticle2_3.SetActive(true);
        tartosPattonPaticle2_5.SetActive(true);
    }

    public void PaticleOn2_3_3()
    {
        tartosPattonPaticle2_7.SetActive(true);
        tartosPattonPaticle2_8.SetActive(true);
    }
    public void PaticleOn2_3_4()
    {
        tartosPattonPaticle2_4.SetActive(true);
        tartosPattonPaticle2_6.SetActive(true);
    }

    public void PaticleOn2_3_5()
    {
        tartosPattonPaticle2_1.SetActive(true);
        tartosPattonPaticle2_2.SetActive(true);
        tartosPattonPaticle2_3.SetActive(true);
        tartosPattonPaticle2_4.SetActive(true);
        tartosPattonPaticle2_5.SetActive(true);
        tartosPattonPaticle2_6.SetActive(true);
        tartosPattonPaticle2_7.SetActive(true);
    }
    public void PaticleOff2_3_1()
    {
        tartosPattonPaticle2_1.SetActive(false);
        tartosPattonPaticle2_2.SetActive(false);
        tartosPattonPaticle2_3.SetActive(false);
        tartosPattonPaticle2_4.SetActive(false);
        tartosPattonPaticle2_5.SetActive(false);
        tartosPattonPaticle2_6.SetActive(false);
        tartosPattonPaticle2_7.SetActive(false);
        tartosPattonPaticle2_8.SetActive(false);
    }
    #endregion
}

