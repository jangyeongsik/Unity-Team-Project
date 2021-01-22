using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public GameObject QuitPopup;
    public void QuitGame()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        GameData.Instance.PlayerSave();
        Application.Quit();
    }
    public void KeepPlay()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        QuitPopup.SetActive(false);
    }
}
