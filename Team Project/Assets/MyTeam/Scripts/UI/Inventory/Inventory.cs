using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void Start()
    {
        //플레이어 인벤토리 초기화
        pInven = JsonManager.Instance.LoadJsonFile<PlayerInven>(Application.dataPath, "/MyTeam/Resources/PlayerInvenData");

        //슬롯초기화
        slots = slotHolder.GetComponentsInChildren<Slot.SlotAddition>();
        slotCount = pInven.EquipmentList.Count;
        SetSlotNumber();
        SlotChange(slotCount);
        SetImage();

        isVisible = gameObject.activeSelf;
        InvenTabNum = 0;
    }
    private void Update()
    {
        //이미지 찾아서 넣어주는 코드
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetImage();
        }
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
        Debug.Log(pInven.EquipmentList.Count);
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
            else slots[i].GetComponent<Button>().interactable = false;
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
        for (int i = 0; i < pInven.EquipmentList.Count; i++)
        {
            slots[i].transform.GetChild(0).gameObject.SetActive(true);
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = itemImages[pInven.EquipmentList[i].itemScriptID]; ;
            slots[i].GetComponent<ItemInfo>().RefreshCount(true);
        }
    }
    //아이템 추가
    public void AddEquipment(string itemName)
    {
        DataManager.Instance.AddEquipmentData(GameData.Instance.findEquipment(itemName));
        pInven = DataManager.Instance.AllInvenData;
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
    }
    public void ChangeTabToIngredient()
    {
        InvenTabNum = 1;
    }
    public void ChangeTabToMisc()
    {
        InvenTabNum = 2;
    }
    //=====================================
}
