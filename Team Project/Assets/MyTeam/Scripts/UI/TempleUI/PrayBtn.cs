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
    private void OnEnable()
    {
        count = 0;
    }
    public void SetPrayCount(int _count)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        keyCount.text = _count.ToString();
        count = _count;
    }
    public void CloseUI()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
    public void Pray()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        if (count <= 0) return;
        if (count > GameData.Instance.player.keyCounter) return;
        UIEventToGame.Instance.OnActivateTemplePortal(true);
        transform.parent.gameObject.SetActive(false);
        GameEventToUI.Instance.OnLoseTempleKeys(count);
    }
}
