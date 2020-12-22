using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public void GoToMainGameScene()
    {
        LoadingProgress.LoadScene("MainGameScene");
    }
}
