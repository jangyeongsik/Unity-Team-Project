using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoMon_Short : MonoBehaviour
{
    private Monster skull;
    private bool targeting = false;

    private GameObject target;
    Vector3 offset;

    public GameObject AttackNotice; //그 에너미용 느낌표 

    float attackTime;
    float attackCountMin = 0.2f;
    float attackCountMax = 0.6f;

    private int count;

    bool isAppear = false;          
    bool isRunning = false;         

    private bool dead = false;

    private void Start()
    {
        skull = GetComponent<Monster>();

        target = GameData.Instance.player.position.gameObject;

        skull.EnemyHitEvent += AttackHit;

        KingSetting();
    }

    private void KingSetting()
    {
        skull.position = transform;
        skull.monsterKind = State.MonsterKind.M_Warrier;
        skull.monsterState = State.MonsterState.M_Idle;
        skull.animator = GetComponent<Animator>();
        skull.rigid = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        skull.EnemyHitEvent -= AttackHit;
    }

    private void Update()
    {
        switch (skull.monsterState)
        {
            case State.MonsterState.M_None:
                break;
            case State.MonsterState.M_Idle:
                KingMove();
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
                break;
            case State.MonsterState.M_Dash:
                break;
            default:
                break;
        }
        
        if (skull.monsterState != State.MonsterState.M_Idle)
        {
            skull.counterjudgement = false;
        }
    }

    //가만히 있다가 거리되면 지 자리에서 공격함. 
    private void KingMove()
    {
        AttackNotice.SetActive(skull.counterjudgement);
        if (skull.DistacneWithTarget() < 2.5f)
        {
            skull.animator.SetBool("IsAttack", true);
        }
        else
        {
            skull.animator.SetBool("IsAttack", false);
            //skullKing.counterjudgement = false;
            //AttackNotice.SetActive(false);
        }
    }

    //거의 대부분의 에너미에 공통적으로 들어가는 메서드. 
    public void OnTargetingEvent()
    {
        targeting = true;
    }

    public void ExitHit()
    {
        if (skull.monsterState == State.MonsterState.M_Damage)
        {
            skull.monsterState = State.MonsterState.M_Idle;
        }
    }

    public void AttackHit()
    {
        //count++;
        //skullKing.animator.SetBool("isWalk", false);
        skull.animator.SetBool("IsAttack", false);
        skull.animator.SetTrigger("Hit");
        //skullKing.monsterState = State.MonsterState.M_Damage;
        if (GameEventToUI.Instance.OnAttack_SuccessEvent())
        {
            GameEventToUI.Instance.OnAttactReset();
        }
    }
}
