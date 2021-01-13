using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosPatton1 : MonoBehaviour
{
    BossData tartos;
    public Animator animator;

    private void Start()
    {
        tartos = animator.transform.parent.GetComponent<BossData>();

    }

    public void Start_Skill1_1()
    {
        animator.SetTrigger("patton1");
        transform.parent.gameObject.SetActive(false);
    }

    public void Start_Skill1_2()
    {
        animator.SetTrigger("patton1_2");
        transform.parent.gameObject.SetActive(false);
    }

    public void Start_Skill1_3()
    {
        animator.SetTrigger("patton1_3");
        transform.parent.gameObject.SetActive(false);
        //tartos.bossState = State.BossState.B_Attack;
        //animator.SetInteger("Attack", 1);
    }
}
