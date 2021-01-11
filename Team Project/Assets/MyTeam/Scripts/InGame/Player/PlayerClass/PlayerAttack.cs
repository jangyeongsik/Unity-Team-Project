using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject trail;
    Animator animator;
    CharacterController controller;
    PlayerSkill playerSkill;

    bool Attack_Success = false;
    bool isReadyToCounter = false;

    Transform curAttackEnemy = null;

    public float ArrowRadius = 1.5f;

    public GameObject prefab;

    COLORZONE colorZone;

    Color red = new Color(1, 0, 0);
    Color green = new Color(0, 1, 0);
    Color blue = new Color(0, 0, 1);

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();
        if (animator == null)
            animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        UIEventToGame.Instance.playerAttack += playerAttack;
        GameEventToUI.Instance.Attack_SuccessEvent += Attack_SuccessEvent;
        GameEventToUI.Instance.AttactReset += AttackReset;
        GameEventToUI.Instance.Player_Hit += PlayerHit;
        UIEventToGame.Instance.Player_Delay += PlayerDelay;

    }

    private void OnDestroy()
    {
        UIEventToGame.Instance.playerAttack -= playerAttack;
        GameEventToUI.Instance.Attack_SuccessEvent -= Attack_SuccessEvent;
        GameEventToUI.Instance.AttactReset -= AttackReset;
        GameEventToUI.Instance.Player_Hit -= PlayerHit;
        UIEventToGame.Instance.Player_Delay -= PlayerDelay;
    }

    //private void Update()
    //{
    //    if(curAttackEnemy != null)
    //    {
            
    //    }
    //}

    void playerAttack(float time, COLORZONE color)
    {
        colorZone = color;
        switch (GameData.Instance.player.m_state)
        {
            case State.PlayerState.P_Idle:
            case State.PlayerState.P_Run:
                curAttackEnemy = CheckCounterEnemy();
                if (CheckArrow() != null)    //화살쏜애한테 이동
                {
                    curAttackEnemy = CheckArrow();
                    StartCoroutine(MoveToEnemy(curAttackEnemy));
                    animator.SetTrigger("NextSkill");
                    GameEventToUI.Instance.OnSkillGaugeActive(true);
                    Attack_Success = true;
                    //에너미 히트 이벤트
                    EnemyHitEvent();
                }
                else if (curAttackEnemy != null) //근접카운터
                {
                    animator.SetTrigger("NextSkill");
                    GameEventToUI.Instance.OnSkillGaugeActive(true);
                    Attack_Success = true;
                    StartCoroutine(MoveToEnemy(curAttackEnemy));
                    //에너미 히트 이벤트
                    EnemyHitEvent();
                }
                else
                    animator.SetTrigger("Guard");
                
                break;
            case State.PlayerState.P_Dash:
                break;
            case State.PlayerState.P_Guard:
                break;
            case State.PlayerState.P_1st_Skill:
            case State.PlayerState.P_2nd_Skill:
            case State.PlayerState.P_3rd_Skill:

                switch (color)
                {
                    case COLORZONE.NONE://검은색 맞추면 딜레이로 돌입
                        if(CheckEnemys() == null)
                        {
                            animator.CrossFade("Idle", 0.1f);
                            Attack_Success = false;
                            GameEventToUI.Instance.OnSkillGaugeActive(false);
                        }
                        else
                        {
                            GameEventToUI.Instance.OnSkillGaugeActive(false);
                            Attack_Success = false;
                            PlayerDelay();
                        }
                        break;
                    case COLORZONE.GREEN:
                    case COLORZONE.YELLOW:
                    case COLORZONE.RED:// 색깔 맞추면 다음스킬 가까운적 이동
                        //현재 때리던거 죽으면
                        if(curAttackEnemy.GetComponent<Monster>().monsterState == State.MonsterState.M_Dead)
                        {
                            curAttackEnemy = CheckEnemys();
                            if(curAttackEnemy == null) // 공격할거 없으면
                            {
                                animator.CrossFade("Idle", 0.1f);
                                Attack_Success = false;
                                GameEventToUI.Instance.OnSkillGaugeActive(false);
                            }
                            else //다른 공격할게 있다면
                            {
                                Attack_Success = true;
                                animator.SetTrigger("NextSkill");
                                GameEventToUI.Instance.OnSkillGaugeActive(false);
                                GameEventToUI.Instance.OnSkillGaugeActive(true);
                                StartCoroutine(MoveToEnemy(curAttackEnemy));
                                //에너미 히트 이벤트
                                EnemyHitEvent();
                            }
                        }
                        else
                        {
                            StartCoroutine(MoveToEnemy(curAttackEnemy));
                            Attack_Success = true;
                            animator.SetTrigger("NextSkill");
                            GameEventToUI.Instance.OnSkillGaugeActive(false);
                            GameEventToUI.Instance.OnSkillGaugeActive(true);
                            //에너미 히트 이벤트
                            EnemyHitEvent();
                        }
                        
                        break;
                }
                break;
            case State.PlayerState.P_Delay:
                break;
            default:
                break;
        }
    }

  

    public bool Attack_SuccessEvent()
    {
        return Attack_Success;
    }

    //가까운 에너미를 찾자
    Transform CheckEnemys()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 8f, LayerMask.GetMask("Enemy"));
        if (colliders.Length == 0) return null;
        float dist = 0f;
        Transform T = null;
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].GetComponent<Monster>().monsterState == State.MonsterState.M_Dead) continue;

            float d = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (dist == 0 || d < dist)
            {
                dist = d;
                T = colliders[i].transform;
            }
        }
        return T;
    }

    //카운터 근접 검사
    Transform CheckCounterEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 8f, LayerMask.GetMask("Enemy"));
        if (colliders.Length == 0) return null;
        Transform T = null;
        Monster mon = null;
        for(int i = 0; i < colliders.Length; ++i)
        {
            mon = colliders[i].GetComponent<Monster>();
            if (mon.monsterKind == State.MonsterKind.M_Archer) continue;
            if (mon.counterjudgement == false) continue;
            if (mon.monsterState == State.MonsterState.M_Dead) continue;
            T = mon.position;
            break;
        }
        return T;
    }

    //화살이 있는지 찾아보자
    Transform CheckArrow()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up, ArrowRadius, LayerMask.GetMask("Bullet"));
        if (colliders.Length == 0) return null;

        Transform T = colliders[0].gameObject.GetComponent<Arrow>().EnemyTranform;
        float dist = Vector3.Distance(transform.position,T.position);

        for(int i = 1; i < colliders.Length; ++i)
        {
            float d = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (d < dist)
                T = colliders[i].gameObject.GetComponent<Arrow>().EnemyTranform;
        }

        return T;
    }

    // 적한테 이동하자구
    IEnumerator MoveToEnemy(Transform T)
    {
        //트레일 이펙트 켜기
        StartCoroutine(SetTrail());
        Vector3 dir = T.position - transform.position;
        dir.x += Random.Range(-2, 2);
        dir.z += Random.Range(-2, 2);
        dir.y = 0;
        float d = dir.magnitude - 1.5f;
        dir.Normalize();
        GameObject obj = Instantiate(prefab);
        obj.transform.position = transform.position + Vector3.up;
        obj.transform.LookAt(obj.transform.position + dir);
        foreach(Transform tr in obj.transform)
        {
            var main = tr.GetComponent<ParticleSystem>().main;
            switch (colorZone)
            {
                case COLORZONE.GREEN:
                    main.startColor = green;
                    break;
                case COLORZONE.YELLOW:
                    main.startColor = blue;
                    break;
                case COLORZONE.RED:
                    main.startColor = red;
                    break;
            }
        }
        yield return new WaitForEndOfFrame();
        controller.Move(dir * d);
        transform.LookAt(transform.position + dir);

    }

    //트레일 켰다가 1초뒤에 삭제
    IEnumerator SetTrail()
    {
        trail.SetActive(true);
        yield return new WaitForSeconds(1);
        trail.SetActive(false);
    }

    //디버깅
    private void OnDrawGizmos()
    {
        //적인식범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 8);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, ArrowRadius);
    }

    ////화살 충돌상태면 화살카운터 가능으로
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
    //    {
    //        isReadyToCounter = true;
    //        if (Enemy == null)
    //            Enemy = other.gameObject.GetComponent<Arrow>().EnemyTranform;
    //    }
    //}

    public void AttackReset()
    {
        Attack_Success = false;
    }
    //맞음
    void PlayerHit(Transform t, int damage)
    {
        if (GameData.Instance.player.m_state == State.PlayerState.P_Idle ||
            GameData.Instance.player.m_state == State.PlayerState.P_Run ||
            GameData.Instance.player.m_state == State.PlayerState.P_Delay)
        {
            Vector3 dir = t.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            transform.LookAt(transform.position + dir);

            animator.CrossFade("Hit", 0.1f);

            GameEventToUI.Instance.OnPlayerHp_Decrease(damage);
        }
        else if (GameData.Instance.player.m_state == State.PlayerState.P_Guard)
        {
            Vector3 dir = t.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            transform.LookAt(transform.position + dir);

            animator.CrossFade("Guard Hit", 0.1f);
        }

    }

    //딜레이
    void PlayerDelay()
    {
        if (CheckEnemys() == null)
        {
            animator.CrossFade("Idle", 0.1f);
            GameEventToUI.Instance.OnSkillGaugeActive(false);
            Attack_Success = false;
            return;
        }
        animator.CrossFade("Delay", 0.17f);
    }

    //트랜스폼 방향으로 바라봐랑
    void LookEnemy(Transform t)
    {
        Vector3 dir = t.position - transform.position;
        dir.y = 0;
        dir.Normalize();
        transform.LookAt(transform.position + dir);
    }

    //현재 공격중인 거 체력 깎아버리기
    void EnemyHitEvent()
    {
        if (curAttackEnemy == null) return;
        {


            //switch (curAttackEnemy.tag)
            //{
            //    case "EnemyWarrior":
            //        curAttackEnemy.GetComponent<EnemyWarrior>().AttackHit();
            //        break;
            //    case "EnemyArcher":
            //        curAttackEnemy.GetComponent<EnemyArcher>().OnDeadEvent();
            //        break;
            //    case "EnemyViper":
            //        curAttackEnemy.GetComponent<ViperFSM>().AttackHit();
            //        break;
            //    case "EnemyWolf":
            //        curAttackEnemy.GetComponent<EnemyWolf>().AttackHit();
            //        break;
            //}
            curAttackEnemy.GetComponent<Monster>().OnEnemyHitEvent();
            

        }
    }
}