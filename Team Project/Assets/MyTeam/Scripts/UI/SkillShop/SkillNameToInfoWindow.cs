using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNameToInfoWindow : MonoBehaviour
{
    public int skillId;
    bool buySucced;

    public GameObject SucceedWindow;
    public GameObject FailWinodw;

    private void Start()
    {
        SucceedWindow.SetActive(false);
        FailWinodw.SetActive(false);
    }
    public void FindSkillInfo()
    {
        switch (skillId)
        {
            case 0:
                UIEventToGame.Instance.OnUIEventSkillName("어썰트 블레이드", "25");
                break;
            case 1:
                UIEventToGame.Instance.OnUIEventSkillName("파워 스트라이크", "30");
                break;
            case 2:
                UIEventToGame.Instance.OnUIEventSkillName("팬텀 댄스", "35");
                break;
            case 3:
                UIEventToGame.Instance.OnUIEventSkillName("어스 스트라이크", "40");
                break;
            case 4:
                UIEventToGame.Instance.OnUIEventSkillName("라스트 윈드", "45");
                break;
        }
        UIEventToGame.Instance.skillId += SkillId;
    }

    public void BuySkillBtn()
    {
        if (GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] != true)
        {
            switch (UIEventToGame.Instance.UIeventSkillId())
            {
                case 3:
                    if(GameData.Instance.player.cylinderCounter > 25)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                        CylinderCounter(25);
                        OpenSuccedWindow();
                    }
                    else
                    {
                        OpenFailWindow();
                    }
                    break;
                case 4:
                    if (GameData.Instance.player.cylinderCounter > 30)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                        CylinderCounter(30);
                        OpenSuccedWindow();
                    }
                    else
                    {
                        OpenFailWindow();
                    }
                    break;
                case 5:
                    if (GameData.Instance.player.cylinderCounter > 35)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                        CylinderCounter(35);
                        OpenSuccedWindow();
                    }
                    else
                    {
                        OpenFailWindow();
                    }
                    break;
                case 6:
                    if (GameData.Instance.player.cylinderCounter > 40)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                        CylinderCounter(40);
                        OpenSuccedWindow();
                    }
                    else
                    {
                        OpenFailWindow();
                    }
                    break;
                case 7:
                    if (GameData.Instance.player.cylinderCounter >= 45)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                        CylinderCounter(45);
                        OpenSuccedWindow();
                    }
                    else
                    {
                        OpenFailWindow();
                    }
                    break;
            }
        }
        else
        {
            OpenFailWindow();
        }
        UIEventToGame.Instance.skillId -= SkillId;
    }
    public void CylinderCounter(int count)
    {
        GameData.Instance.player.cylinderCounter -= count;
    }
    public void NoBtn()
    {
        UIEventToGame.Instance.skillId -= SkillId;
    }
    public int SkillId()
    {
        return skillId + 3;
    }

    public void OpenSuccedWindow()
    {
        SucceedWindow.SetActive(true);
    }

    public void CloseSuccedWindow()
    {
        SucceedWindow.SetActive(false);
    }

    public void OpenFailWindow()
    {
        FailWinodw.SetActive(true);
    }

    public void CloseFailWindow()
    {
        FailWinodw.SetActive(false);
    }
}
