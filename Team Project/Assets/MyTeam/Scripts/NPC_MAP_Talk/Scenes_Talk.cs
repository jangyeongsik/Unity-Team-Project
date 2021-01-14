using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenes_Talk : MonoBehaviour
{
    [SerializeField] int talk_id;
    bool isTrue;

    private void Start()
    {
        if (!isTrue)
        {
            GameEventToUI.Instance.OnEvent_TalkBox(talk_id);
            isTrue = true;
        }
    }
}
