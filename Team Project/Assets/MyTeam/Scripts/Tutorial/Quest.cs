using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
    public TextMeshProUGUI quest_name;
    public TextMeshProUGUI quest_count;

    private void Start()
    {
        UIEventToGame.Instance.Quest_Name += QuestName;
        UIEventToGame.Instance.Quest_Count += QuestCount;
    }
    public void QuestName(string name)
    {
        quest_name.text = name;
    }
    public void QuestCount(string count)
    {
        quest_count.text = count;
    }
}
