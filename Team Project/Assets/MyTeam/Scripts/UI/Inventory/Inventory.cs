using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    
    //슬롯들
    public Slot.SlotAddition[] slots;
    public Transform slotHolder;
    
    public int slotCount;
    
    private void Awake()
    {
        slots = slotHolder.GetComponentsInChildren<Slot.SlotAddition>();
        slotCount = 4;
        AddSlot();
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


    //public void AddItem(string itemName, int count)
    //{
    //    for (int i = 0; i < slots.Length; i++)
    //        {
    //            //동일한 아이템이 있다면 카운트++
    //            if (slots[i].item != null)
    //            {
    //                if (slots[i].item.itemName.Equals(itemName))
    //                {
    //                    slots[i].SetSlotCount(count);
    //                    return;
    //                }
    //            }
    //        }
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        //빈자리를 찾아서 추가
    //        if (slots[i].item == null)
    //        {
    //            if (slots[i].item.itemName.Equals(""))
    //            {
    //                //slots[i].AddItem(ITEM, count);
    //                return;
    //            }
    //        }
    //    }
    //    print("인벤토리에 빈 공간이 없습니다");
    //}

}
