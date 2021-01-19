using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosCharge01 : StateMachineBehaviour
{
    BossData tartos;

    GameObject patton1_3;
    BossTartos paticle1_1;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (tartos == null)
            tartos = animator.transform.parent.GetComponent<BossData>();
        tartos.bossState = State.BossState.B_SkillChargeOne;

        if (patton1_3 == null)
            patton1_3 = animator.transform.parent.GetComponent<BossTartos>().tartosPatton1_3;
        patton1_3.SetActive(true);

        if (paticle1_1 == null)
            paticle1_1 = animator.transform.parent.GetComponent<BossTartos>();

        paticle1_1.PaticleOff1_1();
        paticle1_1.PaticleOff1_2();
        paticle1_1.PaticleOff1_3();

        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Chapter2_Boss, "Pattern1");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        paticle1_1.PaticleOn1_1();
    }

}
