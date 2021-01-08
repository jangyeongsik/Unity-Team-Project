using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipItem : MonoBehaviour
{
    PlayerEquipment pE = new PlayerEquipment();
    public Button HelmBtn;
    public Button ArmorBtn;
    public Button WeaponBtn;
    public Button GloveBtn;
    public Button BootsBtn;

    public Canvas infoScreen;
    public Image image;
    public Transform infoUI;
    public TMP_Text TName;
    public TMP_Text TCategory;
    public TMP_Text TDescription;
    public TMP_Text TStat;
    public TMP_Text TSubStat;
    private void Start()
    {
        RefreshAllImages();
    }

    public void Equip(Equipment _item)
    {
        int i = 0;
        Equipment temp = _item;
        pE = DataManager.Instance.EquipInvenData;
        for (; i < pE.CurrentEquipmentList.Count; i++)
        {
            if (pE.CurrentEquipmentList[i].equipmentType.Equals(_item.equipmentType))
            {
                DataManager.Instance.AddEquipmentData(pE.CurrentEquipmentList[i], 1);
                DataManager.Instance.RemoveEquipInvenData(pE.CurrentEquipmentList[i]);

                DataManager.Instance.AddEquipInvenData(_item);
                DataManager.Instance.RemoveEquipmentData(_item);

                pE = DataManager.Instance.EquipInvenData;
                Inventory.Instance.pInven = DataManager.Instance.AllInvenData;
                Inventory.Instance.AddSlot();
                Inventory.Instance.SetImage();
                SetButtonImage(_item);
                break;
            }
        }
        if (i >= pE.CurrentEquipmentList.Count)
        {
            DataManager.Instance.AddEquipInvenData(_item);
            DataManager.Instance.RemoveEquipmentData(_item);
            pE = DataManager.Instance.EquipInvenData;
            Inventory.Instance.pInven = DataManager.Instance.AllInvenData;
            Inventory.Instance.AddSlot();
            Inventory.Instance.SetImage();
            SetButtonImage(_item);
        }
        Inventory.Instance.SetImage();
    }
    public void RefreshAllImages()
    {
        pE = DataManager.Instance.EquipInvenData;
        foreach (Equipment a in pE.CurrentEquipmentList)
        {
            SetButtonImage(a);
        }
    }
    public void SetButtonImage(Equipment _item)
    {
        switch (_item.equipmentType)
        {
            case EQUIPMENTTYPE.WEAPON:
                WeaponBtn.transform.GetChild(0).gameObject.SetActive(true);
                WeaponBtn.transform.GetChild(0).GetComponent<Image>().sprite = Inventory.Instance.itemImages[_item.itemScriptID];
                break;
            case EQUIPMENTTYPE.ARMOR:
                ArmorBtn.transform.GetChild(0).gameObject.SetActive(true);
                ArmorBtn.transform.GetChild(0).GetComponent<Image>().sprite = Inventory.Instance.itemImages[_item.itemScriptID];
                break;
            case EQUIPMENTTYPE.HELM:
                HelmBtn.transform.GetChild(0).gameObject.SetActive(true);
                HelmBtn.transform.GetChild(0).GetComponent<Image>().sprite = Inventory.Instance.itemImages[_item.itemScriptID];
                break;
            case EQUIPMENTTYPE.GLOVE:
                GloveBtn.transform.GetChild(0).gameObject.SetActive(true);
                GloveBtn.transform.GetChild(0).GetComponent<Image>().sprite = Inventory.Instance.itemImages[_item.itemScriptID];
                break;
            case EQUIPMENTTYPE.BOOTS:
                BootsBtn.transform.GetChild(0).gameObject.SetActive(true);
                BootsBtn.transform.GetChild(0).GetComponent<Image>().sprite = Inventory.Instance.itemImages[_item.itemScriptID];
                break;
        }
    }
    public void ShowEquipItemInfo(string val)
    {
        if (!infoScreen.gameObject.activeSelf)
        {
            infoScreen.gameObject.SetActive(true);
        }
        switch (val)
        {
            case "Helm":
                if (FindItem(EQUIPMENTTYPE.HELM) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                Equipment temp = FindItem(EQUIPMENTTYPE.HELM);
                infoScreen.GetComponent<ItemInfoScreen>().e = temp;
                //아이템 이미지 받아와 넣기
                TName.text = temp.name;
                TCategory.text = temp.equipmentType.ToString();
                image.sprite = HelmBtn.transform.GetChild(0).GetComponent<Image>().sprite;
                //아이템 설명 TDescription.text = data.equipmentData[slotNum].;
                TStat.text = "Grade :  " + temp.itemGrade;
                TSubStat.text = "ID : " + temp.ID;
                break;
            case "Armor":
                if (FindItem(EQUIPMENTTYPE.ARMOR) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                temp = FindItem(EQUIPMENTTYPE.ARMOR);
                infoScreen.GetComponent<ItemInfoScreen>().e = temp;
                //아이템 이미지 받아와 넣기
                TName.text = temp.name;
                TCategory.text = temp.equipmentType.ToString();
                image.sprite = ArmorBtn.transform.GetChild(0).GetComponent<Image>().sprite;
                //아이템 설명 TDescription.text = data.equipmentData[slotNum].;
                TStat.text = "Grade :  " + temp.itemGrade;
                TSubStat.text = "ID : " + temp.ID;
                break;
            case "Weapon":
                if (FindItem(EQUIPMENTTYPE.WEAPON) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                temp = FindItem(EQUIPMENTTYPE.WEAPON);
                infoScreen.GetComponent<ItemInfoScreen>().e = temp;
                //아이템 이미지 받아와 넣기
                TName.text = temp.name;
                TCategory.text = temp.equipmentType.ToString();
                image.sprite = WeaponBtn.transform.GetChild(0).GetComponent<Image>().sprite;
                //아이템 설명 TDescription.text = data.equipmentData[slotNum].;
                TStat.text = "Grade :  " + temp.itemGrade;
                TSubStat.text = "ID : " + temp.ID;
                break;
            case "Glove":
                if (FindItem(EQUIPMENTTYPE.GLOVE) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                temp = FindItem(EQUIPMENTTYPE.GLOVE);
                infoScreen.GetComponent<ItemInfoScreen>().e = temp;
                //아이템 이미지 받아와 넣기
                TName.text = temp.name;
                TCategory.text = temp.equipmentType.ToString();
                image.sprite = GloveBtn.transform.GetChild(0).GetComponent<Image>().sprite;
                //아이템 설명 TDescription.text = data.equipmentData[slotNum].;
                TStat.text = "Grade :  " + temp.itemGrade;
                TSubStat.text = "ID : " + temp.ID;
                break;
            case "Boots":
                if (FindItem(EQUIPMENTTYPE.BOOTS) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                temp = FindItem(EQUIPMENTTYPE.BOOTS);
                infoScreen.GetComponent<ItemInfoScreen>().e = temp;
                //아이템 이미지 받아와 넣기
                TName.text = temp.name;
                TCategory.text = temp.equipmentType.ToString();
                image.sprite = BootsBtn.transform.GetChild(0).GetComponent<Image>().sprite;
                //아이템 설명 TDescription.text = data.equipmentData[slotNum].;
                TStat.text = "Grade :  " + temp.itemGrade;
                TSubStat.text = "ID : " + temp.ID;
                break;
        }
    }
    public Equipment FindItem(EQUIPMENTTYPE equipType)
    {
        foreach(Equipment a in pE.CurrentEquipmentList)
        {
            if (a.equipmentType == equipType)
            {
                return a;
            }
        }
        return null;
    }
}
[Serializable]
public class PlayerEquipment
{
    public List<Equipment> CurrentEquipmentList = new List<Equipment>();
}
