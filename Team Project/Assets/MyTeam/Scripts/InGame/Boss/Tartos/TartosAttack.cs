using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosAttack : MonoBehaviour
{
    BossTartos BossTartos;
    BossData tartos;
    Monster monster;

    // Start is called before the first frame update
    void Start()
    {
        BossTartos = transform.parent.GetComponent<BossTartos>();
        tartos = transform.parent.GetComponent<BossData>();
        monster = transform.parent.GetComponent<Monster>();
    }

    public void playerAttack()
    {
        tartos.navigation.speed = 3.5f;
        switch (tartos.bossState)
        {
            case State.BossState.B_Attack:
                Attack1();
                break;
            case State.BossState.B_SkillChargeOne:
                break;
            case State.BossState.B_SkillChargeTwo:
                break;
            case State.BossState.B_AttackTwo:
                Attack1();
                break;
        }

        Vector3 dir = tartos.target.position - transform.parent.position;

        if (dir.magnitude < 3.5f)
        {
            switch (tartos.bossState)
            {          
                case State.BossState.B_Attack:
                    tartos.animator.SetInteger("Attack", 2);
                    break;
                case State.BossState.B_SkillChargeOne:
                    tartos.animator.SetInteger("Attack", 1);
                    break;
                case State.BossState.B_SkillChargeTwo:
                    tartos.animator.SetInteger("Attack", 1);
                    break;         
                case State.BossState.B_AttackTwo:
                    {
                        int count = tartos.animator.GetInteger("Charge");
                        if (count >= 2) count = 0;

                        ++count;
                        tartos.animator.SetInteger("Charge", count);
                    }
                    break;
            }
        }
        else
        {
            tartos.animator.SetTrigger("Move");
            tartos.animator.SetInteger("Attack", 0);

            tartos.chargeNum = tartos.animator.GetInteger("Charge");
            tartos.animator.SetInteger("Charge", 0);
            tartos.lastAttack = tartos.bossState;
        }
    }


    void Attack1()
    {
        Vector3 dir = transform.parent.position - tartos.target.position;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (angle < 180 && angle > 0 && dir.magnitude < 3.5f)
        {
            tartos.PlayerHit();
        }
        BossTartos.AttackNotice.SetActive(false);
        monster.counterjudgement = false;
    }

    void AttackNoticeActive()
    {
        BossTartos.AttackNotice.SetActive(true);
        monster.counterjudgement = true;
    }
}
