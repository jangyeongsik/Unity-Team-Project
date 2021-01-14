using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScreenSize : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(Screen.width,Screen.width*9/16,true);
    }
}
