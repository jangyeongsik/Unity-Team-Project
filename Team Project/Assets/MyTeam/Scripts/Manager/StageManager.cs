using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public bool isClear = false;
    public bool isRegen = true;

    static Monster[] enemys;

    TeleportMaster[] portals;

    private void Start()
    {
        if(enemys == null)
            enemys = GameObject.FindObjectsOfType<Monster>();
        if (portals == null)
            portals = GameObject.FindObjectsOfType<TeleportMaster>();
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
            for (int i = 0; i < portals.Length; ++i)
            {
                portals[i].isPossibleMove = true;
                if (portals[i].Active != null)
                    portals[i].mesh.mesh = portals[i].Active;
            }
        }

    }

}
