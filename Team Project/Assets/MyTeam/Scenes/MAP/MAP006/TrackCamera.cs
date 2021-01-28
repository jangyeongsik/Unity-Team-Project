using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamera : MonoBehaviour
{
    private float cameraMovingCount = 0.0f;
    private float cameraMovingDelay = 5.0f;
    // Update is called once per frame
    void Update()
    {
        if (GameData.Instance.player.bossClear == false)
        {
            cameraMovingCount += Time.deltaTime;
            if (cameraMovingCount > cameraMovingDelay)
            {
                gameObject.SetActive(false);
                cameraMovingCount = 0.0f;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
