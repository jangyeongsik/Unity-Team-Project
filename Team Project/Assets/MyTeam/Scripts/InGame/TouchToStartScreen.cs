using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchToStartScreen : MonoBehaviour
{
    public void TouchToStart()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "터치투스타트");
        SceneManager.LoadScene("GameStartScene");
    }
}
