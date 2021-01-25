using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNameToInfoWindow : MonoBehaviour
{
    public int skillId;
    int cylinder;

    private void Start()
    {
        cylinder = GameData.Instance.player.cylinderCounter;
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
                    if(cylinder > 25)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                    }
                    else
                    {
                        Debug.Log("씰린더 부족");
                    }
                    break;
                case 4:
                    if (cylinder > 30)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                    }
                    break;
                case 5:
                    if (cylinder > 35)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                    }
                    break;
                case 6:
                    if (cylinder > 40)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                    }
                    break;
                case 7:
                    if (cylinder >= 0)
                    {
                        GameData.Instance.player.skillShop[UIEventToGame.Instance.UIeventSkillId()] = true;
                    }
                    break;
            }
           
        }
        else
        {
            Debug.Log("이미 샀습니다.");
        }
        UIEventToGame.Instance.skillId -= SkillId;
    }

    public void NoBtn()
    {
        UIEventToGame.Instance.skillId -= SkillId;
    }
    public int SkillId()
    {
        return skillId + 3;
    }
}
