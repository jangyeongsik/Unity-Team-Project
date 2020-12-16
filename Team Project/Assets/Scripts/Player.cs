using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float RunSpeed;
    float applySpeed;

    // Start is called before the first frame update
    void Start()
    {
        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Run();
    }

    private void Move()
    {
        float hDir = Input.GetAxisRaw("Horizontal");
        float vDir = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(hDir, 0f, vDir).normalized;
        
        bool isMove = dir != Vector3.zero ? true : false;
        if (isMove)
        {
            transform.position += dir * applySpeed * Time.deltaTime;
            transform.forward = dir;
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            applySpeed = RunSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            applySpeed = walkSpeed; 
        }
    }
}
