using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

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
    public TMP_Text TGrade;
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
        if (pE != null)
        {
            foreach (Equipment a in pE.CurrentEquipmentList)
            {
                SetButtonImage(a);
            }
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
        StringBuilder sb = new StringBuilder();
        Equipment e = new Equipment();
        switch (val)
        {
            case "Helm":
                if (FindItem(EQUIPMENTTYPE.HELM) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                e = FindItem(EQUIPMENTTYPE.HELM);
                //아이템 이미지 받아와 넣기
                TName.text = e.name;
                sb.Append("투구");
                TCategory.text = sb.ToString();
                image.sprite = Inventory.Instance.itemImages[e.itemScriptID];
                sb.Clear();
                sb.AppendFormat("공격력 : {0}\n", e.damage);
                sb.AppendFormat("속도 : {0}\n", e.speed);
                sb.AppendFormat("치명타 피해 : {0}\n", e.critDamage);
                sb.AppendFormat("치명타 확률 : {0}\n", e.critPercent);
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TDescription.text = sb.ToString();
                sb.Clear();
                sb.Append("등급 : ");
                switch (e.itemGrade)
                {
                    case 1:
                        sb.Append("평작");
                        break;
                    case 2:
                        sb.Append("걸작");
                        break;
                    case 3:
                        sb.Append("명작");
                        break;
                }
                TGrade.text = sb.ToString();
                break;
            case "Armor":
                if (FindItem(EQUIPMENTTYPE.ARMOR) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                e = FindItem(EQUIPMENTTYPE.ARMOR);
                //아이템 이미지 받아와 넣기
                TName.text = e.name;
                sb.Append("갑옷");
                TCategory.text = sb.ToString();
                image.sprite = Inventory.Instance.itemImages[e.itemScriptID];
                sb.Clear();
                sb.AppendFormat("공격력 : {0}\n", e.damage);
                sb.AppendFormat("속도 : {0}\n", e.speed);
                sb.AppendFormat("치명타 피해 : {0}\n", e.critDamage);
                sb.AppendFormat("치명타 확률 : {0}\n", e.critPercent);
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TDescription.text = sb.ToString();
                sb.Clear();
                sb.Append("등급 : ");
                switch (e.itemGrade)
                {
                    case 1:
                        sb.Append("평작");
                        break;
                    case 2:
                        sb.Append("걸작");
                        break;
                    case 3:
                        sb.Append("명작");
                        break;
                }
                TGrade.text = sb.ToString();
                break;
            case "Weapon":
                if (FindItem(EQUIPMENTTYPE.WEAPON) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                e = FindItem(EQUIPMENTTYPE.WEAPON);
                //아이템 이미지 받아와 넣기
                TName.text = e.name;
                sb.Append("무기");
                TCategory.text = sb.ToString();
                image.sprite = Inventory.Instance.itemImages[e.itemScriptID];
                sb.Clear();
                sb.AppendFormat("공격력 : {0}\n", e.damage);
                sb.AppendFormat("속도 : {0}\n", e.speed);
                sb.AppendFormat("치명타 피해 : {0}\n", e.critDamage);
                sb.AppendFormat("치명타 확률 : {0}\n", e.critPercent);
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TDescription.text = sb.ToString();
                sb.Clear();
                sb.Append("등급 : ");
                switch (e.itemGrade)
                {
                    case 1:
                        sb.Append("평작");
                        break;
                    case 2:
                        sb.Append("걸작");
                        break;
                    case 3:
                        sb.Append("명작");
                        break;
                }
                TGrade.text = sb.ToString();
                break;
            case "Glove":
                if (FindItem(EQUIPMENTTYPE.GLOVE) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                e = FindItem(EQUIPMENTTYPE.GLOVE);
                //아이템 이미지 받아와 넣기
                TName.text = e.name;
                sb.Append("장갑");
                TCategory.text = sb.ToString();
                image.sprite = Inventory.Instance.itemImages[e.itemScriptID];
                sb.Clear();
                sb.AppendFormat("공격력 : {0}\n", e.damage);
                sb.AppendFormat("속도 : {0}\n", e.speed);
                sb.AppendFormat("치명타 피해 : {0}\n", e.critDamage);
                sb.AppendFormat("치명타 확률 : {0}\n", e.critPercent);
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TDescription.text = sb.ToString();
                sb.Clear();
                sb.Append("등급 : ");
                switch (e.itemGrade)
                {
                    case 1:
                        sb.Append("평작");
                        break;
                    case 2:
                        sb.Append("걸작");
                        break;
                    case 3:
                        sb.Append("명작");
                        break;
                }
                TGrade.text = sb.ToString();
                break;
            case "Boots":
                if (FindItem(EQUIPMENTTYPE.BOOTS) == null)
                {
                    infoScreen.gameObject.SetActive(false);
                    break;
                }
                e = FindItem(EQUIPMENTTYPE.BOOTS);
                //아이템 이미지 받아와 넣기
                TName.text = e.name;
                sb.Append("신발");
                TCategory.text = sb.ToString();
                image.sprite = Inventory.Instance.itemImages[e.itemScriptID];
                sb.Clear();
                sb.AppendFormat("공격력 : {0}\n", e.damage);
                sb.AppendFormat("속도 : {0}\n", e.speed);
                sb.AppendFormat("치명타 피해 : {0}\n", e.critDamage);
                sb.AppendFormat("치명타 확률 : {0}\n", e.critPercent);
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TDescription.text = sb.ToString();
                sb.Clear();
                sb.Append("등급 : ");
                switch (e.itemGrade)
                {
                    case 1:
                        sb.Append("평작");
                        break;
                    case 2:
                        sb.Append("걸작");
                        break;
                    case 3:
                        sb.Append("명작");
                        break;
                }
                TGrade.text = sb.ToString();
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
