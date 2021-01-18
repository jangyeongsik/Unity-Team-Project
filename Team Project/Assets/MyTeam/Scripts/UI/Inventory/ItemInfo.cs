using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Text;

public class ItemInfo : MonoBehaviour
{
    GameData data;
    private int slotNum;
    public Canvas infoScreen;
    public Image image;
    public Transform infoUI;
    public TMP_Text TName;
    public TMP_Text TCategory;
    public TMP_Text TDescription;
    public TMP_Text TGrade;
    public int InvenTabNum;

    public void SetSlotNum(int num)
    {
        slotNum = num;
        InvenTabNum = Inventory.Instance.InvenTabNum;
    }

    private void Start()
    {
        data = GameData.Instance;
    }
    private void Update()
    {
        if (!Inventory.Instance.isVisible)
        {
            infoScreen.gameObject.SetActive(false);
        }
        if (InvenTabNum != Inventory.Instance.InvenTabNum)
        {
            InvenTabNum = Inventory.Instance.InvenTabNum;
        }
    }
    public void ShowItemInfo()
    {
        if (!infoScreen.gameObject.activeSelf)
        {
            infoScreen.gameObject.SetActive(true);
        }
        Debug.Log(Application.persistentDataPath);
        switch (InvenTabNum)
        {
            //EQUIPMENT
            case 0:
                #region 장비 리스트
                if (slotNum < Inventory.Instance.pInven.EquipmentList.Count)
                {
                    StringBuilder sb = new StringBuilder();
                    Equipment e = Inventory.Instance.pInven.EquipmentList[slotNum];
                    //아이템 이미지 받아와 넣기
                    TName.text = e.name;
                    sb.Append("장비");
                    TCategory.text = sb.ToString();
                    image.sprite = Inventory.Instance.slots[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
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
                    //슬롯넘버 넘기기
                    infoScreen.GetComponent<ItemInfoScreen>().slotNum = slotNum;
                }
                #endregion
                break;
            //INGREDIENT
            case 1:
                #region 재료 리스트
              if (slotNum < Inventory.Instance.pInven.IngredientList.Count)
              {
                    StringBuilder sb = new StringBuilder();
                    Ingredient e = Inventory.Instance.pInven.IngredientList[slotNum];
                    //아이템 이미지 받아와 넣기
                    TName.text = e.name;
                    sb.Append("재료");
                    TCategory.text = sb.ToString();
                    image.sprite = Inventory.Instance.slots[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
                    sb.Clear();
                    if (e.itemGrade != 3)
                    {
                        sb.AppendFormat("신전에서 구할 수 있습니다.");
                    }
                    else
                    {
                        sb.AppendFormat("백신 야영지에서 구할 수 있습니다.");
                    }
                    TDescription.text = sb.ToString();
                    sb.Clear();
                    sb.Append(" ");
                    TGrade.text = sb.ToString();
                    //슬롯넘버 넘기기
                    infoScreen.GetComponent<ItemInfoScreen>().slotNum = slotNum;
              }
              #endregion
                break;
            //MISC
            case 2:
                #region 기타 리스트
              if (slotNum < Inventory.Instance.pInven.MiscList.Count)
              {
                    StringBuilder sb = new StringBuilder();
                    Misc e = Inventory.Instance.pInven.MiscList[slotNum];
                    //아이템 이미지 받아와 넣기
                    TName.text = e.name;
                    sb.Append("열쇠");
                    TCategory.text = sb.ToString();
                    image.sprite = Inventory.Instance.slots[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
                    sb.Clear();
                    sb.Append("사냥으로 구할 수 있습니다.");
                    TDescription.text = sb.ToString();
                    sb.Clear();
                    sb.Append(" ");
                    TGrade.text = sb.ToString();
                    //슬롯넘버 넘기기
                    infoScreen.GetComponent<ItemInfoScreen>().slotNum = slotNum;
                }
              #endregion
                break;
        } 
    }
    public void RefreshCount(bool isAdded)
    {
        InvenTabNum = Inventory.Instance.InvenTabNum;
        if (isAdded)
        {
            switch (InvenTabNum)
            {
                case 0:
                    gameObject.GetComponent<Slot.SlotAddition>().SetCountText(Inventory.Instance.pInven.EquipmentList[slotNum].count.ToString());
                    break;
                case 1:
                    gameObject.GetComponent<Slot.SlotAddition>().SetCountText(Inventory.Instance.pInven.IngredientList[slotNum].count.ToString());
                    break;
                case 2:
                    gameObject.GetComponent<Slot.SlotAddition>().SetCountText(Inventory.Instance.pInven.EquipmentList[slotNum].count.ToString());
                    break;
            }
        }
    }
}
