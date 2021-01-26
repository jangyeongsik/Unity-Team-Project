using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 *  InGame스크립트 함수를 이벤트에 넣어야 하는것들을 여기다 추가합니다 (영식이 이해용)
 *  UI에서 이벤트를 호출!
 *  
 *   if (playerAttack != null)
            playerAttack(time, color);
      이거랑
      playerAttack?.Invoke(); 
      이거랑 같은거래요
 */

public class UIEventToGame : Singleton<UIEventToGame>
{

    #region"튜토리얼 퀘스트"
    public event System.Action<string> Quest_Name;
    public event System.Action<string> Quest_Count;

    public void OnUiEventQuest_Name(string name)
    {

        Quest_Name(name);
    }

    public void OnUiEventQuest_Count(string count)
    {

        Quest_Count(count);
    }
    #endregion

    #region"플레이어 조이스틱"
    public event System.Action<Vector2, float> PlayerMove;
    public event System.Action<bool> PlayerDash;


    public event System.Action minMap;

    public void OnPlayerMove(Vector2 direction, float amount)
    {
        if (PlayerMove != null)
            PlayerMove(direction, amount);
    }

    public void OnPlayerDash(bool isDash)
    {
        if (PlayerDash != null)
            PlayerDash(isDash);
    }
    #endregion

    #region "플레이어 스킬버튼"

    public System.Action<float, COLORZONE> playerAttack;

    public void OnPlayerAttack(float time, COLORZONE color)
    {
        playerAttack?.Invoke(time, color);
    }

    #endregion

    #region "플레이어 딜레이"
    public System.Action Player_Delay;
    public void OnPlayerDelay()
    {
        Player_Delay?.Invoke();
    }
    #endregion

    public delegate int SkillId();
    public SkillId skillId;

    //텔레포터 isActivated로 변경
    public event System.Action<bool> TPActivate;
    //텔레포터 작동 안함 메시지 보냄
    public event System.Action<bool> CancelActivate;
    public event System.Action<int> SwordChangeEvent;
    public event System.Action<bool> ActivateVaccineCampPortal;
    public event System.Action<bool> ActivateTemplePortal;
    public event System.Action<bool> ActivateTemple;
    public event System.Action<string, string> SkillName;
    
    public event System.Action JoystickSetting;
    public void OnTPActivate(bool isOn)
    {
        TPActivate(isOn);
    }
    public void OnCancel(bool isOn)
    {
        CancelActivate(isOn);
    }

    public int UIeventSkillId()
    {
        return skillId();
    }
    public void OnSwordChangeEvent(int index)
    {
        SwordChangeEvent(index);
    }
    public void OnActivateVaccineCampPortal(bool isOn)
    {
        ActivateVaccineCampPortal(isOn);
    }
    public void OnActivateTemplePortal(bool isOn)
    {
        ActivateTemplePortal(isOn);
    }
    public void OnActivateTemple(bool isActivated)
    {
        ActivateTemple(isActivated);
    }

    public void OnUiEventJoystickSetting()
    {
        JoystickSetting();
    }

    public void OnUIEventMinMap()
    {
        minMap();
    }

    public void OnUIEventSkillName(string skillName, string skillCylinderCount)
    {
        SkillName(skillName, skillCylinderCount);
    }
}
