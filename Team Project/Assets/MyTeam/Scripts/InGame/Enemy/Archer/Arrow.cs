using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform EnemyTranform;
    private void Update()
    {

        transform.Translate(Vector3.forward * 15f * Time.deltaTime);
       

    }

}
