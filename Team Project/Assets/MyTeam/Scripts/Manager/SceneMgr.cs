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
        Debug.Log(portalName);
        ScreenFade.Instance.OnFadeIn(1);

        if (CurrentSceneName != "")
        {
            SceneManager.UnloadSceneAsync(CurrentSceneName);
        }
        CurrentSceneName = SceneName;
        //현재씬 이름 저장
        GameData.Instance.player.curSceneName = SceneName;
        //플레이어 데이터 저장
        GameData.Instance.PlayerSave();
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        while (GameObject.Find(portalName) == null)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(GameData.Instance.player.PlayerMovePosition(GameObject.Find(portalName).transform.position));
    }
}
