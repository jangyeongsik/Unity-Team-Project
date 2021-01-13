using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleEnterButtons : MonoBehaviour
{
    public GameObject talk_btn;

    private void Start()
    {
        GameEventToUI.Instance.talkButOnOff += OnOffEvent;
    }
    public void OnOffEvent(bool isOn)
    {
        talk_btn.SetActive(isOn);
    }

    public void onEventEnter()
    {
        UIEventToGame.Instance.OnActivateTemplePortal(true);
        gameObject.SetActive(false);
    }


    public void offEventCancel()
    {
        gameObject.SetActive(false);
    }



    private void OnDestroy()
    {
        GameEventToUI.Instance.talkButOnOff -= OnOffEvent;
    }
}
   


