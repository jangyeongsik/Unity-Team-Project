using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenes_Talk : MonoBehaviour
{
    [SerializeField] int talk_id;
    [SerializeField] int Map_number;

    private void Start()
    {
        if (GameData.Instance.player.Talk_Box[Map_number] == false)
        {
            GameEventToUI.Instance.OnEvent_TalkBox(talk_id);
            GameData.Instance.player.Talk_Box[Map_number] = true;
        }
    }
}
