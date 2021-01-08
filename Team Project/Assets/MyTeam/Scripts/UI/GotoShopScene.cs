using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GotoShopScene : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject TalkCanvas;
    public GameObject miniMapCanvas;

    public GameObject BadgeCanvas;
    public GameObject EncyclopediaCanvas;
    public GameObject MinimapCanvas;
    public GameObject SettingCanvas;
    public GameObject TPCanvas;
    public GameObject TPOperateCanvas;
    public GameObject CraftCanvas;

    public GameObject InventoryCanvas;
    public GameObject InvenUI;
    public GameObject EquipUI;
    public GameObject UIMenuButtons;
    public Toggle[] Toggles;
    public GameObject Menu;
    public GameObject SkillMenu;


    List<GameObject> CanvasList = new List<GameObject>();

    private bool isUIOn = false;
    private bool startTalking = false;
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
        GameEventToUI.Instance.TPOpearteOnOff += OnOffTPOperateCanvas;
        GameEventToUI.Instance.TPCanvasOnOff += OnOffTPCanvas;
        //SceneManager.LoadScene("MAP001", LoadSceneMode.Additive);
        SceneMgr.Instance.LoadScene("MAP004", "ToMap003");
        GameData.Instance.player.SetGravity(0.9f); 
    }

    private void Start()
    {
        GameObject text = TalkCanvas.transform.Find("sorse").gameObject;
        talk = text.GetComponent<Text>();

        CanvasList.Add(shopCanvas);
        CanvasList.Add(TalkCanvas);
        CanvasList.Add(miniMapCanvas);
        CanvasList.Add(InventoryCanvas);
        CanvasList.Add(SettingCanvas);
        CanvasList.Add(CraftCanvas);
        CanvasList.Add(InvenUI);
        CanvasList.Add(EquipUI);
        CanvasList.Add(SkillMenu);
        Toggles = UIMenuButtons.transform.GetChild(0).GetComponentsInChildren<Toggle>();
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
            if (GameData.Instance.data[i].id == id && GameData.Instance.data[i].name.Equals(NpcName))
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
        if (startTalking)
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
        if (!InventoryCanvas.activeSelf)
        {
            InventoryCanvas.SetActive(true);
        }
        if (!EquipUI.activeSelf)
        {
            EquipUI.SetActive(true);
        }
        Toggles[0].GetComponent<MenuButtonsController>().SetIsOn();
    }
    //가방 창 켜기
    public void InvenScreenOn()
    {
        if (!InventoryCanvas.activeSelf)
        {
            InventoryCanvas.SetActive(true);
        }
        if (!InvenUI.activeSelf)
        {
            InvenUI.SetActive(true);
        }
        Toggles[1].GetComponent<MenuButtonsController>().SetIsOn();
    }
    //제작 창 켜기
    public void CraftScreenOn()
    {
        if (!CraftCanvas.activeSelf)
        {
            CraftCanvas.SetActive(true);
        }
        Toggles[2].GetComponent<MenuButtonsController>().SetIsOn();
    }
    //스킬 창 켜기
    public void SkillScreenOn()
    {
        SkillMenu.SetActive(true);
        Toggles[3].GetComponent<MenuButtonsController>().SetIsOn();
    }
    //설정 창 켜기
    public void SettingScreenOn()
    {
        SettingCanvas.SetActive(true);
    }
    //모든 UI창 끄기
    public void SetAllInactive()
    {
        for (int i = 0; i < CanvasList.Count; i++)
        {
            CanvasList[i].SetActive(false);
        }
    }
    //UI 스크린 변경
    //드랍다운 메뉴로 키기
    public void OpenUIThroughDropdownMenu()
    {
        ChangeScreen(Menu.transform.GetChild(0).GetComponent<TMP_Dropdown>().value);
    }
    //화면 전환
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
                CraftScreenOn();
                break;
            case 4:
                SkillScreenOn();
                break;
            case 5:
                SettingScreenOn();
                break;
        }
    }
    //모든 창 끄고 나가기
    public void CloseUI()
    {
        StartCoroutine(CloseUICoroutine());
    }
    public IEnumerator CloseUICoroutine()
    {
        isUIOn = false;
        //트리거 리셋
        for (int i = 0; i < 5; i++)
        {
            Toggles[i].GetComponent<MenuButtonsController>().ResetTrigger();
        }
        yield return new WaitForSeconds(0.5f);
        Menu.transform.GetChild(0).GetComponent<TMP_Dropdown>().value = 0;
        SetAllInactive();
        UIMenuButtons.SetActive(false);
    }

    public void OnOffBadgeCanvas()
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
    public void OnOffTPOperateCanvas(bool isOn)
    {
        TPOperateCanvas.SetActive(isOn);
    }
    public void OnOffTPCanvas(bool isOn)
    {
        TPCanvas.SetActive(isOn);
    }

}
