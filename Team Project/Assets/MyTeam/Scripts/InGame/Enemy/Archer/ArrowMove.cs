using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    Vector3 curPos;
    Vector3 startPos;

    Vector3 endPos;

    public GameObject target;

    private void Start()
    {
        startPos = transform.position;
        curPos = startPos;

        endPos = target.transform.position;
    }

    private void Update()
    {
        Fly();
    }

    private void Fly()
    {
        //float step = 15.0f * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(curPos, endPos, step);
    }
}
