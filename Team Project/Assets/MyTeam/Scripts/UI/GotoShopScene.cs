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
    public GameObject CraftCanvas;
    public GameObject LeverCanvas;
    public Temple TempleCanvas;

    public GameObject ItemInfoScreen;
    public GameObject EquipInfoScreen;
    public GameObject IngredientInfoScreen;

    public GameObject InventoryCanvas;
    public GameObject InvenUI;
    public GameObject EquipUI;
    public GameObject UIMenuButtons;
    public Toggle[] Toggles;
    public GameObject Menu;
    public GameObject SkillMenu;
    public GameObject TalkBtn;
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
        GameEventToUI.Instance.skillShop += ShopOn;
        GameEventToUI.Instance.leverOnOff += OnOffLeverPopup;
        GameEventToUI.Instance.templeOnOff += OnOffTemplePopup;
        GameEventToUI.Instance.talkOnOff += TalkOff;
        GameEventToUI.Instance.Event_TalkBox += TalkBox;
        GameEventToUI.Instance.talkButOnOff += Talk_Box_onOff;
        GameEventToUI.Instance.joystick_on += joystickon;
        GameEventToUI.Instance.joystick_off += joystickoff;
        GameEventToUI.Instance.skillShopPush += PushShop;
        GameEventToUI.Instance.skillShopback += BackShop;
        //SceneMgr.Instance.LoadScene("MAP028", "FromMap028 ToMap016");
        if (GameData.Instance.player.tutorial == false)
        {
            SceneMgr.Instance.LoadScene("MAP000", "FromMap000 ToMap000");
        }
        else
        {
            SceneMgr.Instance.LoadScene(GameData.Instance.player.SaveSceneName, GameData.Instance.player.SavePortalName);
        }
        
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
        CanvasList.Add(IngredientInfoScreen);
        Toggles = UIMenuButtons.transform.GetChild(0).GetComponentsInChildren<Toggle>();
        //장착 무기가 있으면 캐릭터 검 이미지 변경
        if (CheckEquipmentExists())
        {
            UIEventToGame.Instance.OnSwordChangeEvent(DataManager.Instance.FindEquipment(EQUIPMENTTYPE.WEAPON).itemGrade);
        }
        //없으면 무조건 0번 무기로
        else
        {
            UIEventToGame.Instance.OnSwordChangeEvent(1);
        }
    }
    #region 장착 아이템에 무기가 있는지 체크
    private bool CheckEquipmentExists()
    {
        if (DataManager.Instance.EquipInvenData.CurrentEquipmentList == null) return false;
        foreach (Equipment e in DataManager.Instance.EquipInvenData.CurrentEquipmentList)
        {
            if (e.equipmentType.Equals(EQUIPMENTTYPE.WEAPON))
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    private void OnDestroy()
    {
        GameEventToUI.Instance.leverOnOff -= OnOffLeverPopup;
        GameEventToUI.Instance.templeOnOff -= OnOffTemplePopup;
        GameEventToUI.Instance.talkOnOff -= TalkOff;
        GameEventToUI.Instance.Event_TalkBox -= TalkBox;
        GameEventToUI.Instance.talkButOnOff -= Talk_Box_onOff;
        GameEventToUI.Instance.joystick_on -= joystickon;
        GameEventToUI.Instance.joystick_off -= joystickoff;
        GameEventToUI.Instance.skillShopPush -= PushShop;
        GameEventToUI.Instance.skillShopback -= BackShop;
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
    public void ShopOn()
    {
        shopCanvas.SetActive(true);
    }

    public void ShopOff()
    {
        shopCanvas.SetActive(false);
    }
    public void Talk_Box_Print()
    {
        joystickoff();
        TalkCanvas.SetActive(true);
        GameEventToUI.Instance.OnNpc_name_Setting(GameData.Instance.Talk_Find_index(GameEventToUI.Instance.OnEventTalk_id()));
        GameEventToUI.Instance.Onnpc_talk_setting(GameData.Instance.Talk_Find_index(GameEventToUI.Instance.OnEventTalk_id()));

        GameEventToUI.Instance.OnNpc_name_print();
        GameEventToUI.Instance.Onnpc_talk_print();

        GameEventToUI.Instance.OnTalkBtnEvent();
        
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
        GameEventToUI.Instance.OnNpc_name_Setting(GameData.Instance.Talk_Find_index(id));
        GameEventToUI.Instance.Onnpc_talk_setting(GameData.Instance.Talk_Find_index(id));
        
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
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
    }
    public void LogOut()
    {
        if (!GameData.Instance.playerData[GameData.Instance.playerIdx].tutorial)
        {
            DataManager.Instance.DeleteData();
        }
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        SceneMgr.CurrentSceneName = "";
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
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭1");
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
    public void OnOffLeverPopup(bool isOn, string name, string description)
    {
        LeverCanvas.SetActive(isOn);
        LeverCanvas.transform.GetChild(3).GetComponent<TMP_Text>().text = name;
        LeverCanvas.transform.GetChild(4).GetComponent<TMP_Text>().text = description;
    }
    public void OnOffTemplePopup(bool isOn, bool isActivated, string name)
    {
        TempleCanvas.gameObject.SetActive(isOn);
        if (!isActivated)
        {
            TempleCanvas.TempleSelectUI.SetActive(isOn);
            TempleCanvas.PrayUI.SetActive(!isOn);
            TempleCanvas.TempleSelectUINameText.text = name;
        }
        else
        {
            TempleCanvas.TempleSelectUI.SetActive(!isOn);
            TempleCanvas.PrayUI.SetActive(isOn);
            TempleCanvas.PrayUINameText.text = name;
        }
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
    public void Talk_Box_onOff(bool isOn)
    {
        TalkBtn.SetActive(isOn);
    }

    public void PushShop()
    {
        GameEventToUI.Instance.skillShop += ShopOn;
    }
    public void BackShop()
    {
        GameEventToUI.Instance.skillShop -= ShopOn;
    }
}
