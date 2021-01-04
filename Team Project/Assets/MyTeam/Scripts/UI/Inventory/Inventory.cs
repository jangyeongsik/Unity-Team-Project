using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : SingletonMonobehaviour<Inventory>
{
    public static bool inventoryActivated = false;
    //슬롯들
    public Slot.SlotAddition[] slots;
    public Transform slotHolder;
    public int slotCount;

    //플레이어 인벤토리
    public PlayerInven pInven = new PlayerInven();
    //아이템 이미지 리스트
    public List<Sprite> itemImages;

    public bool isVisible;
    //0 = 장비, 1 = 재료, 2 = 기타
    public int InvenTabNum;
    public GameObject InvenUI;
    public Toggle[] InvenTabs;
    //드롭다운 메뉴 (정렬)
    public GameObject menu;
    private TMP_Dropdown dropDown;
    private void Start()
    {
        //플레이어 인벤토리 초기화
        pInven = DataManager.Instance.AllInvenData;
        //인벤토리 탭 번호 (0 = 장비, 1 = 재료, 2 = 기타)
        InvenTabNum = 0;
        InvenTabs = InvenUI.GetComponentsInChildren<Toggle>();
        //인벤토리 드롭다운 메뉴 초기화
        dropDown = menu.GetComponent<TMP_Dropdown>();
        //슬롯초기화
        slots = slotHolder.GetComponentsInChildren<Slot.SlotAddition>();
        slotCount = pInven.EquipmentList.Count;
        SetSlotNumber();
        SlotChange(slotCount);

        //시작할 때 높은 등급 순으로 정렬
        DataManager.Instance.SortByIDDecending(InvenTabNum);
        pInven = DataManager.Instance.AllInvenData;
        SetImage();

        isVisible = gameObject.activeSelf;
    }
    private void Update()
    {
        //인벤에 아이템 추가
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AddEquipment("검");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            AddEquipment("활");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            AddEquipment("창");
        }
        isVisible = gameObject.activeSelf;
    }
    //=====================================
    //아이템 슬롯 관련
    //슬롯버튼 사용가능으로 바꾸기
    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < val) slots[i].GetComponent<Button>().interactable = true;
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
                slots[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    //슬롯버튼 한개 잠금해제
    public void AddSlot()
    {
        switch (InvenTabNum)
        {
            case 0:
                slotCount = pInven.EquipmentList.Count;
                break;
            case 1:
                slotCount = pInven.IngredientList.Count;
                break;
            case 2:
                slotCount = pInven.MiscList.Count;
                break;
        }
        SlotChange(slotCount);
    }
    //각 슬롯에 번호 붙여주기
    public void SetSlotNumber()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<ItemInfo>().SetSlotNum(i);
        }
    }
    //=====================================

    //=====================================
    //아이템 관련
    //아이템 이미지 찾아서 추가
    public void SetImage()
    {
        switch (InvenTabNum)
        {
            case 0:
                #region 장비 인벤토리 아이템 개수 따라 표시
                for (int i = 0; i <= pInven.EquipmentList.Count; i++)
                {
                    if (i >= pInven.EquipmentList.Count)
                    {
                        if (i < slots.Length)
                        {
                            for (int j = i; j < slots.Length; j++)
                            {
                                slots[j].transform.GetChild(0).gameObject.SetActive(false);
                                slots[j].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                        break;
                    }
                    slots[i].transform.GetChild(0).gameObject.SetActive(true);
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = itemImages[pInven.EquipmentList[i].itemScriptID]; ;
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                    slots[i].transform.GetChild(2).gameObject.SetActive(true);
                    switch (pInven.EquipmentList[i].itemGrade)
                    {
                        case 1:
                            slots[i].transform.GetChild(2).GetComponent<Image>().color = Color.green;
                            break;
                        case 2:
                            slots[i].transform.GetChild(2).GetComponent<Image>().color = Color.blue;
                            break;
                        case 3:
                            slots[i].transform.GetChild(2).GetComponent<Image>().color = Color.red;
                            break;
                    }
                }
                #endregion
                break;
            case 1:
                #region 재료 인벤토리 아이템 개수 따라 표시
                for (int i = 0; i <= pInven.IngredientList.Count; i++)
                {
                    if (i >= pInven.IngredientList.Count)
                    {
                        if (i < slots.Length)
                        {
                            for (int j = i; j < slots.Length; j++)
                            {
                                slots[j].transform.GetChild(0).gameObject.SetActive(false);
                                slots[j].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                        break;
                    }
                    slots[i].transform.GetChild(0).gameObject.SetActive(true);
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = itemImages[pInven.IngredientList[i].itemScriptID]; ;
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                }
                #endregion
                break;
            case 2:
                #region 기타 인벤토리 아이템 개수 따라 표시
                for (int i = 0; i <= pInven.MiscList.Count; i++)
                {
                    if (i >= pInven.MiscList.Count)
                    {
                        if (i < slots.Length)
                        {
                            for (int j = i; j < slots.Length; j++)
                            {
                                slots[j].transform.GetChild(0).gameObject.SetActive(false);
                                slots[j].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                        break;
                    }
                    slots[i].transform.GetChild(0).gameObject.SetActive(true);
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = itemImages[pInven.MiscList[i].itemScriptID]; ;
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                }
                #endregion
                break;
        }
        
    }
    //아이템 추가
    public void AddEquipment(string itemName)
    {
        DataManager.Instance.AddEquipmentData(GameData.Instance.findEquipment(itemName));
        for (int i = 0; i < pInven.EquipmentList.Count; i++)
        {
            slots[i].GetComponent<ItemInfo>().RefreshCount(true);
        }
        AddSlot();
        SetImage();
    }
    //=====================================
    //아이템탭 교체
    //=====================================
    public void ChangeTabToEquipment()
    {
        InvenTabNum = 0;
        CheckIsSelected();
        ChangeTab();
    }
    public void ChangeTabToIngredient()
    {
        InvenTabNum = 1; 
        CheckIsSelected();
        ChangeTab();
    }
    public void ChangeTabToMisc()
    {
        InvenTabNum = 2;
        CheckIsSelected();
        ChangeTab();
    }
    public void CheckIsSelected()
    {
        if (InvenTabs[InvenTabNum].isOn)
        {
            InvenTabs[InvenTabNum].animator.ResetTrigger("Deselected");
            InvenTabs[InvenTabNum].animator.SetTrigger("Selected");
        }
        else
        {
            InvenTabs[InvenTabNum].animator.ResetTrigger("Selected");
            InvenTabs[InvenTabNum].animator.SetTrigger("Deselected");
        }
    }
    //=====================================
    //아이템 탭에 따라 슬롯 아이템 교체
    //=====================================
    public void ChangeTab()
    {
        AddSlot();
        SetImage();
    }
    //=====================================
    //정렬 탭에 따라 정렬
    //=====================================
    public void Sort()
    {
        switch (dropDown.value)
        {
            case 0:
                DataManager.Instance.SortByIDDecending(InvenTabNum);
                break;
            case 1:
                DataManager.Instance.SortByIDAscending(InvenTabNum);
                break;
            case 2:
                DataManager.Instance.SortByName(InvenTabNum);
                break;
        }
        pInven = DataManager.Instance.AllInvenData;
        SetImage();
    }
}
