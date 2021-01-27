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
    public TMP_Text TStat;
    public TMP_Text TGrade;
    public TMP_Text TDescription;
    private void Start()
    {
        RefreshAllImages();
    }

    public void SetEquipmentDescription(StringBuilder sb, int itemID)
    {
        sb.Clear();
        switch (itemID)
        {
            case 1000:
                sb.Append("평범한 갑옷이다.");
                break;
            case 1001:
                sb.Append("평범한 신발이다.");
                break;
            case 1002:
                sb.Append("평범한 투구다.");
                break;
            case 1003:
                sb.Append("평범한 장갑이다.");
                break;
            case 1004:
                sb.Append("평범한 대검이다.");
                break;
            case 1005:
                sb.Append("실력있는 대장장이가 만든 갑옷이다. 튼튼하진 않은거 같은데..?");
                break;
            case 1006:
                sb.Append("실력있는 대장장이가 만든 신발이다. 치명적인 매력이 있다.");
                break;
            case 1007:
                sb.Append("실력있는 대장장이가 만든 투구다. 치명적인 아픔이 있다.");
                break;
            case 1008:
                sb.Append("실력있는 대장장이가 만든 장갑이다. 좀 더 강하게 때릴 수 있다.");
                break;
            case 1009:
                sb.Append("실력있는 대장장이가 만든 대검이다. 강 하 다!");
                break;
            case 1010:
                sb.Append("I'M THE CREEPER. CATCH ME IF YOU CAN!");
                break;
            case 1011:
                sb.Append("DON'T TREAT ME. IT'S USELESS.");
                break;
            case 1012:
                sb.Append("I'M PERFECT. YOU WANT ME, RIGHT?");
                break;
            case 1013:
                sb.Append("I'M THE CREEPER. CATCH ME IF YOU CAN!");
                break;
            case 1014:
                sb.Append("DON'T TREAT ME. IT'S USELESS.");
                break;
            case 1015:
                sb.Append("I'M PERFECT. YOU WANT ME, RIGHT?");
                break;
            case 1016:
                sb.Append("I'M THE CREEPER. CATCH ME IF YOU CAN!");
                break;
            case 1017:
                sb.Append("DON'T TREAT ME. IT'S USELESS.");
                break;
            case 1018:
                sb.Append("I'M PERFECT. YOU WANT ME, RIGHT?");
                break;
            case 1019:
                sb.Append("I'M THE CREEPER. CATCH ME IF YOU CAN!");
                break;
            case 1020:
                sb.Append("DON'T TREAT ME. IT'S USELESS.");
                break;
            case 1021:
                sb.Append("I'M PERFECT. YOU WANT ME, RIGHT?");
                break;
            case 1022:
                sb.Append("I'M THE CREEPER. CATCH ME IF YOU CAN!");
                break;
            case 1023:
                sb.Append("DON'T TREAT ME. IT'S USELESS.");
                break;
            case 1024:
                sb.Append("I'M PERFECT. YOU WANT ME, RIGHT?");
                break;
        }
    }
    public void Equip(Equipment _item)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
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
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        StringBuilder sb = new StringBuilder();
        Equipment e;
        if (!infoScreen.gameObject.activeSelf)
        {
            infoScreen.gameObject.SetActive(true);
        }
        switch (val)
        {
            #region 투구
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
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TStat.text = sb.ToString();
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
                SetEquipmentDescription(sb, e.ID);
                TDescription.text = sb.ToString();
                infoScreen.GetComponent<ItemInfoScreen>().e = e;
                break;
            #endregion
            #region 갑옷
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
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TStat.text = sb.ToString();
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
                SetEquipmentDescription(sb, e.ID);
                TDescription.text = sb.ToString();
                infoScreen.GetComponent<ItemInfoScreen>().e = e;
                break;
            #endregion
            #region 무기
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
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TStat.text = sb.ToString();
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
                SetEquipmentDescription(sb, e.ID);
                TDescription.text = sb.ToString();
                infoScreen.GetComponent<ItemInfoScreen>().e = e;
                break;
            #endregion
            #region 장갑
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
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TStat.text = sb.ToString();
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
                SetEquipmentDescription(sb, e.ID);
                TDescription.text = sb.ToString();
                infoScreen.GetComponent<ItemInfoScreen>().e = e;
                break;
            #endregion
            #region 신발
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
                sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                TStat.text = sb.ToString();
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
                SetEquipmentDescription(sb, e.ID);
                TDescription.text = sb.ToString();
                infoScreen.GetComponent<ItemInfoScreen>().e = e;
                break;
                #endregion
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
