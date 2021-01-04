using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuButtonsController : MonoBehaviour
{
    Toggle toggle;
    private void Awake()
    {
        toggle = gameObject.GetComponent<Toggle>();
    }
    public void SetIsOn()
    {
        if (toggle.isOn)
        {
            gameObject.GetComponent<Animator>().ResetTrigger("Deselected");
            gameObject.GetComponent<Animator>().SetTrigger("Selected");
        }
        else
        {
            gameObject.GetComponent<Animator>().ResetTrigger("Selected");
            gameObject.GetComponent<Animator>().SetTrigger("Deselected");
        }
    }
}
