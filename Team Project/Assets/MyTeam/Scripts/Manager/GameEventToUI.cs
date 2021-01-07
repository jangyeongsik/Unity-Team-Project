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

    #region "플레이어 에너미한테 피격"
    public event System.Action<Transform, int> Player_Hit;

    public void OnPlayerHit(Transform t, int damage)
    {
        if (Player_Hit != null)
            Player_Hit(t, damage);
    }
    #endregion

    #region "실린더 게이지"
    public event System.Action<int> PlayerCylinderGauge;
    public void OnPlayerCylinderGauge(int value)
    {
        PlayerCylinderGauge?.Invoke(value);
    }
    #endregion
    public delegate KeyValuePair<bool, Transform> AttackEnvent();
    public AttackEnvent Player_Attack;

    public delegate bool Attack_success();
    public Attack_success Attack_SuccessEvent;

    public delegate void PlayerHP_Decrease(int damage);
    public PlayerHP_Decrease playerHP_Decrease;

    public event System.Action AttactReset;

    public event System.Action<bool> onOff;

    public event System.Action<bool, int, string> talk;
    public event System.Action<bool> interOnOff;
    public event System.Action<bool> TPOpearteOnOff;
    public event System.Action<bool> TPCanvasOnOff;

    public void OnEventShopOnOff(bool isOn)
    {
        onOff(isOn);
    }

    public void OnEventTalkOnOff(bool isOn, int id, string npcName)
    {
        talk(isOn, id, npcName);
    }

    public KeyValuePair<bool, Transform> OnPlayer_AttackEvent()
    {
        return Player_Attack.Invoke();
    }

    public bool OnAttack_SuccessEvent()
    {
        return Attack_SuccessEvent.Invoke();
    }

    public void OnAttactReset()
    {
        AttactReset.Invoke();
    }
    public void OnPlayerHp_Decrease(int damage)
    {
        if (playerHP_Decrease != null)
            playerHP_Decrease(damage);
    }
    public void OnEventInterActionOnOff(bool isOn)
    {
        interOnOff(isOn);
    }
    public void OnEventTPOpearteOnOff(bool isOn)
    {
        TPOpearteOnOff(isOn);
    }
    public void OnEventTPCanvasOnOff(bool isOn)
    {
        TPCanvasOnOff(isOn);
    }
}
