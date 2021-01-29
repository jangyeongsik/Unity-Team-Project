using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMaster : MonoBehaviour
{
    public Mesh Disable;
    public Mesh Active;

    [HideInInspector]
    public MeshFilter mesh;
    public string NextPortalName;
    public string NextSceneName;
    public bool isPossibleMove = false;
    public bool checkBossClear;

    ClearZone clearZone;

    private void Start()
    {
        Disable = Resources.Load("DeActive") as Mesh;
        Active = Resources.Load("Active") as Mesh;

        mesh = GetComponent<MeshFilter>();
        if (!isPossibleMove && Disable != null)
            mesh.mesh = Disable;

        clearZone = FindObjectOfType<ClearZone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkBossClear && !GameData.Instance.player.bossClear) return;
        if (!isPossibleMove) return;
        if (!other.CompareTag("Player")) return;
        if (GameData.Instance.player.isSceneMove) return;
        if (NextPortalName == "") return;
        SceneMgr.Instance.LoadScene(NextSceneName, NextPortalName);
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameData.Instance.player.isSceneMove)
            GameData.Instance.player.isSceneMove = false;
        if(clearZone != null)
            clearZone.portalName = transform;
    }
}
