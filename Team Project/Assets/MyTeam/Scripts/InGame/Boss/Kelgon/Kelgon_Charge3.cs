using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kelgon_Charge3 : StateMachineBehaviour
{
    BossData kelgon;
    BossKelgon bossKelgon;
    GameObject circle;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (kelgon == null)
            kelgon = animator.transform.parent.GetComponent<BossData>();
        kelgon.bossState = State.BossState.B_SkillChargeThree;

        if (circle == null)
            circle = animator.transform.parent.GetComponent<BossKelgon>().ChargeCircle3;
        circle.SetActive(true);

        circle.transform.position = kelgon.target.position;


        if (bossKelgon == null)
            bossKelgon = animator.transform.parent.GetComponent<BossKelgon>();
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Chapter1_Boss, "EnCounter");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        kelgon.navigation.SetDestination(circle.transform.position);
        float dist = Vector3.Distance(kelgon.position.position, circle.transform.position);
        kelgon.navigation.speed = dist;
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
