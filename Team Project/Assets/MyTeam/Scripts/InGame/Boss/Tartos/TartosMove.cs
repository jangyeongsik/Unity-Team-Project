using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosMove : StateMachineBehaviour
{
    BossData tartos;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (tartos == null)
            tartos = animator.transform.parent.GetComponent<BossData>();

        tartos.bossState = State.BossState.B_Move;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (tartos.navigation.destination == null)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            if (tartos.bossState != State.BossState.B_Move) return;

            Vector3 dir = tartos.target.position - tartos.position.position;
            dir.y = 0;
            tartos.position.LookAt(tartos.position.position + dir);
            tartos.navigation.SetDestination(tartos.target.position);

            if (dir.magnitude < 3f)
            {
                tartos.navigation.SetDestination(tartos.position.position);
                switch (tartos.lastAttack)
                {
                    case State.BossState.B_Attack:
                        animator.SetInteger("Attack", 2);
                        break;
                    case State.BossState.B_SkillChargeOne:
                        animator.SetInteger("Attack", 1);
                        break;
                    case State.BossState.B_SkillChargeTwo:
                        animator.SetInteger("Attack", 1);
                        break;  
                    case State.BossState.B_AttackTwo:
                        {
                            int count = tartos.chargeNum;
                            if (count >= 2) count = 0;

                            ++count;
                            tartos.animator.SetInteger("Charge", count);
                        }
                        break;
                    default:
                        {
                            int count = animator.GetInteger("Charge");
                            if (count >= 2) count = 0;

                            ++count;
                            animator.SetInteger("Charge", count);
                            tartos.bossState = State.BossState.B_SkillChargeOne;
                        }
                        break;
                }

            }

        }

    }

}
