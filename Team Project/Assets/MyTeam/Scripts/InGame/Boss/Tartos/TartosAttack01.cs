using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosAttack01 : StateMachineBehaviour
{
    BossData tartos;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (tartos == null)
            tartos = animator.transform.parent.GetComponent<BossData>();
        tartos.bossState = State.BossState.B_Attack;

        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_Chapter2_Boss, "Attack1");

        Vector3 dir = tartos.target.position - tartos.position.position;
        dir.y = 0;
        tartos.position.LookAt(tartos.position.position + dir);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}
}
