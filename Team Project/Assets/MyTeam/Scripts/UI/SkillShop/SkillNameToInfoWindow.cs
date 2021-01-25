using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNameToInfoWindow : MonoBehaviour
{
    public int skillId;

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
    }
}
