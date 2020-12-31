using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestBullet : MonoBehaviour
{
    public Vector3 start;
    public Transform enemyTR;

    private void Update()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
    }
}
