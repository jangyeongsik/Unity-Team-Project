using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineLever : MonoBehaviour
{
    public string leverName;
    public string description;
    public GameObject templePortal;
    public StageManager sM;
    bool isUsed;
    private void Start()
    {
        
        isUsed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (sM.isClear && !isUsed)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                UIEventToGame.Instance.ActivateVaccineCampPortal += ActiavateVaccineCampPortal;
                GameEventToUI.Instance.OnLeverPopup(true, leverName, description);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (sM.isClear && !isUsed)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                UIEventToGame.Instance.ActivateVaccineCampPortal -= ActiavateVaccineCampPortal;
                GameEventToUI.Instance.OnLeverPopup(false, leverName, description);
            }
        }
    }

    private void ActiavateVaccineCampPortal(bool isOn)
    {
        isUsed = true;
        templePortal.SetActive(isOn);
        templePortal.GetComponent<TeleportMaster>().isPossibleMove = true;
    }
    private void OnDestroy()
    {
        UIEventToGame.Instance.ActivateVaccineCampPortal -= ActiavateVaccineCampPortal;
    }
}
