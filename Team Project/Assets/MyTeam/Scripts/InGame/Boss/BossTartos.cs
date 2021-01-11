using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTartos : MonoBehaviour
{

    private BossData b_tartos;         //몬스터 클래스
    private bool targeting = false;             //타겟 조준

    public GameObject target;
    Vector3 offset;
    Vector3 pattonoffset;

    public bool counterjudgement;

    float attackTime;

    float attckCountMin = 0.2f;
    float attckCountMax = 0.6f;

    private int count;

    int animCount;
    int pattonCount;

    bool running = false;

    #region 패턴1 
    public GameObject pattonCharge1_1;
    public GameObject patton1_1;

    public GameObject pattonCharge1_2;
    public GameObject patton1_2;

    public GameObject pattonCharge1_3;
    public GameObject patton1_3;

    public GameObject pattonCharge1_4;
    public GameObject patton1_4;

    public GameObject pattonCharge1_5;
    public GameObject patton1_5;


    float timer1_1 = 0;
    float timer1_2 = 0;
    float timer1_3 = 0;
    int skillCount = 0;

    private bool isCharge = false;
    private bool isAttack = false;
    private bool checkingPlayer = false;

    private Collider[] cols;
    #endregion

    #region 패턴2
     GameObject skillChargeTwo;
     GameObject PattonTwo;

    float coolTime;

    private bool isTwoCharge = false;
    private bool isTwoAttack = false;
    private bool checkingPlayerTwo = false;

    private Collider[] colsTwo;
    #endregion

  
    private bool dead;
    private void Start()
    {
        GameEventToUI.Instance.Player_Attack += Player_AttackEvent;
        b_tartos = GetComponent<BossData>();
        setting();
    }

    private void setting()
    {
        b_tartos.bossState = State.BossState.B_Idle;
        b_tartos.animator = GetComponent<Animator>();
        b_tartos.movespeed = 13.0f;
        b_tartos.attack_aware_distance = 3;
    }
    private void Update()
    {
        if (!dead)
        {
            if (count >= 8)
            {
                dead = true;
                running = false;
                counterjudgement = false;
                b_tartos.animator.SetInteger("SetAnim", 10);
                b_tartos.bossState = State.BossState.B_Dead;
                GameEventToUI.Instance.OnAttactReset();
            }
            switch (b_tartos.bossState)
            {
                case State.BossState.B_Idle:
                    B_Idle();
                    break;
                case State.BossState.B_Move:
                    B_Move();
                    break;
                case State.BossState.B_Attack:
                    //B_Attack();
                    break;
                case State.BossState.B_SkillChargeOne:
                    animCount = 0;
                    B_SkillCharge1_1();
                    break;
                case State.BossState.B_SkillChargeTwo:
                    animCount = 3;
                    B_SkillChargetwo();
                    break;
                case State.BossState.B_SkillOne:
                    B_SkillOne();
                    break;
                case State.BossState.B_SkillTwo:
                    B_SkillTwo();
                    break;
                case State.BossState.B_Hit:
                    break;
                case State.BossState.B_Dead:
                    break;
                case State.BossState.B_AttackTwo:
                    //B_AttackTwo();
                    break;
            }
        }
        if (b_tartos.bossState != State.BossState.B_Attack)
        {
            attackTime = 0;
            counterjudgement = false;
        }

    }

    private void B_Idle()
    {
        targeting = false;
        if (P_distance() > b_tartos.attack_aware_distance)
        {
            b_tartos.bossState = State.BossState.B_Move;
            b_tartos.animator.SetInteger("SetAnim", 1);
        }
        if (targeting)
        {
            b_tartos.bossState = State.BossState.B_Move;
            b_tartos.animator.SetInteger("SetAnim", 1);
        }
    }

    private void B_Move()
    {

        if (P_distance() < b_tartos.attack_aware_distance)
        {
            pattonSet();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.05f);
            Vector3 dir = target.transform.position - gameObject.transform.position;
            gameObject.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }

    }


    public void B_Attack()
    {

        if (P_distance() > b_tartos.attack_aware_distance)
        {
            b_tartos.bossState = State.BossState.B_Move;
            b_tartos.animator.SetInteger("SetAnim", 1);
        }
        else
        {
            b_tartos.bossState = State.BossState.B_AttackTwo;
            b_tartos.animator.SetInteger("SetAnim", 11);
        }
        animCount = 1;
    }
    public void B_AttackTwo()
    {

        if (P_distance() > b_tartos.attack_aware_distance)
        {
            b_tartos.bossState = State.BossState.B_Move;
            b_tartos.animator.SetInteger("SetAnim", 1);
        }
        else
        {
            if (pattonCount == 0)
            {
                b_tartos.bossState = State.BossState.B_SkillChargeTwo;
                b_tartos.animator.SetInteger("SetAnim", 4);
                isTwoCharge = true;
            }
            else
            {
                b_tartos.bossState = State.BossState.B_SkillChargeOne;
                b_tartos.animator.SetInteger("SetAnim", 3);
                isCharge = true;
            }

        }
        animCount = 2;
    }



    public void OnTargetingEvent()
    {
        targeting = true;
    }
    #region 플레이어와 거리 구하는 함수
    private float P_distance()
    {
        offset = transform.position - target.transform.position;
        float distance = offset.magnitude;
        return distance;
    }
    #endregion
    #region 패턴3 거리 구하는 함수
    private float Pattondistance()
    {
        pattonoffset = target.transform.position - gameObject.transform.position;
        float distance = pattonoffset.magnitude;
        return distance;
    }
    #endregion

    public void ExitHit()
    {
        if (b_tartos.bossState == State.BossState.B_Hit)
            b_tartos.bossState = State.BossState.B_Idle;
    }
    void attackCount()
    {
        attackTime += Time.deltaTime;

        if (attackTime > attckCountMin && attackTime < attckCountMax)
        {
            counterjudgement = true;
        }
        else
        {
            counterjudgement = false;
        }
    }


    private KeyValuePair<bool, Transform> Player_AttackEvent()
    {
        return new KeyValuePair<bool, Transform>(counterjudgement, transform);
    }

    public void AttackSetting()
    {
        attackTime = 0;
    }

    public void AttackHit()
    {
        count++;

        b_tartos.animator.SetInteger("SetAnim", 9);
        b_tartos.bossState = State.BossState.B_Hit;
        if (GameEventToUI.Instance.Attack_SuccessEvent())
        {
            GameEventToUI.Instance.OnAttactReset();
        }
    }

    public void B_SkillCharge1_1()
    {
        animCount = 0;

        if (pattonCharge1_3.transform.localScale.x > 1)
        {
            isCharge = false;
            b_tartos.animator.SetInteger("SetAnim", 3);
            b_tartos.bossState = State.BossState.B_SkillChargeOne;
            B_SkillCharge1_2();

        }
        if (!isCharge)
        {
            b_tartos.animator.SetInteger("SetAnim", 6);
            b_tartos.bossState = State.BossState.B_SkillOne;
            pattonCharge1_3.transform.localScale = new Vector3(0, 0, 0.1f);
            patton1_3.SetActive(false);
            pattonCharge1_3.SetActive(false);
            timer1_1 = 0;
        }
        else
        {
            patton1_3.SetActive(true);
            pattonCharge1_3.SetActive(true);
            timer1_1 += Time.deltaTime;
            if (pattonCharge1_3.transform.localScale.x < 1.0f)
            {
                pattonCharge1_3.transform.localScale = new Vector3(0.4f * timer1_1, 0.4f * timer1_1, 1);
                isAttack = true;
            }
        }

    }

    public void B_SkillCharge1_2()
    {
        bool isCharge1_2 = false;
        animCount = 0;

        if (pattonCharge1_2.transform.localScale.x > 1)
        {
            isCharge1_2 = false;
            B_SkillCharge1_3();

        }
        if (!isCharge1_2)
        {
            b_tartos.animator.SetInteger("SetAnim", 6);
            b_tartos.bossState = State.BossState.B_SkillOne;

            pattonCharge1_2.transform.localScale = new Vector3(0, 0, 0.1f);
            patton1_2.SetActive(false);
            pattonCharge1_2.SetActive(false);

            pattonCharge1_4.transform.localScale = new Vector3(0, 0, 0.1f);
            patton1_4.SetActive(false);
            pattonCharge1_4.SetActive(false);

            timer1_2 = 0;
        }
        else
        {
            patton1_2.SetActive(true);
            pattonCharge1_2.SetActive(true);

            patton1_4.SetActive(true);
            pattonCharge1_4.SetActive(true);

            timer1_2 += Time.deltaTime;

            if (pattonCharge1_2.transform.localScale.x < 1.0f)
            {
                pattonCharge1_2.transform.localScale = new Vector3(0.4f * timer1_2, 0.4f * timer1_2, 0.1f);
                pattonCharge1_4.transform.localScale = new Vector3(0.4f * timer1_2, 0.4f * timer1_2, 0.1f);
                isAttack = true;
            }
        }
    }

    public void B_SkillCharge1_3()
    {
        bool isCharge1_3 = false;
        animCount = 0;

        if (pattonCharge1_3.transform.localScale.x > 1)
        {
            isCharge1_3 = false;
            b_tartos.animator.SetInteger("SetAnim", 2);
            b_tartos.bossState = State.BossState.B_Attack;

        }
        if (!isCharge1_3)
        {
            b_tartos.animator.SetInteger("SetAnim", 6);
            b_tartos.bossState = State.BossState.B_SkillOne;

            pattonCharge1_1.transform.localScale = new Vector3(0, 0, 0.1f);
            patton1_1.SetActive(false);
            pattonCharge1_1.SetActive(false);

            pattonCharge1_3.transform.localScale = new Vector3(0, 0, 0.1f);
            patton1_3.SetActive(false);
            pattonCharge1_3.SetActive(false);

            pattonCharge1_5.transform.localScale = new Vector3(0, 0, 0.1f);
            patton1_5.SetActive(false);
            pattonCharge1_5.SetActive(false);

            timer1_3 = 0;
        }
        else
        {
            patton1_1.SetActive(true);
            pattonCharge1_1.SetActive(true);

            patton1_3.SetActive(true);
            pattonCharge1_3.SetActive(true);

            patton1_5.SetActive(true);
            pattonCharge1_5.SetActive(true);

            timer1_3 += Time.deltaTime;

            if (pattonCharge1_3.transform.localScale.x < 1.0f)
            {
                pattonCharge1_1.transform.localScale = new Vector3(0.4f * timer1_3, 0.4f * timer1_3, 0.1f);
                pattonCharge1_3.transform.localScale = new Vector3(0.4f * timer1_3, 0.4f * timer1_3, 0.1f);
                pattonCharge1_5.transform.localScale = new Vector3(0.4f * timer1_3, 0.4f * timer1_3, 0.1f);
                isAttack = true;
            }
        }
    }

    private void B_SkillOne()
    {

        cols = Physics.OverlapSphere(gameObject.transform.position, 8.0f);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Player")
            {
                checkingPlayer = true;
            }
        }
        b_tartos.animator.SetInteger("SetAnim", 2);
        b_tartos.bossState = State.BossState.B_Attack;
        pattonCount = 0;
    }

    public void B_SkillChargetwo()
    {

        if (skillChargeTwo.transform.localScale.x > 1)
        {
            isTwoCharge = false;
        }
        if (!isTwoCharge)
        {
            b_tartos.animator.SetInteger("SetAnim", 7);
            b_tartos.bossState = State.BossState.B_SkillTwo;
            skillChargeTwo.transform.localScale = new Vector3(0, 0, 0.1f);
            PattonTwo.SetActive(false);
            skillChargeTwo.SetActive(false);
            coolTime = 0;
        }
        else
        {
            PattonTwo.SetActive(true);
            skillChargeTwo.SetActive(true);
            coolTime += Time.deltaTime;
            if (skillChargeTwo.transform.localScale.x < 1.0f)
            {

                skillChargeTwo.transform.localScale = new Vector3(0.25f * coolTime, 0.25f * coolTime, 0.1f);
                isTwoAttack = true;
            }
        }
    }
    public void B_SkillTwo()
    {
        colsTwo = Physics.OverlapSphere(gameObject.transform.position, 8.0f);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Player")
            {
                checkingPlayerTwo = true;
            }
        }
        pattonCount = 1;
        b_tartos.animator.SetInteger("SetAnim", 2);
        b_tartos.bossState = State.BossState.B_Attack;
    }

    public void pattonSet()
    {
        if (animCount == 0)
        {
            isCharge = true;
            b_tartos.bossState = State.BossState.B_SkillChargeOne;
            b_tartos.animator.SetInteger("SetAnim", 3);
        }
        else if (animCount == 1)
        {
            b_tartos.bossState = State.BossState.B_Attack;
            b_tartos.animator.SetInteger("SetAnim", 2);
        }
        else if (animCount == 2)
        {
            b_tartos.bossState = State.BossState.B_AttackTwo;
            b_tartos.animator.SetInteger("SetAnim", 11);
        }
        else if (animCount == 3)
        {
            isTwoCharge = true;
            b_tartos.bossState = State.BossState.B_SkillChargeTwo;
            b_tartos.animator.SetInteger("SetAnim", 4);
        }
    }
}

