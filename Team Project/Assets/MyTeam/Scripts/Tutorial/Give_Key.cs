using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Give_Key : MonoBehaviour
{
    bool isTrue;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTrue)
        {
            GameEventToUI.Instance.OnEventMonsterDrop(5);
            isTrue = true;

        }
    }

}
