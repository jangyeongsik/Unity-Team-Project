using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class TeleportController : MonoBehaviour
{
    public static GameObject player;
    StringBuilder sb = new StringBuilder();
    public static string destSceneName;
    public static string currentSceneName;
    public static int destTPIdx;
    public static bool isTeleporting = false;
    private void Start()
    {
        destTPIdx = 0;
        sb.Append("MainGameScene");
        currentSceneName = sb.ToString();
        DontDestroyOnLoad(gameObject);
    }
    private void LateUpdate()
    {
        if (player == null)
        {
            player = GameData.Instance.player.position.gameObject;
        }
    }
    public void Teleport()
    {
        isTeleporting = true;
        SceneManager.UnloadSceneAsync(currentSceneName);
        sb.Clear();
        sb.Append(destSceneName);
        currentSceneName = sb.ToString();
        SceneManager.LoadScene(destSceneName,LoadSceneMode.Additive);
    }
    public void SetTPSceneName(int num)
    {
        switch (num)
        {
            case 0:
                sb.Clear();
                sb.Append("MainGameScene");
                destSceneName = sb.ToString();
                break;
            case 1:
                sb.Clear();
                sb.Append("PortalTestScene");
                destSceneName = sb.ToString();
                break;
        }
    }
    public void SetTPIndex(int num)
    {
        destTPIdx = num + 1;
    }
}