using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GotoShopScene : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject TalkCanvas;
    public GameObject miniMapCanvas;

    public GameObject BadgeCanvas;
    public GameObject EncyclopediaCanvas;
    public GameObject MinimapCanvas;

    public GameObject InventoryCanvas;
    public GameObject InvenUI;
    public GameObject EquipUI;
    public GameObject UIMenuButtons;
    public GameObject Menu;


    List<GameObject> CanvasList = new List<GameObject>();

    private bool isUIOn = false;
    private bool startTalking = false;
    private bool inventoryOnOff = false;
    private bool equipmentOnOff = false;
    private bool BadgeCanvastOnOff = false;
    private bool EncyclopediaCanvasOnOff = false;
    private bool MinimapCanvasOnOff = false;
    Text talk;

    int count;
    int firstTxt;

    private void Awake()
    {
        GameEventToUI.Instance.onOff += ShopOn;
        GameEventToUI.Instance.talk += TalkOn;
    }

    private void Start()
    {
        GameObject text = TalkCanvas.transform.Find("sorse").gameObject;
        talk = text.GetComponent<Text>() as Text;

        CanvasList.Add(shopCanvas);
        CanvasList.Add(TalkCanvas);
        CanvasList.Add(miniMapCanvas);
        CanvasList.Add(InventoryCanvas);
        CanvasList.Add(InvenUI);
        CanvasList.Add(EquipUI);
    }

    void Update()
    {
        NextDialouge();

        DropdownOnOff();
    }

    private void DropdownOnOff()
    {
        if (isUIOn)
            Menu.SetActive(false);
        else
            Menu.SetActive(true);
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
    //장비창(캐릭터 창) 켜기
    public void EquipmentScreenOn()
    {
        InventoryCanvas.SetActive(true);
        EquipUI.SetActive(true);
    }
    //가방 창 켜기
    public void InvenScreenOn()
    {
        InventoryCanvas.SetActive(true);
        InvenUI.SetActive(true);
    }
    //모든 UI창 끄기
    public void SetAllInactive()
    {
        foreach(GameObject a in CanvasList)
        {
            if (a.activeSelf)
            {
                a.SetActive(false);
            }
        }
    }
    //UI 스크린 변경
    //드랍다운 메뉴로 키기
    public void OpenUIThroughDropdownMenu()
    {
        ChangeScreen(Menu.transform.GetChild(0).GetComponent<TMP_Dropdown>().value);
    }
    //화면 전환 캡슐화
    public void ChangeScreen(int screenNum)
    {
        if (!UIMenuButtons.activeSelf)
            UIMenuButtons.SetActive(true);
        SetAllInactive();
        switch (screenNum)
        {
            case 1:
                EquipmentScreenOn();
                break;
            case 2:
                InvenScreenOn();
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }
    //모든 창 끄고 나가기
    public void CloseUI()
    {
        isUIOn = false;
        //트리거 리셋
        for (int i = 0; i < 5; i++)
        {
            UIMenuButtons.transform.GetChild(i).GetComponent<Button>().animator.ResetTrigger("Selected");
        }
        Menu.transform.GetChild(0).GetComponent<TMP_Dropdown>().value = 0;
        UIMenuButtons.SetActive(false);
        SetAllInactive();
    }

    public void OnOffbadgeCabvas()
    {
        BadgeCanvastOnOff = !BadgeCanvastOnOff;
        BadgeCanvas.SetActive(BadgeCanvastOnOff);
    }

    public void OnOffEncyclopediaCanvas()
    {
        EncyclopediaCanvasOnOff = !EncyclopediaCanvasOnOff;
        EncyclopediaCanvas.SetActive(EncyclopediaCanvasOnOff);
    }

    public void OnOffMinimapCanvas()
    {
        MinimapCanvasOnOff = !MinimapCanvasOnOff;
        MinimapCanvas.SetActive(MinimapCanvasOnOff);
    }

}
