using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public GameObject QuitPopup;
    public void QuitGame()
    {
        GameData.Instance.PlayerSave();
        Application.Quit();
    }
    public void KeepPlay()
    {
        QuitPopup.SetActive(false);
    }
}
