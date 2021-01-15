using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelgonCharge : MonoBehaviour
{
    public Animator animator;
    public GameObject paticle;
    private void Start()
    {
        paticle.SetActive(false);
    }

    public void Start_Skill1()
    {
        animator.SetTrigger("Skill Attack");
        //transform.parent.gameObject.SetActive(false);
    }
    public void paticleOn()
    {
        paticle.SetActive(true);
    }
}
