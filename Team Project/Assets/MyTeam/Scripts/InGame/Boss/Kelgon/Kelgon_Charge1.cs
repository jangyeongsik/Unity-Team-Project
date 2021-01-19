using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kelgon_Charge1 : StateMachineBehaviour
{
    BossData kelgon;
    GameObject circle;
    GameObject paticle;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (kelgon == null)
            kelgon = animator.transform.parent.GetComponent<BossData>();
        kelgon.bossState = State.BossState.B_SkillChargeOne;

        if (circle == null)
            circle = animator.transform.parent.GetComponent<BossKelgon>().ChargeCircle1;
        circle.SetActive(true);
        circle.transform.position = kelgon.position.position;

        if (paticle == null)
            paticle = animator.transform.parent.GetComponent<BossKelgon>().ChargeCircle1_1;
        paticle.SetActive(false);

        Vector3 dir = kelgon.target.position - kelgon.position.position;
        dir.y = 0;
        kelgon.position.LookAt(kelgon.position.position + dir);

        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Chapter1_Boss, "Pattern1");
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
