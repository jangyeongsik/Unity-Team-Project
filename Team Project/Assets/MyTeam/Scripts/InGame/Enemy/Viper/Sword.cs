using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PoolableObject
{
    public Transform EnemyTranform;

    private void OnEnable()
    {
        StartCoroutine(RoMove());
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * 15f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectPoolManager.GetInstance().objectPool.PushObject(gameObject);
    }

    IEnumerator RoMove()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        ObjectPoolManager.GetInstance().objectPool.PushObject(gameObject);
    }
}
