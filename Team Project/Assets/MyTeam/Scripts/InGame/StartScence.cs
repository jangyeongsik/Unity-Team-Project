using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScence : MonoBehaviour
{
    public void StartSence()
    {
        SceneManager.LoadScene("GameStartScene");
    }
}
