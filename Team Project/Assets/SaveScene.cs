using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScene : MonoBehaviour
{
    public string CurrentSceneName;
    public string CurrentPortalName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameData.Instance.player.SaveSceneName = CurrentSceneName;
            GameData.Instance.player.SavePortalName = CurrentPortalName;
        }
    }
}
