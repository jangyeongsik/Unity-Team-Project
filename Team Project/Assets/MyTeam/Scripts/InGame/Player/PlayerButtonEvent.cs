using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerButtonEvent : MonoBehaviour
{

    private bool shopOnOff = false;
    private bool talkOnOff = false;
    private bool miniMapOnOff = false;
    private bool invenOnOff = false;
    private void Start()
    {
        SceneManager.LoadScene("UI Scene", LoadSceneMode.Additive); 
    }
    private void Update()
    {
        if (Input.anyKey)
        {
            if (!talkOnOff)
            {
                GotoShopScene();
                GotoInven();
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
    void GotoInven()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            invenOnOff = !invenOnOff;
            GameEventToUI.Instance.OnEventInventoryOnOff(invenOnOff);
        }
    }

    public void OpenMiniMap()
    {
        BetweenPlayerAndShop.Instance.OnEventMinimapOnOff(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            talkOnOff = !talkOnOff;
            BetweenPlayerAndShop.Instance.OnEventTalkOnOff(talkOnOff, 1001, other.gameObject.name);
        }
    }
}
