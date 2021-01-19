using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrayBtn : MonoBehaviour
{
    public Button prayTwice;
    public Button prayFiveTimes;
    public Button prayTenTimes;
    public TMP_Text keyCount;
    public int count;
    public void SetPrayCount(int _count)
    {
        keyCount.text = _count.ToString();
        count = _count;
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
    public void Pray()
    {
        if (count <= 0) return;
        if (count > GameData.Instance.player.keyCounter) return;
        UIEventToGame.Instance.OnActivateTemplePortal(true);
        transform.parent.gameObject.SetActive(false);
        GameEventToUI.Instance.OnLoseTempleKeys(count);
    }
}
