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

    public void SetSlotNum(int num)
    {
        slotNum = num;
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
    }
    public void ShowItemInfo()
    {
        if (!infoScreen.gameObject.activeSelf)
        {
            infoScreen.gameObject.SetActive(true);
        }
        if (slotNum < Inventory.Instance.pInven.ListData.Count)
        {
            //아이템 이미지 받아와 넣기
            TName.text = Inventory.Instance.pInven.ListData[slotNum].name;
            TCategory.text = Inventory.Instance.pInven.ListData[slotNum].itemCategory.ToString();
            image.sprite = Inventory.Instance.pInven.ListData[slotNum].image;
            //아이템 설명 TDescription.text = data.equipmentData[slotNum].;
            TStat.text = Inventory.Instance.pInven.ListData[slotNum].itemGrade;
            TSubStat.text = "ID : " + Inventory.Instance.pInven.ListData[slotNum].ID;
            //슬롯넘버 넘기기
            infoScreen.GetComponent<ItemInfoScreen>().slotNum = slotNum;
        }
    }
    public void RefreshCount(bool isAdded)
    {
        if (isAdded)
        {
            gameObject.GetComponent<Slot.SlotAddition>().SetCountText(Inventory.Instance.pInven.ListData[slotNum].count.ToString());
        }
    }
}
