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
    EquipItemStatInfo EquipItemStatInfo;
    private void Awake()
    {
        goToShop = UIManager.GetComponent<GotoShopScene>();
    }
    private void Start()
    {
        EquipItemStatInfo = FindObjectOfType<EquipItemStatInfo>();
    }
    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
    public void EquipItem()
    {
        EquipUI.GetComponent<EquipItem>().Equip(DataManager.Instance.AllInvenData.EquipmentList[slotNum]);
        GameData.Instance.player.damage = 1;
        GameData.Instance.player.movespeed = 1;
        GameData.Instance.player.counter_judgement = 1;

        for (int i = 0; i < DataManager.Instance.EquipInvenData.CurrentEquipmentList.Count; ++i)
        {
            GameData.Instance.player.damage += (int)DataManager.Instance.EquipInvenData.CurrentEquipmentList[i].damage;

            GameData.Instance.player.movespeed += (int)DataManager.Instance.EquipInvenData.CurrentEquipmentList[i].speed;

            GameData.Instance.player.counter_judgement += (int)DataManager.Instance.EquipInvenData.CurrentEquipmentList[i].counterJudgement;
        }
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
        
        GameData.Instance.player.damage -= (int)e.damage;

        GameData.Instance.player.movespeed -= (int)e.speed;

        GameData.Instance.player.counter_judgement -= (int)e.counterJudgement;

        EquipItemStatInfo.SetText();
    }
    public void MoveToCraftScreen()
    {
        Equipment temp = DataManager.Instance.AllInvenData.EquipmentList[slotNum];
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
