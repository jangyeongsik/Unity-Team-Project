using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  UI스크립트 함수를 이벤트에 넣어야 하는것들을 여기다 추가합니다 (영식이 이해용)\
 *  InGame에서 이벤트를 호출!
 */


public class GameEventToUI : Singleton<GameEventToUI>
{

    #region "스테미나 게이지"
    public event System.Action<STAMINAGAUGE, float> staminaRestore;

    public void OnEventStaminaRestore(STAMINAGAUGE state, float percent = 0)
    {
        if (staminaRestore != null)
            staminaRestore(state, percent);
    }
    #endregion

    #region "스킬게이지"

    public System.Action<bool> skillGaugeActive;

    public void OnSkillGaugeActive(bool isOn)
    {
        if (skillGaugeActive != null)
            skillGaugeActive(isOn);
    }


    #endregion
    public delegate bool AttackEnvent();
    public AttackEnvent Player_Attack;

    public delegate bool Attack_success();
    public Attack_success Attack_SuccessEvent;

    public delegate void PlayerHP_Decrease(int damage);
    public PlayerHP_Decrease playerHP_Decrease;


    public event System.Action<bool> onOff;
    public event System.Action<bool, int, string> talk;


    public void OnEventShopOnOff(bool isOn)
    {
        onOff(isOn);
    }


    public void OnEventTalkOnOff(bool isOn, int id, string npcName)
    {
        talk(isOn, id, npcName);
    }


    public bool OnPlayer_AttackEvent()
    {
        return Player_Attack.Invoke();
    }

    public bool OnAttack_SuccessEvent()
    {
        return Attack_SuccessEvent.Invoke();
    }

    public void OnPlayerHp_Decrease(int damage)
    {
        if (playerHP_Decrease != null)
            playerHP_Decrease(damage);
    }

}
