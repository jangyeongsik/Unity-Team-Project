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

    public DataManager dataManager = DataManager.Instance;
    public bool isVisible;
    
    private void Start()
    {
        //플레이어 인벤토리 초기화
        pInven = JsonManager.Instance.LoadJsonFile<PlayerInven>(Application.dataPath, "/MyTeam/Resources/playerInvenData");

        //슬롯초기화
        slots = slotHolder.GetComponentsInChildren<Slot.SlotAddition>();
        slotCount = pInven.ListData.Count;
        SetSlotNumber();
        SlotChange(slotCount);
        SetImage();

        isVisible = gameObject.activeSelf;
    }
    private void Update()
    {
        //이미지 찾아서 넣어주는 코드
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetImage();
        }
        //인벤에 아이템 추가
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddItem("활", ITEMCATEGORY.EQUIPMENT);
            AddItem("검", ITEMCATEGORY.EQUIPMENT);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AddItem("창", ITEMCATEGORY.EQUIPMENT);
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
            else slots[i].GetComponent<Button>().interactable = false;
        }
    }
    //슬롯버튼 한개 잠금해제
    public void AddSlot()
    {
        slotCount = pInven.ListData.Count;
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
        for (int i = 0; i < pInven.ListData.Count; i++)
        {
            pInven.ListData[i].image = itemImages[pInven.ListData[i].scriptName];
            slots[i].transform.GetChild(0).gameObject.SetActive(true);
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = pInven.ListData[i].image;
            slots[i].GetComponent<ItemInfo>().RefreshCount(true);
        }
    }
    //아이템 추가
    public void AddItem(string itemName, ITEMCATEGORY itemType)
    {
        int i = 0;
        //아이템창에 하나도 없으면 바로 추가
        if (pInven.ListData.Count == 0)
        {
            switch (itemType)
            {
                case ITEMCATEGORY.EQUIPMENT:
                    pInven.ListData.Add(GameData.Instance.findEquipmentAsPlayerInventoryItem(itemName));
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                    break;
                case ITEMCATEGORY.INGREDIENT:
                    break;
                case ITEMCATEGORY.CONSUMABLE:
                    break;
                case ITEMCATEGORY.MISC:
                    break;
                default:
                    break;
            }
            AddSlot();
            return;
        }
        //동일한 아이템이 있으면 카운트 ++
        for (; i < pInven.ListData.Count; i++)
        {
            if (pInven.ListData[i].name.Equals(itemName))
            {
                pInven.ListData[i].count++;
                slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                break;
            }
        }
        //동일한 아이템이 없으면 새 칸에 추가
        if (i >= pInven.ListData.Count)
        {
            switch (itemType)
            {
                case ITEMCATEGORY.EQUIPMENT:
                    pInven.ListData.Add(GameData.Instance.findEquipmentAsPlayerInventoryItem(itemName));
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                    break;
                case ITEMCATEGORY.INGREDIENT:
                    break;
                case ITEMCATEGORY.CONSUMABLE:
                    break;
                case ITEMCATEGORY.MISC:
                    break;
                default:
                    break;
            }
            AddSlot();
        }
        SetImage();

        //아이템 추가할 때 마다 데이터 저장
        JsonManager.Instance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/playerInvenData", pInven);
    }
    //=====================================
}
