using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamera : MonoBehaviour
{
    private float cameraMovingCount = 0.0f;
    private float cameraMovingDelay = 5.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindObjectOfType<Monster>() == null)
        {
            cameraMovingCount += Time.deltaTime;
            if (cameraMovingCount > cameraMovingDelay)
            {
                gameObject.SetActive(false);
                cameraMovingCount = 0.0f;
            }
        }
    }
}
