using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineCampEnterButtons : MonoBehaviour
{
    public void onEventEnter()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        UIEventToGame.Instance.OnActivateVaccineCampPortal(true);
        gameObject.SetActive(false);
    }
    public void offEventCancel()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        gameObject.SetActive(false);
    }
}
   


