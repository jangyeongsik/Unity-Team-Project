using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc_talk_chage : MonoBehaviour
{
    Text npc_talk;

    private void Start()
    {
        npc_talk = GetComponent<Text>();
        GameEventToUI.Instance.npc_talk_chage += OnEventNameChage;

    }

    public void OnEventNameChage(string name)
    {
        npc_talk.text = name;
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.npc_talk_chage -= OnEventNameChage;
    }
}
