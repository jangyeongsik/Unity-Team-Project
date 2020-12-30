using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("MainGameScene", LoadSceneMode.Additive);

    }
}
