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
        for(int i = 0; i < DataManager.Instance.EquipInvenData.CurrentEquipmentList.Count; ++i)
        {
            GameData.Instance.player.damage = 1;
            GameData.Instance.player.damage += (int)DataManager.Instance.EquipInvenData.CurrentEquipmentList[i].damage;

            GameData.Instance.player.movespeed = 1;
            GameData.Instance.player.movespeed += (int)DataManager.Instance.EquipInvenData.CurrentEquipmentList[i].speed;

            GameData.Instance.player.counter_judgement = 1;
            GameData.Instance.player.counter_judgement += (int)DataManager.Instance.EquipInvenData.CurrentEquipmentList[i].counterJudgement;
        }
        gameObject.SetActive(false);
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
