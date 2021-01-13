using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosIdle : StateMachineBehaviour
{
    BossData tartos;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        if (tartos == null)
            tartos = animator.transform.parent.GetComponent<BossData>();

        tartos.bossState = State.BossState.B_Idle;
   }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (tartos.target != null && tartos.bossState == State.BossState.B_Idle)
        {
            tartos.bossState = State.BossState.B_Move;
            tartos.navigation.SetDestination(tartos.target.position);
            animator.SetTrigger("Move");
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
