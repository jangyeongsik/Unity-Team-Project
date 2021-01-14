using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosPatton2Wall : MonoBehaviour
{
    BossData tartos;
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        tartos = transform.parent.GetComponent<BossData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tartos.bossState == State.BossState.B_SkillChargeTwo)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
