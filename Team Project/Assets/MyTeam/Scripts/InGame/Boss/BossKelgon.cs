using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossKelgon : MonoBehaviour
{
    private BossData b_Kelgon ;         //몬스터 클래스
    private bool targeting = false;             //타겟 조준

    public GameObject target;
    Vector3 offset;

    public bool counterjudgement;

    float attackTime;

    float attckCountMin = 0.2f;
    float attckCountMax = 0.6f;

    private int count;

    int animCount;
    int pattonCount;

    bool running = false;

    #region 패턴1 
    public GameObject skillCharge;
    public GameObject cyclePatton;

    //public GameObject donutCharge;
    //public GameObject donutPatton;

    float saveTime;

    int skillCount = 0;

    private bool isCharge = false;
    private bool isDonutCharge = false;
    private bool isAttack = false;
    private bool checkingPlayer = false;

    private Collider[] cols;
    #endregion

    #region 패턴2
    public GameObject skillChargeTwo;
    public GameObject PattonTwo;

    float coolTime;

    private bool isTwoCharge = false;
    private bool isTwoAttack = false;
    private bool checkingPlayerTwo = false;

    private Collider[] colsTwo;
    #endregion

    #region 패턴3

    public GameObject pattonThreeCharge;
    public GameObject pattonThree;

    bool isPattonThreeCharge = false;
    private bool isThreeAttack = false;
    private bool checkingPlayerThree = false;

    private Collider[] colsThree;

    #endregion
    private bool dead;
    private void Start()
    {
        GameEventToUI.Instance.Player_Attack += Player_AttackEvent;
        b_Kelgon = GetComponent<BossData>();
        setting();
    }

    private void setting()
    {
        b_Kelgon.bossState = State.BossState.B_Idle;
        b_Kelgon.navigation = GetComponent<NavMeshAgent>();
        b_Kelgon.animator = GetComponent<Animator>();
        b_Kelgon.movespeed = 11.0f;
        b_Kelgon.attack_aware_distance = 3;
    }
    private void Update()
    {
        if (!dead)
        {
            if (count >= 6)
            {
                b_Kelgon.navigation.enabled = false;

                dead = true;
                running = false;
                counterjudgement = false;
                b_Kelgon.animator.SetInteger("SetAnim", 10);
                b_Kelgon.bossState = State.BossState.B_Dead;
                GameEventToUI.Instance.OnAttactReset();
            }
            switch (b_Kelgon.bossState)
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
                    B_SkillChargeOne();
                    break;
                case State.BossState.B_SkillChargeTwo:
                    animCount = 3;
                    B_SkillChargetwo();
                    break;
                case State.BossState.B_SkillChargeThree:
                    B_SkillChargeThree();
                    break;
                case State.BossState.B_SkillOne:
                    B_SkillOne();
                    break;
                case State.BossState.B_SkillTwo:
                    B_SkillTwo();
                    break;
                case State.BossState.B_SkillThree:
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
        if (b_Kelgon.bossState != State.BossState.B_Attack)
        {
            attackTime = 0;
            counterjudgement = false;
        }

    }

    private void B_Idle()
    {
        targeting = false;
        if (P_distance() > b_Kelgon.attack_aware_distance)
        {
            b_Kelgon.bossState = State.BossState.B_Move;
            b_Kelgon.animator.SetInteger("SetAnim", 1);
        }
        if (targeting)
        {
            b_Kelgon.bossState = State.BossState.B_Move;
            b_Kelgon.animator.SetInteger("SetAnim", 1);
        }
    }

    private void B_Move()
    {
        Vector3 velo = Vector3.zero;


        if (P_distance() < b_Kelgon.attack_aware_distance)
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
        
        if (P_distance() > b_Kelgon.attack_aware_distance)
        {
            b_Kelgon.bossState = State.BossState.B_Move;
            b_Kelgon.animator.SetInteger("SetAnim", 1);
        }
        else
        {
            b_Kelgon.bossState = State.BossState.B_AttackTwo;
            b_Kelgon.animator.SetInteger("SetAnim", 11);
        }
        animCount = 1;
    }
    public void B_AttackTwo()
    {

        if (P_distance() > b_Kelgon.attack_aware_distance)
        {
            b_Kelgon.bossState = State.BossState.B_Move;
            b_Kelgon.animator.SetInteger("SetAnim", 1);
        }
        else
        {
            if (pattonCount == 0)
            {
                b_Kelgon.bossState = State.BossState.B_SkillChargeTwo;
                b_Kelgon.animator.SetInteger("SetAnim", 4);
                isTwoCharge = true;
            }
            else
            {
                b_Kelgon.bossState = State.BossState.B_SkillChargeOne;
                b_Kelgon.animator.SetInteger("SetAnim", 3);
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

    public void ExitHit()
    {
        if (b_Kelgon.bossState == State.BossState.B_Hit)
            b_Kelgon.bossState = State.BossState.B_Idle;
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

        b_Kelgon.animator.SetInteger("SetAnim", 9);
        b_Kelgon.bossState = State.BossState.B_Hit;
        if (GameEventToUI.Instance.Attack_SuccessEvent())
        {
            GameEventToUI.Instance.OnAttactReset();
        }
    }

    public void B_SkillChargeOne()
    {
        animCount = 0;
        if (skillCharge.transform.localScale.x > 1)
        {
            isCharge = false;
        }
        if (!isCharge)
        {
            b_Kelgon.animator.SetInteger("SetAnim", 6);
            b_Kelgon.bossState = State.BossState.B_SkillOne;
            skillCharge.transform.localScale = new Vector3(0, 0, 0.1f);
            cyclePatton.SetActive(false);
            skillCharge.SetActive(false);
            saveTime = 0;
        }
        else
        {
            cyclePatton.SetActive(true);
            skillCharge.SetActive(true);
            saveTime += Time.deltaTime;
            if (skillCharge.transform.localScale.x < 1.0f)
            {
                
                skillCharge.transform.localScale = new Vector3(0.4f * saveTime, 0.4f * saveTime, 0.1f);
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
        b_Kelgon.animator.SetInteger("SetAnim", 2);
        b_Kelgon.bossState = State.BossState.B_Attack;
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
            b_Kelgon.animator.SetInteger("SetAnim", 7);
            b_Kelgon.bossState = State.BossState.B_SkillTwo;
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
        b_Kelgon.animator.SetInteger("SetAnim", 2);
        b_Kelgon.bossState = State.BossState.B_Attack;
    }

    public void B_SkillChargeThree()
    {

    }

    public void pattonSet()
    {

        if (animCount == 0)
        {
            isCharge = true;
            b_Kelgon.bossState = State.BossState.B_SkillChargeOne;
            b_Kelgon.animator.SetInteger("SetAnim", 3);
        }
        else if (animCount == 1)
        {
            b_Kelgon.bossState = State.BossState.B_Attack;
            b_Kelgon.animator.SetInteger("SetAnim", 2);
        }
        else if (animCount == 2)
        {
            b_Kelgon.bossState = State.BossState.B_AttackTwo;
            b_Kelgon.animator.SetInteger("SetAnim", 11);
        }
        else if (animCount == 3)
        {
            isTwoCharge = true;
            b_Kelgon.bossState = State.BossState.B_SkillChargeTwo;
            b_Kelgon.animator.SetInteger("SetAnim", 4);
        }
    }
}