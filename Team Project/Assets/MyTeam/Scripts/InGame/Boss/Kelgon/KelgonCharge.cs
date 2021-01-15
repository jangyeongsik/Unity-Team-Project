using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelgonCharge : MonoBehaviour
{
    public Animator animator;

    public void Start_Skill1()
    {
        animator.SetTrigger("Skill Attack");
        //transform.parent.gameObject.SetActive(false);
    }
}
