using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosPattonChenkPlayer : MonoBehaviour
{
    private Collider[] cols;
    bool checkingPlayer = false;

    // Update is called once per frame
    void Update()
    {
        cols = Physics.OverlapBox(gameObject.transform.position, gameObject.transform.position);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag == "Player")
            {
                Debug.Log("잡았다");
                checkingPlayer = true;
            }
        }
    }
}
