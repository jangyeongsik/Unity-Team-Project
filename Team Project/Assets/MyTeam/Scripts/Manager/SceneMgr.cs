using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : SingletonMonobehaviour<SceneMgr>
{
    public static string CurrentSceneName = "";
    
    public void LoadScene(string SceneName,string portalName)
    {
        StartCoroutine(MovePlayer(SceneName, portalName));  
    }

    IEnumerator MovePlayer(string SceneName, string portalName)
    {
        if(CurrentSceneName != "")
        {
            SceneManager.UnloadSceneAsync(CurrentSceneName);
        }
        CurrentSceneName = SceneName;
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        while (GameObject.Find(portalName) == null)
        {
            yield return new WaitForEndOfFrame();
        }
        GameData.Instance.player.PlayerMovePosition(GameObject.Find(portalName).transform.position);
    }
}
