using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  UI스크립트 함수를 이벤트에 넣어야 하는것들을 여기다 추가합니다 (영식이 이해용)\
 *  InGame에서 이벤트를 호출!
 */


public class GameEventToUI : Singleton<GameEventToUI>
{
    #region "튜토리얼 카운트 판정 카운트"
    public System.Action TutoAttack;

    public void OnEventTutoAttack()
    {
        TutoAttack();
    }
    #endregion

    #region "스테미나 게이지"
    public event System.Action<STAMINAGAUGE, float> staminaRestore;
    public event System.Action<int> AddStamina;

    public void OnEventStaminaRestore(STAMINAGAUGE state, float percent = 0)
    {
        if (staminaRestore != null)
            staminaRestore(state, percent);
    }
    public void OnAddStamina(int add)
    {
        AddStamina?.Invoke(add);
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
    public event System.Action<Transform, int, State.BossState> Player_Boss_Hit;

    public void OnPlayerHit(Transform t, int damage)
    {
        if (Player_Hit != null)
            Player_Hit(t, damage);
    }
    public void OnPlayerBossHit(Transform t, int damage, State.BossState state)
    {
        Player_Boss_Hit?.Invoke(t, damage, state);
    }
    #endregion

    #region "실린더 게이지"
    public event System.Action<int> PlayerCylinderGauge;
    public void OnPlayerCylinderGauge(int value)
    {
        PlayerCylinderGauge?.Invoke(value);
    }
    #endregion

    #region "몬스터 키 드랍"
    public event System.Action<int> keyCount;
    public event System.Action<int> LoseTempleKeys;
    #endregion

    #region "드롭아이템 멘트박스"
    public event System.Action isGet;
    #endregion 

    #region "선택형 텔레포터 팝업"
    public event System.Action<bool, string> SelectiveTeleport;
    public void ActivateSelectiveTeleporter(bool isOn, string curSceneName)
    {
        SelectiveTeleport?.Invoke(isOn, curSceneName);
    }
    #endregion

    #region "아이템 드랍"
    //백신 야영지
    public event System.Action<bool,int> ItemDropInfo;
    //신전
    public event System.Action<bool, int> TempleItemDropInfo;
    public void OnItemDropInfo(bool value, int str)
    {
        ItemDropInfo?.Invoke(value, str);
    }
    public void OnTempleItemDropInfo(bool val, int str)
    {
        TempleItemDropInfo?.Invoke(val, str);
    }
    #endregion

    #region "플레이어 최대체력 증가"
    public event System.Action<int> AddMaxHp;
    public void OnAddMaxHp(int add)
    {
        AddMaxHp?.Invoke(add);
    }
    #endregion

    #region"게임오버"
    public event System.Action gameover;
    public void OnGameOver()
    {
        gameover?.Invoke();
    }
    #endregion

    public delegate KeyValuePair<bool, Transform> AttackEnvent();
    public AttackEnvent Player_Attack;

    public delegate bool Attack_success();
    public Attack_success Attack_SuccessEvent;

    public delegate void PlayerHP_Decrease(int damage);
    public PlayerHP_Decrease playerHP_Decrease;

    public delegate int Talk_Box();
    public Talk_Box talk_box;

    public event System.Action<int,int> playerHP_Increase;
    public event System.Action AttactReset;

    public event System.Action<bool> onOff;

    public event System.Action talk;
    public event System.Action<bool> interOnOff;
    public event System.Action<bool> TPOpearteOnOff;
    public event System.Action<bool> TPCanvasOnOff;
    public event System.Action<bool, string, string> leverOnOff;
    public event System.Action<bool, bool, string> templeOnOff;
    public event System.Action<bool> talkButOnOff;
    public event System.Action<bool> dodbogiImgOnOff;
    public event System.Action talkOnOff;
    public event System.Action skillShop;

    public event System.Action talkBtnEvent;

    public event System.Action joystick_on;
    public event System.Action joystick_off;


    public event System.Action<int> Event_TalkBox;


    public delegate bool Player_Trigger();
    public Player_Trigger player_Trigger;
    #region 대화 출력
    public event System.Action<string> npc_name_chage;
    public event System.Action<int> npc_name_setting;
    public event System.Action npc_name_print;

    public event System.Action<string> npc_talk_chage;
    public event System.Action<int> npc_talk_setting;
    public event System.Action npc_talk_print;


    public event System.Action npc_talk_Next;

    public event System.Action skillShopPush;
    public event System.Action skillShopback;

    public void OnNpc_name_Setting(int data)
    {
        npc_name_setting(data);
    }
    public void OnNpc_name_print()
    {
        npc_name_print();
    }
    public void Onnpc_talk_setting(int data)
    {
        npc_talk_setting(data);
    }
    public void Onnpc_talk_print()
    {
        npc_talk_print();
    }
    public void Onnpc_talk_Next()
    {
        npc_talk_Next();
    }

    public void OnEventTalkoff()
    {
        talkOnOff();
    }
    #endregion
    public void OnLoseTempleKeys(int count)
    {
        LoseTempleKeys(count);
    }

    public void OnEventJoystick()
    {
        joystick_on();
    }
    public void OnEventJoystickOff()
    {
        joystick_off();
    }
    public void OnEventShopOnOff(bool isOn)
    {
        onOff(isOn);
    }

    public void OnEventTalkOnOff()
    {
        talk();
    }

    public void OnEventTalkBtn(bool isOn)
    {
        talkButOnOff(isOn);
    }

    public void OnEventDodbogi(bool isOn)
    {
        dodbogiImgOnOff(isOn);
    }

    public void OnEvent_TalkBox(int id) {
        Event_TalkBox(id);

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
    public void OnPlayerHp_Increase(int value,int per)
    {
        playerHP_Increase.Invoke(value,per);
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
    public bool onEventPlayer_Trigger()
    {
        return player_Trigger.Invoke();
    }

    public void OnEventMonsterDrop(int count)
    {
        keyCount(count);
    }

    public void OnEventDropItemMentBoxOnOff()
    {
        isGet();
    }

    public int OnEventTalk_id()
    {
        return talk_box.Invoke();
    }

    public void OnTalkBtnEvent()
    {
        talkBtnEvent();
    }

    public void onEventSkillShop()
    {
        skillShop();
    }

    public void OnEventSkillShopPush()
    {
        skillShopPush();
    }

    public void onEventSkillShopback()
    {
        skillShopback();
    }

    #region 레버 충돌시 팝업창 출현
    public void OnLeverPopup(bool isOn, string name = " ", string description = " ")
    {
        leverOnOff(isOn, name, description);
    }
    #endregion
    #region 신전 충돌시 팝업창 출현
    public void OnTemplePopup(bool isOn, bool isActivated = false, string name = " ")
    {
        templeOnOff(isOn, isActivated, name);
    }
    #endregion
}
