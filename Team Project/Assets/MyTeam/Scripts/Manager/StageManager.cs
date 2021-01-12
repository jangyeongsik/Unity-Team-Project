using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public bool isClear = false;
    public bool isRegen = true;

    static Monster[] enemys;

    TeleportMarster[] potars;

    private void Start()
    {
        if(enemys == null)
            enemys = GameObject.FindObjectsOfType<Monster>();
        if (potars == null)
            potars = GameObject.FindObjectsOfType<TeleportMarster>();
        if(!isRegen && isClear)
        {
            for(int i = 0; i < enemys.Length; ++i)
            {
                enemys[i].gameObject.SetActive(true);
            }
        }

    }

    private void Update()
    {
        if(isClear == false)
        {
            if(GameObject.FindObjectOfType<Monster>() == null)
            {
                isClear = true;
                isRegen = true;
            }
        }
        else
        {
            for (int i = 0; i < potars.Length; ++i)
            {
                potars[i].isPossibleMove = true;
            }
        }

    }

}
