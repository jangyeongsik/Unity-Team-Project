using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamera : MonoBehaviour
{
    private float cameraMovingCount = 0.0f;
    private float cameraMovingDelay = 5.0f;
    void Start()
    {
       if(GameObject.FindObjectsOfType<Monster>().Length == 0)
       {
           transform.parent.gameObject.SetActive(false);
            Debug.Log("aa");
       }
    }

    // Update is called once per frame
    void Update()
    {
            cameraMovingCount += Time.deltaTime;
            if (cameraMovingCount > cameraMovingDelay)
            {
                gameObject.SetActive(false);
                cameraMovingCount = 0.0f;
            }
    }
}
