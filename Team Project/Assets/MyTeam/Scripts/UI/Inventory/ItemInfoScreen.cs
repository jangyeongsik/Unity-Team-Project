using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoScreen : MonoBehaviour
{
    public int slotNum;
    public Image itemInfoImage;
    public Equipment e;
    public Ingredient ing;
    public Misc m;
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
        goToShop.ChangeScreen(3);
        goToShop.CraftCanvas.GetComponent<CraftController>().SetCraftTree(temp);
        gameObject.SetActive(false);
    }
    public void MoveToCraftScreen(Equipment e)
    {
        goToShop.ChangeScreen(3);
        goToShop.CraftCanvas.GetComponent<CraftController>().SetCraftTree(e);
        gameObject.SetActive(false);
    }
    public void MoveToCraftScreenFromEquipUI()
    {
        goToShop.ChangeScreen(3);
        goToShop.CraftCanvas.GetComponent<CraftController>().SetCraftTree(e);
        gameObject.SetActive(false);
    }
}
