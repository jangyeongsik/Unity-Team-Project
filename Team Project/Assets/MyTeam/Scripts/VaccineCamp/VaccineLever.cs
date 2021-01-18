using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineLever : MonoBehaviour
{
    public string leverName;
    public string description;
    public GameObject templePortal;

    private void Start()
    {
        UIEventToGame.Instance.ActivateVaccineCampPortal += ActiavateVaccineCampPortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEventToUI.Instance.OnLeverPopup(true, leverName, description);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEventToUI.Instance.OnLeverPopup(false, leverName, description);
        }
    }

    private void ActiavateVaccineCampPortal(bool isOn)
    {
        templePortal.SetActive(isOn);
    }
}
