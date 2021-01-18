using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineEnterButtons : MonoBehaviour
{
    public void Enter()
    {
        UIEventToGame.Instance.OnActivateVaccineCampPortal(true);
        gameObject.SetActive(false);
    }
    public void Revoke()
    {
        gameObject.SetActive(false);
    }
}
