using System;
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
    public GameObject equipment;
    public GameObject encyclopediaPanel;
    public GameObject badgeCanvas;


    private bool startTalking = false;
    private bool inventoryOnOff = false;
    private bool equipmentOnOff = false;
    private bool encyclopediaOnOff = false;
    Text talk;

    int count;
    int firstTxt;

    private void Awake()
    {
        GameEventToUI.Instance.onOff += ShopOn;
        GameEventToUI.Instance.miniOnOff += MiniMapOn; ;
        GameEventToUI.Instance.badgeOnOff += BadgeOn; ;
        GameEventToUI.Instance.talk += TalkOn;
        GameEventToUI.Instance.encyclopediaOnOff += EncyclopediaOn;
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            equipOnOff();
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

    public void BadgeOn(bool isOn)
    {
        badgeCanvas.SetActive(isOn);
    }

    public void EncyclopediaOn(bool isOn)
    {
        encyclopediaPanel.SetActive(isOn);
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
        if (equipmentOnOff) equipmentOnOff = !equipmentOnOff;
        inventoryOnOff = !inventoryOnOff;
        inventory.gameObject.SetActive(inventoryOnOff);
    }
    void equipOnOff()
    {
        if (inventoryOnOff) inventoryOnOff = !inventoryOnOff;
        equipmentOnOff = !equipmentOnOff;
        equipment.gameObject.SetActive(equipmentOnOff);
    }
}
