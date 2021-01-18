using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GotoShopScene : MonoBehaviour
{

    public GameObject joystick;
    public GameObject shopCanvas;
    public GameObject TalkCanvas;
    public GameObject miniMapCanvas;

    public GameObject BadgeCanvas;
    public GameObject EncyclopediaCanvas;
    public GameObject MinimapCanvas;
    public GameObject SettingCanvas;
    public GameObject QuitGameScreen;
    public GameObject TPCanvas;
    public GameObject TPOperateCanvas;
    public GameObject CraftCanvas;
    public GameObject LeverCanvas;

    public GameObject ItemInfoScreen;
    public GameObject EquipInfoScreen;

    public GameObject InventoryCanvas;
    public GameObject InvenUI;
    public GameObject EquipUI;
    public GameObject UIMenuButtons;
    public Toggle[] Toggles;
    public GameObject Menu;
    public GameObject SkillMenu;

    public GameObject MinMapCam;

    List<GameObject> CanvasList = new List<GameObject>();

    private bool minMapOpen = false;
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
        GameEventToUI.Instance.leverOnOff += OnOffLeverPopup;
        GameEventToUI.Instance.talkOnOff += TalkOff;
        GameEventToUI.Instance.Event_TalkBox += TalkBox;
        
        GameEventToUI.Instance.joystick_on += joystickon;
        //SceneManager.LoadScene("MAP001", LoadSceneMode.Additive);
        //SceneMgr.Instance.LoadScene("MAP001", "MAP001");
        SceneMgr.Instance.LoadScene(GameData.Instance.player.SaveSceneName, GameData.Instance.player.SavePortalName);
        //SceneMgr.Instance.LoadScene("MAP006", "FromMap006 ToMap005");
        //SceneMgr.Instance.LoadScene("MAP025", "FromMap025 ToMap006");
        GameData.Instance.player.SetGravity(0.9f); 
    }

    private void Start()
    {
        
        CanvasList.Add(shopCanvas);
        CanvasList.Add(TalkCanvas);
        CanvasList.Add(miniMapCanvas);
        CanvasList.Add(InventoryCanvas);
        CanvasList.Add(SettingCanvas);
        CanvasList.Add(CraftCanvas);
        CanvasList.Add(InvenUI);
        CanvasList.Add(EquipUI);
        CanvasList.Add(SkillMenu);
        CanvasList.Add(ItemInfoScreen);
        CanvasList.Add(EquipInfoScreen);
        Toggles = UIMenuButtons.transform.GetChild(0).GetComponentsInChildren<Toggle>();
        UIEventToGame.Instance.OnSwordChangeEvent(DataManager.Instance.FindEquipment(EQUIPMENTTYPE.WEAPON).itemGrade);
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
    public void TalkOn()
    {
        if (GameEventToUI.Instance.onEventPlayer_Trigger())
        {
            joystickoff();
            TalkCanvas.SetActive(true);
            if (GameData.Instance.player.tutorial == false)
            {
                GameEventToUI.Instance.OnNpc_name_Setting(Talk_Find_index(8000));
                GameEventToUI.Instance.Onnpc_talk_setting(Talk_Find_index(8000));

                GameEventToUI.Instance.OnNpc_name_print();
                GameEventToUI.Instance.Onnpc_talk_print();
                GameData.Instance.player.tutorial = true;
            }
            else
            {
                GameEventToUI.Instance.OnNpc_name_Setting(Talk_Find_index(8006));
                GameEventToUI.Instance.Onnpc_talk_setting(Talk_Find_index(8006));

                GameEventToUI.Instance.OnNpc_name_print();
                GameEventToUI.Instance.Onnpc_talk_print();
            }
        }
        
    }

    public void MinMapOpen()
    {
        minMapOpen = !minMapOpen;
        MinMapCam.SetActive(minMapOpen);
        UIEventToGame.Instance.OnUIEventMinMap();
    }

    public void TalkBox(int id)
    {
        joystickoff();
        TalkCanvas.SetActive(true);
        GameEventToUI.Instance.OnNpc_name_Setting(Talk_Find_index(id));
        GameEventToUI.Instance.Onnpc_talk_setting(Talk_Find_index(id));

        GameEventToUI.Instance.OnNpc_name_print();
        GameEventToUI.Instance.Onnpc_talk_print();
    }

    public void TalkOff()
    {
        TalkCanvas.SetActive(false);
    }
    public void MiniMapOn(bool isOn)
    {
        miniMapCanvas.SetActive(isOn);
    }
    public int Talk_Find_index(int id)
    {
        for(int i = 0; i < GameData.Instance.data.Count;i++)
        {
            if (GameData.Instance.data[i].id == id) return i;
        }
        return 999;
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
        EquipUI.GetComponent<EquipItem>().RefreshAllImages();
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
        Inventory.Instance.ChangeTab(0);
        Toggles[1].GetComponent<MenuButtonsController>().SetIsOn();
    }
    //제작 창 켜기
    public void CraftScreenOn()
    {
        if (!CraftCanvas.activeSelf)
        {
            CraftCanvas.SetActive(true);
        }
        CraftCanvas.GetComponent<CraftController>().SetEquipItem();
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
        Toggles[4].GetComponent<MenuButtonsController>().SetIsOn();
    }
    public void QuitGameScreenOn()
    {
        QuitGameScreen.SetActive(true);
    }
    public void LogOut()
    {
        GameData.Instance.PlayerSave();
        Inventory.Instance.Destroy();
        SceneManager.LoadScene("GameStartScene");
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

        //무기 체인지
        UIEventToGame.Instance.OnSwordChangeEvent(DataManager.Instance.FindEquipment(EQUIPMENTTYPE.WEAPON).itemGrade);
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
    public void OnOffLeverPopup(bool isOn, string name, string description)
    {
        LeverCanvas.SetActive(isOn);
        LeverCanvas.transform.GetChild(3).GetComponent<TMP_Text>().text = name;
        LeverCanvas.transform.GetChild(4).GetComponent<TMP_Text>().text = description;
    }

    public void joystickon()
    {
        joystick.SetActive(true);
    }
    public void joystickoff()
    {
        UIEventToGame.Instance.OnUiEventJoystickSetting();
        joystick.SetActive(false);

    }

    public void Next_talk()
    {
        GameEventToUI.Instance.Onnpc_talk_Next();
    }
}
