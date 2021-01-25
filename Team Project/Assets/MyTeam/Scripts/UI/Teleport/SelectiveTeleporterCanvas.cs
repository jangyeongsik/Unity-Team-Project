using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveTeleporterCanvas : MonoBehaviour
{
    public GameObject ActivationScreen;
    public GameObject TPScreen;
    private void Start()
    {
        GameEventToUI.Instance.SelectiveTeleport += PopUp;
    }
    private void PopUp(bool isOn, string curSceneName)
    {
        gameObject.SetActive(isOn);
        bool isTpActivated = false;
        int idx = 0;
        switch (curSceneName)
        {
            case "MAP001":
                isTpActivated = GameData.Instance.player.tpActivate[0];
                idx = 0;
                break;
            case "MAP014":
                isTpActivated = GameData.Instance.player.tpActivate[1];
                idx = 1;
                break;
            case "MAP021":
                isTpActivated = GameData.Instance.player.tpActivate[2];
                idx = 2;
                break;
        }
        if (isTpActivated)
        {
            if (ActivationScreen.activeSelf)
            {
                ActivationScreen.SetActive(false);
            }
            TPScreen.SetActive(true);
            TPScreen.GetComponent<SelectiveTeleporterTpScreen>().curTPPointIdx = idx;
        }
        else
        {
            if (TPScreen.activeSelf)
            {
                TPScreen.SetActive(false);
            }
            ActivationScreen.SetActive(true);
            ActivationScreen.GetComponent<SelectiveTeleporterActivationScreen>().TPPointIdx = idx;
        }
    }
    private void OnDestroy()
    {
        GameEventToUI.Instance.SelectiveTeleport -= PopUp;
    }
}
