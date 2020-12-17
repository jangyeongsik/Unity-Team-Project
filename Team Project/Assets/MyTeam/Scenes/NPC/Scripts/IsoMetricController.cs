using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoMetricController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    Vector3 forward, right;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0.0f;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Update()
    {
        if(Input.anyKey)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 RightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 ForwardMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 FinalMovement = ForwardMovement + RightMovement;
        Vector3 direction = Vector3.Normalize(FinalMovement);

        if(direction != Vector3.zero)
        {
            transform.forward = direction;
            transform.position += FinalMovement;
        }
    }
}
