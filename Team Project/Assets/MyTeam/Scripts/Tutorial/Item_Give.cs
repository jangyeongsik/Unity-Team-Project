using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Give : MonoBehaviour
{
    private bool isTrue;

    private void Start()
    {
        DataManager.Instance.DeleteData();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTrue && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            for (int i = 0; i < 3; i++)
            {
                Inventory.Instance.AddIngredient(101);
                Inventory.Instance.AddIngredient(102);
            }
            isTrue = true;
        }
    }
}
