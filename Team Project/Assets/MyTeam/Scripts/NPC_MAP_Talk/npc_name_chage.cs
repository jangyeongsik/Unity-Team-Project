using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc_name_chage : MonoBehaviour
{
    Text npc_name;

    private void Start()
    {
        npc_name = GetComponent<Text>();
        GameEventToUI.Instance.npc_name_chage += OnEventNameChage;

    }

    public void OnEventNameChage(string name)
    {
        npc_name.text = name;
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.npc_talk_chage -= OnEventNameChage;
    }
}
