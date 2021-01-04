using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Dash : StateMachineBehaviour
{
    CharacterController controller;
    float dashSpeed = 30f;
    [Range(1,10)]
    public float dashDistance = 5f;
    Vector3 startPos;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(controller == null)
        controller = animator.transform.parent.GetComponent<CharacterController>();
        GameData.Instance.player.m_state = State.PlayerState.P_Dash;

        startPos = animator.transform.parent.position;
        dashSpeed = 30f;

        GameEventToUI.Instance.OnEventStaminaRestore(STAMINAGAUGE.DECREASE, 15);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector3.Distance(startPos,animator.transform.parent.position) < dashDistance)
        controller.Move(animator.transform.parent.forward * dashSpeed * Time.deltaTime);
        if (dashSpeed > 0)
            dashSpeed -= 0.5f;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameEventToUI.Instance.OnEventStaminaRestore(STAMINAGAUGE.RESTORE);
    }

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
