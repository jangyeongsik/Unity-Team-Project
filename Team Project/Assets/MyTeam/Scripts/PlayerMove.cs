using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public JoyStick joyStick;
    private float moveSpeed = 15;

    // Update is called once per frame
    void Update()
    {
        float x = joyStick.Horizontal();
        float y = joyStick.Vertical();

        Vector3 dir = new Vector3(x, 0, y);
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
