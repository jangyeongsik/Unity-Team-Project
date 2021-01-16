using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Dash : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameData.Instance.player.m_state = State.PlayerState.P_Dash;
        GameEventToUI.Instance.OnEventStaminaRestore(STAMINAGAUGE.DECREASE, 15);
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_PlayerMove, "Dash");
        GameEventToUI.Instance.OnSkillGaugeActive(false);
        GameEventToUI.Instance.OnEventStaminaRestore(STAMINAGAUGE.RESTORE);
    }

}