using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleLever : MonoBehaviour
{
    public string leverName;
    public string description;
    public GameObject templePortal;

    private void Start()
    {
        UIEventToGame.Instance.ActivateTemplePortal += ActiavateTemplePortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEventToUI.Instance.OnLeverPopup(true, leverName, description);
        }
    }

    private void ActiavateTemplePortal(bool isOn)
    {
        templePortal.SetActive(isOn);
    }
}
