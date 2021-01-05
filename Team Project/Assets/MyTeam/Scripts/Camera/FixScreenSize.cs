using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScreenSize : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(Screen.width,Screen.width*9/16,true);
        player = GameObject.Find("player");
    }
}
