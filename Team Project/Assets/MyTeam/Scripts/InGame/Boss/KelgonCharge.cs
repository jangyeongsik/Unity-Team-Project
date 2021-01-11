using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelgonCharge : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_Skill1()
    {
        animator.SetTrigger("Skill Attack");
        transform.parent.gameObject.SetActive(false);
    }
}
