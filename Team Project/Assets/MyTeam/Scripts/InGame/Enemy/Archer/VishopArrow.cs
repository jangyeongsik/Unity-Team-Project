using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VishopArrow : PoolableObject
{
    public Transform EnemyTranform;
    public int damage = 1;
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
        if(other.CompareTag("Player"))
        {
            ObjectPoolManager.GetInstance().objectPool2.PushObject(gameObject);
            GameEventToUI.Instance.OnPlayerHit(EnemyTranform, damage);
        }
    }

    IEnumerator ReMove()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        ObjectPoolManager.GetInstance().objectPool2.PushObject(gameObject);
    }
}
