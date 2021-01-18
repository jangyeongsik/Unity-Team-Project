using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleLever : MonoBehaviour
{
    public string leverName;
    public GameObject templePortal;
    public bool isActivated;

    private void Start()
    {
        UIEventToGame.Instance.ActivateTemplePortal += ActivateTemplePortal;
        UIEventToGame.Instance.ActivateTemple += ActivateTemple;
        isActivated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEventToUI.Instance.OnTemplePopup(true, isActivated, leverName);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEventToUI.Instance.OnTemplePopup(false, isActivated, leverName);
        }
    }
    private void ActivateTemplePortal(bool isOn)
    {
        templePortal.SetActive(isOn);
    }
    private void ActivateTemple(bool _isActivated)
    {
        isActivated = _isActivated;
    }
}
