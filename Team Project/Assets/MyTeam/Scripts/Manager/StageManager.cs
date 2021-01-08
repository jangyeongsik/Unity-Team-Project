using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static bool isClear = false;

    private void Update()
    {
        if(isClear == false)
        {
            if(GameObject.FindObjectOfType<Monster>() == null)
            {
                isClear = true;
            }
        }

        Debug.Log(isClear);
    }
}
