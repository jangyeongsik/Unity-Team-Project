using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMapCam : MonoBehaviour
{
    private GameObject target;
    Vector3 direction;
    public float velocity;
    public float accelaration;
    void Start()
    {
       target = GameData.Instance.player.position.gameObject;
        
    }
    private void Update()
    {
        Vector3 dir;
        dir.x = target.transform.position.x;
        dir.y = transform.position.y;
        dir.z = target.transform.position.z;

        transform.position = dir;
    }

}
