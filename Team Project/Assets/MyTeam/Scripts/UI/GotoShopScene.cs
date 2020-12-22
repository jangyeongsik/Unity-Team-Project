using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GotoShopScene : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject TalkCanvas;
    public GameObject miniMapCanvas;
    public GameObject inventory;

    private bool startTalking = false;
    private bool inventoryOnOff = false;
    Text talk;

    int count;
    int firstTxt;

    private void Awake()
    {
        GameEventToUI.Instance.onOff += ShopOn;
        GameEventToUI.Instance.miniOnOff += MiniMapOn; ;
        GameEventToUI.Instance.talk += TalkOn;
    }

    private void Start()
    {
        GameObject text = TalkCanvas.transform.Find("sorse").gameObject;
        talk = text.GetComponent<Text>() as Text;
    }

    void Update()
    {
        NextDialouge();

        if (Input.GetKeyDown(KeyCode.B))
        {
            invenOnOff();
        }
    }

    public void ShopOn(bool isOn)
    {
        shopCanvas.SetActive(isOn);
    }

    public void TalkOn(bool isOn, int id, string NpcName)
    {
        FindNpc(id, NpcName);
        TalkCanvas.SetActive(isOn);
    }

    public void MiniMapOn(bool isOn)
    {
        miniMapCanvas.SetActive(isOn);
    }

    public void FindNpc(int id, string NpcName)
    {
        for (int i = 0; i < GameData.Instance.data.Count; i++)
        {
            if(GameData.Instance.data[i].id == id && GameData.Instance.data[i].name.Equals(NpcName))
            {
                firstTxt = i;
                startTalking = true;
                count = 0;
                break;
            }
        }
        talk.text = GameData.Instance.data[firstTxt].talk[count++];
    }

    void NextDialouge()
    {
        if(startTalking)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (count < GameData.Instance.data[firstTxt].talk.Count)
                {
                    talk.text = GameData.Instance.data[firstTxt].talk[count++];
                }
                else
                {
                    startTalking = false;
                    TalkCanvas.SetActive(false);
                }
            }
        }
    }

    void invenOnOff()
    {
        inventoryOnOff = !inventoryOnOff;
        inventory.gameObject.SetActive(inventoryOnOff);
    }

}
