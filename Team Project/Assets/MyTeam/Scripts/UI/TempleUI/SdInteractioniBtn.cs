using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SdInteractioniBtn : MonoBehaviour
{
    public GameObject secretDungeonWindow;

    public void OpenTheSecretDungeonInfo()
    {
        UIEventToGame.Instance.OnActivateTemplePortal(true);
        secretDungeonWindow.SetActive(true);
    }

    public void CloseTheSecretDungeonInfo()
    {
        UIEventToGame.Instance.OnActivateTemplePortal(false);
        secretDungeonWindow.SetActive(false);
    }
}
