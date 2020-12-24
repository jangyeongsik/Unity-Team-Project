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
    public List<PlayerInventory> pInven = new List<PlayerInventory>();
    public List<Sprite> itemImages;
    public int slotCount;

    private void Awake()
    {
        slots = slotHolder.GetComponentsInChildren<Slot.SlotAddition>();
        slotCount = 4;
        AddSlot();
        SetSlotNumber();
    }

    private void Start()
    {
        PlayerInventory a = new PlayerInventory();
        a.ID = 1;
        a.name = "검";
        a.scriptName = 0;
        a.count = 1;
        a.itemCategory = ItemCategory.Equipment;
        pInven.Add(a);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < pInven.Count; i++)
            {
                pInven[i].image = itemImages[pInven[i].scriptName];
                slots[i].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = pInven[i].image;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddItem("활", ItemCategory.Equipment);
            
        }
    }
    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < val) slots[i].GetComponent<Button>().interactable = true;
            else slots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void AddSlot()
    {
        slotCount++;
        SlotChange(slotCount);
    }

    public void SetSlotNumber()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<ItemInfo>().SetSlotNum(i);
        }
    }

    public void AddItem(string itemName, ItemCategory itemType)
    {
        int i = 0;
        for (; i < pInven.Count; i++)
        {
            if (pInven[i].name.Equals(itemName))
            {
                pInven[i].count++;
                slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                break;
            }
        }

        if (i >= pInven.Count)
        {
            switch (itemType)
            {
                case ItemCategory.Equipment:
                    pInven.Add(GameData.Instance.findEquipment(itemName));
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
    }
}
