using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SdInteractioniBtn : MonoBehaviour
{
    public GameObject secretDungeonWindow;

    public void OpenTheSecretDungeonInfo()
    {
        secretDungeonWindow.SetActive(true);
    }

    public void CloseTheSecretDungeonInfo()
    {
        secretDungeonWindow.SetActive(false);
    }
}
