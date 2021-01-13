﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosCharge02 : StateMachineBehaviour
{
    BossData tartos;

    GameObject patton1_1;
    GameObject patton1_2;
    GameObject patton1_4;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (tartos == null)
            tartos = animator.transform.parent.GetComponent<BossData>();
        tartos.bossState = State.BossState.B_SkillChargeOne;

        if (patton1_1 == null)
            patton1_1 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton1_1;
        patton1_1.SetActive(false);

        if (patton1_2 == null)
            patton1_2 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton1_2;
        patton1_2.SetActive(true);

        if (patton1_4 == null)
            patton1_4 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton1_4;
        patton1_4.SetActive(true);

        Vector3 dir = tartos.target.position - tartos.position.position;
        dir.y = 0;
        tartos.position.LookAt(tartos.position.position + dir);
    }
}