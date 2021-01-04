using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerButtonEvent : MonoBehaviour
{

    private bool shopOnOff = false;
    private bool talkOnOff = false;
    private bool miniMapOnOff = false;

    private void Awake()
    {
        //SceneManager.LoadScene("UI Scene", LoadSceneMode.Additive);
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
            GameEventToUI.Instance.OnEventTalkOnOff(talkOnOff, 1001, other.gameObject.name);
        }
    }
}
