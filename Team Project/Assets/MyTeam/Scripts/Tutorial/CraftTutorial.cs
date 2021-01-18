using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTutorial : MonoBehaviour
{
    public GameObject tipPopup;
    public bool isTrue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")&& isTrue == false)
        {
            isTrue = true;
            tipPopup.SetActive(true);
            GameEventToUI.Instance.OnEventJoystickOff();
        }
    }
    public void Close()
    {
        tipPopup.SetActive(false);
        GameEventToUI.Instance.OnEventJoystick();
    }
}
