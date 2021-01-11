﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kelgon_Charge2 : StateMachineBehaviour
{
    BossData kelgon;
    GameObject circle;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (kelgon == null)
            kelgon = animator.transform.parent.GetComponent<BossData>();
        kelgon.bossState = State.BossState.B_SkillChargeTwo;

        if (circle == null)
            circle = animator.transform.parent.GetComponent<BossKelgon>().ChargeCircle2;
        circle.SetActive(true);
        circle.transform.position = kelgon.position.position;
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
