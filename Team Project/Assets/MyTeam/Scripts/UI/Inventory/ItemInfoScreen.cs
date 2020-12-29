using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoScreen : MonoBehaviour
{
    public int slotNum;
    public GameObject EquipUI;
    
    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
    public void EquipItem()
    {
        EquipUI.GetComponent<PlayerEquipment>().EquipItem(Inventory.Instance.pInven.ListData[slotNum]);
    }
}
