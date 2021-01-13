using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc_name_chage : MonoBehaviour
{
    public List<string> name_data;
    int count;
    Text npc_name;
    private void Awake()
    {
        name_data = new List<string>();
        npc_name = GetComponent<Text>();
        GameEventToUI.Instance.npc_name_chage += OnEventNameChage;
        GameEventToUI.Instance.npc_name_setting += OnNameSetting;
        GameEventToUI.Instance.npc_name_print += name_print;
        GameEventToUI.Instance.npc_talk_Next += Nextname;
    }
    private void Start()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void OnEventNameChage(string name)
    {
        npc_name.text = name;
    }

    public void OnNameSetting(int index)
    {
        name_data =  GameData.Instance.data[index].talk_name;
    }

    public void name_print()
    {
        npc_name.text = name_data[count++];
    }

    public void Nextname()
    {
        if (count < name_data.Count)
        {
            name_print();
        }
        else
        {
            count = 0;
            GameEventToUI.Instance.OnEventJoystick();
        }
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.npc_talk_chage -= OnEventNameChage;
        GameEventToUI.Instance.npc_name_setting -= OnNameSetting;
        GameEventToUI.Instance.npc_name_print -= name_print;
        GameEventToUI.Instance.npc_talk_Next -= Nextname;
    }


}
