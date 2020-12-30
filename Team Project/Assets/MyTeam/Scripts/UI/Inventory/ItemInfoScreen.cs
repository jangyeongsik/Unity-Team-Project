using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoScreen : MonoBehaviour
{
    public int slotNum;
    public Image itemInfoImage;
    public GameObject EquipUI;
    
    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
    public void EquipItem()
    {
        EquipUI.GetComponent<EquipItem>().Equip(Inventory.Instance.pInven.EquipmentList[slotNum]);
    }
}
