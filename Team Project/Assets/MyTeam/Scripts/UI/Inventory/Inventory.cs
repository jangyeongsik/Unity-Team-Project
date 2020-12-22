using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    //필요한 컴포넌트
    [SerializeField]
    private GameObject goInventoryBase;
    [SerializeField]
    private GameObject goSlotsParent;

    //슬롯들
    private Slot[] slots;

    private void Awake()
    {
        
    }

    private void Start()
    {
        slots = goSlotsParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void CloseInventory()
    {
        goInventoryBase.SetActive(false);
    }

    private void OpenInventory()
    {
        goInventoryBase.SetActive(true);
    }

   

    public void AddItem(string itemName, int count)
    {
        for (int i = 0; i < slots.Length; i++)
            {
                //동일한 아이템이 있다면 카운트++
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName.Equals(itemName))
                    {
                        slots[i].SetSlotCount(count);
                        return;
                    }
                }
            }
        for (int i = 0; i < slots.Length; i++)
        {
            //빈자리를 찾아서 추가
            if (slots[i].item == null)
            {
                if (slots[i].item.itemName.Equals(""))
                {
                    //slots[i].AddItem(ITEM, count);
                    return;
                }
            }
        }
        print("인벤토리에 빈 공간이 없습니다");
    }
}
