using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    [SerializeField] private GameObject element_prefab = null;
    [SerializeField] private Transform content = null;

    public void AddElement()
    {
        var instance = Instantiate(element_prefab);
        instance.transform.SetParent(content);
    }


}
