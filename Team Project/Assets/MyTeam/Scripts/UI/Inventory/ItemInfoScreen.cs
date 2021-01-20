using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoScreen : MonoBehaviour
{
    public EquipItem eI;
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
        gameObject.SetActive(false);
    }
    public void MoveToInventoryScreen()
    {
        goToShop.ChangeScreen(2);
        gameObject.SetActive(false);
    }
    public void UnEquipItem()
    {
        DataManager.Instance.RemoveEquipInvenData(e);
        Inventory.Instance.AddEquipment(e.ID, 1);
        CloseTab();
        switch (e.equipmentType)
        {
            case EQUIPMENTTYPE.WEAPON:
                eI.WeaponBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case EQUIPMENTTYPE.ARMOR:
                eI.ArmorBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case EQUIPMENTTYPE.HELM:
                eI.HelmBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case EQUIPMENTTYPE.GLOVE:
                eI.GloveBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case EQUIPMENTTYPE.BOOTS:
                eI.BootsBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
        }
        
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
