using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveTeleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventToUI.Instance.ActivateSelectiveTeleporter(true, GameData.Instance.player.curSceneName);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventToUI.Instance.ActivateSelectiveTeleporter(false, GameData.Instance.player.curSceneName);
        }
    }
}
