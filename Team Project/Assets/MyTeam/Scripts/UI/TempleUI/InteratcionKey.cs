using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteratcionKey : MonoBehaviour
{
    public GameObject interactionWindow;

    

   // private void Start()
    
        //gameObject.SetActive(false);
        //GameEventToUI.Instance.interOnOff += interactionBtnOn;
    

    public void OpenTheTempleInfo()
    {
        interactionWindow.SetActive(true);
    }

    public void CloseTheTempleInfo()
    {
        interactionWindow.SetActive(false);
    }

    public void interactionBtnOn(bool isOn)
    { 
        gameObject.SetActive(isOn);
    }


}
