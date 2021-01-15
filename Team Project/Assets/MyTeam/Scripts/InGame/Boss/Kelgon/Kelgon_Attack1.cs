using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kelgon_Attack1 : StateMachineBehaviour
{
    BossData kelgon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (kelgon == null)
            kelgon = animator.transform.parent.GetComponent<BossData>();
        kelgon.bossState = State.BossState.B_Attack;

        Vector3 dir = kelgon.target.position - kelgon.position.position;
        dir.y = 0;
        kelgon.position.LookAt(kelgon.position.position + dir);

        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Chapter1_Boss, "Attack1");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
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
