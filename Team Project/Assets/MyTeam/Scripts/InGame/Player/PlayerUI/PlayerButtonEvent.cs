using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerButtonEvent : MonoBehaviour
{
    private bool shopOnOff = false;
    private bool templeOnOff = false;
    private bool talkOnOff = false;
    private bool miniMapOnOff = false;
    [SerializeField]
    GameObject vCam;
    GameObject _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = gameObject;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(vCam.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
       if (Input.anyKey)
       {
           if (!talkOnOff)
           {
               GotoShopScene();
           }
       }
    }

    void GotoShopScene()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            shopOnOff = !shopOnOff;
            GameEventToUI.Instance.OnEventShopOnOff(shopOnOff);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            talkOnOff = !talkOnOff;
            GameEventToUI.Instance.OnEventTalkOnOff(talkOnOff, 1000, other.gameObject.name);
        }
    }


}
