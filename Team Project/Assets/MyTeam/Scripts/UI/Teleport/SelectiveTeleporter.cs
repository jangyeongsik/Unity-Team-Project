using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveTeleporter : MonoBehaviour
{
    bool isTeleporting;
    private void Start()
    {
        isTeleporting = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTeleporting) return;
        if (other.CompareTag("Player"))
        {
            GameEventToUI.Instance.ActivateSelectiveTeleporter(true, GameData.Instance.player.curSceneName);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTeleporting = false;
            GameEventToUI.Instance.ActivateSelectiveTeleporter(false, GameData.Instance.player.curSceneName);
        }
    }
}
