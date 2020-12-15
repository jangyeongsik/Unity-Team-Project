using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera[] vCamera;

    void Start()
    {
        vCamera[0].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach(var cam in vCamera)
            {
                cam.enabled = false;
            }
            vCamera[0].enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (var cam in vCamera)
            {
                cam.enabled = false;
            }
            vCamera[1].enabled = true;
        }
    }
}
