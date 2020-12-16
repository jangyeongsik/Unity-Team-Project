using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public JoyStick joyStick;
    private float moveSpeed = 15f;
    public Vector3 frameMovement;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float x = joyStick.Horizontal();
        float y = joyStick.Vertical();
        //                                                                        이거해야 대각선 안빨라짐
        Vector3 movement = transform.InverseTransformDirection(new Vector3(x, 0, y).normalized);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        frameMovement = new Vector3(x, 0f, y).normalized;
        Quaternion rotation = Quaternion.LookRotation(frameMovement);
        transform.rotation = rotation;
    }
}
