using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosCharge02 : StateMachineBehaviour
{
    BossData tartos;

    GameObject patton1_1;
    GameObject patton1_2;
    GameObject patton1_4;

    BossTartos paticle1_2;
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

        if (paticle1_2 == null)
            paticle1_2 = animator.transform.parent.GetComponent<BossTartos>();

        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Chapter2_Boss, "Pattern1");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        paticle1_2.PaticleOn1_2();
        paticle1_2.PaticleOff1_1();
    }

}
