using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMarster : MonoBehaviour
{
    public string NextPortalName;
    public string NextSceneName;
    public bool isPossibleMove = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!isPossibleMove) return;
        if (!other.CompareTag("Player")) return;
        Debug.Log(NextPortalName);
        Debug.Log(NextSceneName);
        SceneMgr.Instance.LoadScene(NextSceneName, NextPortalName);
    }
}
