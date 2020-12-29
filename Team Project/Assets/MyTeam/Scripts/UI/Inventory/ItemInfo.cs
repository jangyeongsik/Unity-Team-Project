using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
    public TMP_Text TStat;
    public TMP_Text TSubStat;
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
        switch (InvenTabNum)
        {
            //EQUIPMENT
            case 0:
                #region 장비 리스트
                if (slotNum < Inventory.Instance.pInven.EquipmentList.Count)
                {
                    //아이템 이미지 받아와 넣기
                    TName.text = Inventory.Instance.pInven.EquipmentList[slotNum].name;
                    TCategory.text = Inventory.Instance.pInven.EquipmentList[slotNum].equipmentType.ToString();
                    image.sprite = Inventory.Instance.slots[slotNum].GetComponent<Image>().sprite;
                    //아이템 설명 TDescription.text = data.equipmentData[slotNum].;
                    TStat.text = Inventory.Instance.pInven.EquipmentList[slotNum].itemGrade;
                    TSubStat.text = "ID : " + Inventory.Instance.pInven.EquipmentList[slotNum].ID;
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
                  //아이템 이미지 받아와 넣기
                  TName.text = Inventory.Instance.pInven.IngredientList[slotNum].name;
                  TCategory.text = Inventory.Instance.pInven.IngredientList[slotNum].itemCategory.ToString();
                  image.sprite = Inventory.Instance.slots[slotNum].GetComponent<Image>().sprite;
                  //아이템 설명 TDescription.text = data.equipmentData[slotNum].;
                  TStat.text = Inventory.Instance.pInven.IngredientList[slotNum].itemGrade;
                  TSubStat.text = "ID : " + Inventory.Instance.pInven.IngredientList[slotNum].ID;
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
                  //아이템 이미지 받아와 넣기
                  TName.text = Inventory.Instance.pInven.MiscList[slotNum].name;
                  TCategory.text = Inventory.Instance.pInven.MiscList[slotNum].itemCategory.ToString();
                  image.sprite = Inventory.Instance.slots[slotNum].GetComponent<Image>().sprite;
                  //아이템 설명 TDescription.text = data.equipmentData[slotNum].;
                  TStat.text = Inventory.Instance.pInven.MiscList[slotNum].itemGrade;
                  TSubStat.text = "ID : " + Inventory.Instance.pInven.MiscList[slotNum].ID;
                  //슬롯넘버 넘기기
                  infoScreen.GetComponent<ItemInfoScreen>().slotNum = slotNum;
              }
              #endregion
                break;
        }
        
    }
    public void RefreshCount(bool isAdded)
    {
        if (isAdded)
        {
            switch (InvenTabNum)
            {
                case 0:
                    gameObject.GetComponent<Slot.SlotAddition>().SetCountText(Inventory.Instance.pInven.EquipmentList[slotNum].count.ToString());
                    break;
                //case 1:
                //    gameObject.GetComponent<Slot.SlotAddition>().SetCountText(Inventory.Instance.pInven.IngredientList[slotNum].count.ToString());
                //    break;
                //case 2:
                //    gameObject.GetComponent<Slot.SlotAddition>().SetCountText(Inventory.Instance.pInven.EquipmentList[slotNum].count.ToString());
                //    break;
            }
        }
    }
}
