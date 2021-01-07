using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : SingletonMonobehaviour<SceneMgr>
{
    
    
    public void LoadScene(string SceneName,string portalName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        GameData.Instance.player.PlayerMovePosition(GameObject.Find(portalName).transform.position);
    }
}
