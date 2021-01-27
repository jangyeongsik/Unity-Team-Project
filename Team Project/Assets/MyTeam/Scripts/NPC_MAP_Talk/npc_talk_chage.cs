using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc_talk_chage : MonoBehaviour
{
    public List<string> talk_data;
    int count;
    Text npc_talk;
    private void Awake()
    {
        talk_data = new List<string>();
        npc_talk = GetComponent<Text>();
        GameEventToUI.Instance.npc_talk_chage += OnEventTalkChage;
        GameEventToUI.Instance.npc_talk_setting += OnTalkSetting;
        GameEventToUI.Instance.npc_talk_print += talk_print;
        GameEventToUI.Instance.npc_talk_Next += NextTalk;
    }

    public void OnEventTalkChage(string name)
    {
        npc_talk.text = name;
    }

    public void OnTalkSetting(int index)
    {
        
        talk_data = GameData.Instance.data[index].talk;
    }

    public void talk_print()
    {
        npc_talk.text = talk_data[count++];
    }

    public void NextTalk()
    {
        if(count < talk_data.Count)
        {
            talk_print();
        }
        else
        {
            count = 0;
            GameEventToUI.Instance.OnEventJoystick();
            GameEventToUI.Instance.OnEventTalkoff();
            if (GameData.Instance.player.curSceneName.Equals("MAP001"))
            {
                GameEventToUI.Instance.onEventSkillShop();
            }
           
        }
    }


    private void OnDestroy()
    {
        GameEventToUI.Instance.npc_talk_chage -= OnEventTalkChage;
        GameEventToUI.Instance.npc_talk_setting -= OnTalkSetting;
        GameEventToUI.Instance.npc_talk_print -= talk_print;
        GameEventToUI.Instance.npc_talk_Next -= NextTalk;
    }
}
