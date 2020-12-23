using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour   
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    [SerializeField]
    private Text textCount;
    [SerializeField]
    private GameObject goCountImage;

    //이미지의 투명도 조절
    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    //아이템 획득
    public void AddItem(Item item, int count = 1)
    {
        this.item = item;
        itemCount = count;

        //if (item.itemType != Item.ItemType.Equipment)
        //{
        //    goCountImage.SetActive(true);
        //    textCount.text = itemCount.ToString();
        //}
        //else
        //{
        //    textCount.text = itemCount.ToString();
        //    goCountImage.SetActive(false);
        //}
        SetColor(1);
    }

    //아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        textCount.text = itemCount.ToString();
        if (itemCount <= 0) ClearSlot();
    }

    //슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        goCountImage.SetActive(false);
        textCount.text = "0";
    }
}
