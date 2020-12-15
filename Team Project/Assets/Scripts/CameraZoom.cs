using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float ZoomSpeed;

    Cinemachine.CinemachineFollowZoom offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = GetComponent<Cinemachine.CinemachineFollowZoom>();
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoomDir = Input.GetAxis("Mouse ScrollWheel");
        offset.m_Width -= zoomDir * ZoomSpeed * 2.0f;
        if (offset.m_Width <= 0f) offset.m_Width = 0f;
        if (offset.m_Width >= 22f) offset.m_Width = 22f;
    }
}
