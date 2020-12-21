using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerButtonEvent : MonoBehaviour
{

    private bool shopOnOff = false;
    private bool talkOnOff = false;
    private bool miniMapOnOff = false;
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
                FindTarget();
            }
        }
    }
    void GotoShopScene()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            shopOnOff = !shopOnOff;
            BetweenPlayerAndShop.Instance.OnEventShopOnOff(shopOnOff);
        }
    }

    public void OpenMiniMap()
    {
        BetweenPlayerAndShop.Instance.OnEventMinimapOnOff(true);
    }

    void FindTarget()
    {
       /* Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Npc"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    talkOnOff = !talkOnOff;
                    BetweenPlayerAndShop.Instance.OnEventTalkOnOff(talkOnOff, 1001, hit.collider.gameObject.name);
                }
            }
        }*/

    }
}
