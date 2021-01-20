using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kelgon_Walk : StateMachineBehaviour
{
    BossData kelgon;
    public float jumpDistance = 9f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (kelgon == null)
            kelgon = animator.transform.parent.GetComponent<BossData>();
        kelgon.bossState = State.BossState.B_Move;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(kelgon.navigation.destination == null)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            //끝나고 한바퀴 더도는거 예외처리
            if (kelgon.bossState != State.BossState.B_Move) return;
            //방향 타겟쪽으로
            Vector3 dir = kelgon.target.position - kelgon.position.position;
            dir.y = 0;
            kelgon.position.LookAt(kelgon.position.position + dir);
            kelgon.navigation.SetDestination(kelgon.target.position);

            //거리가 멀면 덮쳐!
            if(dir.magnitude > jumpDistance)
            {
                animator.SetInteger("Charge", 3);
                kelgon.bossState = State.BossState.B_SkillChargeThree;
                kelgon.navigation.SetDestination(kelgon.position.position);
            }

            //거리가 가까워지면 공격을 실행하거라
            else if (dir.magnitude < 3f )
            {
                kelgon.navigation.SetDestination(kelgon.position.position);
                switch (kelgon.lastAttack)
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
                    case State.BossState.B_SkillChargeThree:
                        animator.SetInteger("Charge", 1);
                        break;
                    case State.BossState.B_AttackTwo:
                        {
                            int count = kelgon.chargeNum;
                            if (count >= 2)
                                count = 0;

                            ++count;
                            kelgon.animator.SetInteger("Charge", count);
                        }
                        break;
                    default:
                        {
                            int count = animator.GetInteger("Charge");
                            if (count >= 2)
                                count = 0;

                            ++count;
                            animator.SetInteger("Charge", count);
                            kelgon.bossState = State.BossState.B_SkillChargeOne;
                        }
                        break;
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
