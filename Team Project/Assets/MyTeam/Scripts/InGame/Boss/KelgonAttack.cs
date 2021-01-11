using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelgonAttack : MonoBehaviour
{
    BossData kelgon;
    Monster monster;

    public GameObject attackNotice;

    private void Start()
    {
        kelgon = transform.parent.GetComponent<BossData>();
        monster = transform.parent.GetComponent<Monster>();
    }

    public void playerAttack()
    {
        kelgon.navigation.speed = 3.5f;
        switch (kelgon.bossState)
        {
            case State.BossState.B_Attack:
                break;
            case State.BossState.B_SkillChargeOne:
                Charge1();
                break;
            case State.BossState.B_SkillChargeTwo:
                break;
            case State.BossState.B_SkillChargeThree:
                break;
            case State.BossState.B_AttackTwo:
                break;
        }

        Vector3 dir = kelgon.target.position - transform.parent.position;
        //가까우면 공격 멀면 3번 애매하면 걷기
        if (dir.magnitude < 3.5f)
        {
            //현재 공격에 따라 다음 공격 하기
            switch (kelgon.bossState)
            {
                case State.BossState.B_Attack:
                    kelgon.animator.SetInteger("Attack", 2);
                    break;
                case State.BossState.B_SkillChargeOne:
                    kelgon.animator.SetInteger("Attack", 1);
                    break;
                case State.BossState.B_SkillChargeTwo:
                    kelgon.animator.SetInteger("Attack", 1);
                    break;
                case State.BossState.B_SkillChargeThree:
                    kelgon.animator.SetInteger("Charge", 1);
                    break;
                case State.BossState.B_AttackTwo:
                    {
                        int count = kelgon.animator.GetInteger("Charge");
                        if (count >= 2)
                            count = 0;
                        ++count;
                        kelgon.animator.SetInteger("Charge", count);
                    }
                    break;
            }
        }
        else if (dir.magnitude > 9)
        {
            kelgon.animator.SetInteger("Charge", 3);
            kelgon.bossState = State.BossState.B_SkillChargeThree;
            kelgon.navigation.SetDestination(kelgon.position.position);

            kelgon.animator.SetTrigger("Walk");
            kelgon.animator.SetInteger("Attack", 0);
        }
        else
        {
            kelgon.animator.SetTrigger("Walk");
            kelgon.animator.SetInteger("Attack", 0);
            //몇번차지였는지 저장한다
            kelgon.chargeNum = kelgon.animator.GetInteger("Charge");
            kelgon.animator.SetInteger("Charge", 0);
            kelgon.lastAttack = kelgon.bossState;
        }
    }

    void Charge1()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.parent.position, 8, LayerMask.GetMask("Player"));
        if(colliders.Length >= 1)
        {
            //GameEventToUI.Instance.OnPlayerHit(transform, 1);
        }
    }

    void Charge2()
    {
        Collider[] colliders = Physics.OverlapBox(transform.parent.position + new Vector3(0, 0, 1.2f), new Vector3(9, 3, 5), Quaternion.identity, LayerMask.GetMask("Player"));
        if (colliders.Length >= 1)
        {
            //GameEventToUI.Instance.OnPlayerHit(transform, 1);
        }
    }

    void Charge3()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.parent.position, 8, LayerMask.GetMask("Player"));
        if (colliders.Length >= 1)
        {
            //GameEventToUI.Instance.OnPlayerHit(transform, 1);
        }
    }

    void Attack1()
    {

    }

    void AttackNoticeActive()
    {
        attackNotice.SetActive(true);
        monster.counterjudgement = true;
        
    }

    void AttackNoticeDeActive()
    {
        attackNotice.SetActive(false);
        monster.counterjudgement = false;
    }
}
