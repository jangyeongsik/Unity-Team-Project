using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosPattonChenkPlayer : MonoBehaviour
{
    BossData tartos;
    private Collider[] cols;
    bool checkingPlayer = false;

    private void Start()
    {
        tartos = transform.parent.GetComponent<BossData>();
    }

    // Update is called once per frame
    void Update()
    {

            cols = Physics.OverlapBox(gameObject.transform.position, gameObject.transform.position);
            for (int i = 0; i < cols.Length; i++)
            {                
                /*if (cols[i].CompareTag("Player"))
                {
                    tartos.PlayerHit();
                    isCheck = true;
                    break;
                }*/
            }
             
    }


}
