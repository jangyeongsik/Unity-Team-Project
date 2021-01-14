using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PoolableObject
{
    public Transform EnemyTranform;

    private void OnEnable()
    {
        StartCoroutine(ReMove());
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * 15f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ObjectPoolManager.GetInstance().objectPool.PushObject(gameObject);
            GameEventToUI.Instance.OnPlayerHit(EnemyTranform, 1);
        }
    }

    IEnumerator ReMove()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        ObjectPoolManager.GetInstance().objectPool.PushObject(gameObject);
    }
}
