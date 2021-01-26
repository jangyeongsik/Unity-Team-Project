using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosPattern2_5 : StateMachineBehaviour
{
    BossData tartos;
    BossTartos bossTartos;


    GameObject patton2_1;
    GameObject patton2_2;
    GameObject patton2_3;
    GameObject patton2_4;
    GameObject patton2_5;
    GameObject patton2_6;
    GameObject patton2_7;
    GameObject patton2_8;
    GameObject patton2_C;


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
        tartos.transform.rotation = Quaternion.Euler(0, -90, 0);
        tartos.navigation.SetDestination(bossTartos.pattern2Point.position);



        if (patton2_1 == null)
            patton2_1 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_1;
        if (patton2_2 == null)
            patton2_2 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_2;
        if (patton2_3 == null)
            patton2_3 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_3;
        if (patton2_4 == null)
            patton2_4 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_4;
        if (patton2_5 == null)
            patton2_5 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_5;
        if (patton2_6 == null)
            patton2_6 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_6;
        if (patton2_7 == null)
            patton2_7 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_7;
        if (patton2_8 == null)
            patton2_8 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_8;
        if (patton2_C == null)
            patton2_C = animator.transform.parent.GetComponent<BossTartos>().tartosPatton2_Center;


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tartos.position.position = bossTartos.pattern2Point.position;
        tartos.transform.rotation = Quaternion.Euler(0, -90, 0);

        patton2_1.SetActive(true);
        patton2_2.SetActive(true);
        patton2_3.SetActive(true);
        patton2_4.SetActive(true);
        patton2_5.SetActive(true);
        patton2_6.SetActive(true);
        patton2_7.SetActive(true);

        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossTartos.PaticleOff2_3_1();

        bossTartos.PaticleOn2_3_5();

        patton2_1.SetActive(false);
        patton2_2.SetActive(false);
        patton2_3.SetActive(false);
        patton2_4.SetActive(false);
        patton2_5.SetActive(false);
        patton2_6.SetActive(false);
        patton2_7.SetActive(false);
        patton2_C.SetActive(false);
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
