using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosPattern2_3 : StateMachineBehaviour
{
    BossData tartos;
    BossTartos bossTartos;

    GameObject patton2_7;
    GameObject patton2_8;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (tartos == null)
            tartos = animator.transform.parent.GetComponent<BossData>();
        tartos.bossState = State.BossState.B_SkillChargeTwo;

        if (bossTartos == null)
            bossTartos = animator.transform.parent.GetComponent<BossTartos>();


        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Chapter2_Boss, "Pattern2");

        tartos.position.position = bossTartos.pattern2Point.position;
        tartos.navigation.SetDestination(bossTartos.pattern2Point.position);

        if (patton2_7 == null)
            patton2_7 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_7;
        if (patton2_8 == null)
            patton2_8 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_8;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tartos.position.position = bossTartos.pattern2Point.position;
        tartos.transform.rotation = Quaternion.Euler(0, -90, 0);

        patton2_7.SetActive(true);
        patton2_8.SetActive(true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossTartos.PaticleOn2_3_3();
        patton2_7.SetActive(false);
        patton2_8.SetActive(false);
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
