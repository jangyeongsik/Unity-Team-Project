using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosPatton1 : MonoBehaviour
{
    public BossTartos p_b_tartos;
    public Animator anim;

    private void Update()
    {
        if (p_b_tartos.isCharge)
        {
            anim.SetTrigger("ChargeOn1_1");
            anim.SetTrigger("ChargeOn1_4");
        }
    }

    // Update is called once per frame
    public void endCharge()
    {
        p_b_tartos.TartosCheckPattonOne();
        anim.SetTrigger("ChargeOff1_1");
        anim.SetTrigger("ChargeOff1_4");
    }
}
