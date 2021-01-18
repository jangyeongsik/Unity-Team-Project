using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineCampEnterButtons : MonoBehaviour
{
    public void onEventEnter()
    {
        UIEventToGame.Instance.OnActivateVaccineCampPortal(true);
        gameObject.SetActive(false);
    }
    public void offEventCancel()
    {
        gameObject.SetActive(false);
    }
}
   


