using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Key : MonoBehaviour
{
    Text resourceText;
    private int resource;

    void Start()
    {
        resourceText = GetComponent<Text>();
        resourceText.text = resource.ToString();
        GameEventToUI.Instance.keyCount += OnGetTempleKeys;
        GameEventToUI.Instance.LoseTempleKeys += OnLoseTempleKeys;
        resourceText.text = GameData.Instance.player.keyCounter.ToString();
    }

    public void OnGetTempleKeys(int addition)
    {
        GameData.Instance.player.keyCounter += addition;
        resourceText.text = GameData.Instance.player.keyCounter.ToString();
    }

    public void OnLoseTempleKeys(int addition)
    {
        GameData.Instance.player.keyCounter -= addition;
        resourceText.text = GameData.Instance.player.keyCounter.ToString();
    }



    private void OnDestroy()
    {
        GameEventToUI.Instance.keyCount -= OnGetTempleKeys;
    }
}
