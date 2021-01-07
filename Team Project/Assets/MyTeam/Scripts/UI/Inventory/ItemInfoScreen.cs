using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoScreen : MonoBehaviour
{
    public int slotNum;
    public Image itemInfoImage;
    public GameObject EquipUI;
    public GameObject UIManager;
    private GotoShopScene goToShop;
    private void Awake()
    {
        goToShop = UIManager.GetComponent<GotoShopScene>();
    }
    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
    public void EquipItem()
    {
        EquipUI.GetComponent<EquipItem>().Equip(Inventory.Instance.pInven.EquipmentList[slotNum]);
    }
    public void MoveToInventoryScreen()
    {
        goToShop.ChangeScreen(2);
        gameObject.SetActive(false);
    }
    public void MoveToCraftScreen()
    {
        Equipment temp = Inventory.Instance.pInven.EquipmentList[slotNum];
        GameObject craftCanvas = goToShop.CraftCanvas;
        craftCanvas.GetComponent<CraftController>().SetCraftTree(temp);
        goToShop.ChangeScreen(3);
        gameObject.SetActive(false);
    }
}
