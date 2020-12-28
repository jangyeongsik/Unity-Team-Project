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

    public DataManager dataManager = new DataManager();
    
    private void Start()
    {
        //슬롯초기화
        slots = slotHolder.GetComponentsInChildren<Slot.SlotAddition>();
        slotCount = 4;
        AddSlot();
        SetSlotNumber();

        //플레이어 인벤토리 초기화
        PlayerInventory a = new PlayerInventory();
        pInven = JsonManager.Instance.LoadJsonFile<PlayerInven>(Application.dataPath, "InvenData/playerInvenData");
        SetImage();

        ////테스트용 검 1개 추가
        //a.ID = 1;
        //a.name = "검";
        //a.scriptName = 0;
        //a.count = 1;
        //a.itemCategory = ItemCategory.Equipment;
        //pInven.ListData.Add(a);
        //slots[0].GetComponent<ItemInfo>().RefreshCount(true);
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
            AddItem("활", ItemCategory.Equipment);
            AddItem("검", ItemCategory.Equipment);
        }
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
        slotCount++;
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
            if (pInven.ListData[i].image == null) pInven.ListData[i].image = itemImages[pInven.ListData[i].scriptName];
            slots[i].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true);
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = pInven.ListData[i].image;
            slots[i].GetComponent<ItemInfo>().RefreshCount(true);
        }
    }

    //아이템 추가
    public void AddItem(string itemName, ItemCategory itemType)
    {
        int i = 0;
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
                case ItemCategory.Equipment:
                    pInven.ListData.Add(GameData.Instance.findEquipment(itemName));
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                    break;
                case ItemCategory.Consumable:
                    break;
                case ItemCategory.Ingredient:
                    break;
                case ItemCategory.Misc:
                    break;
                default:
                    break;
            }
        }
        SetImage();

        //아이템 추가할 때 마다 데이터 저장
        JsonManager.Instance.CreateJsonFile(Application.dataPath, "InvenData/playerInvenData", pInven);
    }
    //=====================================
}
