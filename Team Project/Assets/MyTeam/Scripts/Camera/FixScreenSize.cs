using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScreenSize : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1600, 900, true);
        
    }

}
