using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Temple : MonoBehaviour
{
    public GameObject TempleSelectUI;
    public GameObject PrayUI;
    public TMP_Text TempleSelectUINameText;
    public TMP_Text PrayUINameText;

    public void Cancel()
    {
        TempleSelectUI.SetActive(false);
        gameObject.SetActive(false);
    }
    public void Enter()
    {
        TempleSelectUI.SetActive(false);
        PrayUI.SetActive(true);
        PrayUINameText.text = TempleSelectUINameText.text;
        UIEventToGame.Instance.OnActivateTemple(true);
    }
}
