using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftController : MonoBehaviour
{
    //장비 아이템 리스트
    public GameObject EquipButtonGroup;
    private Dictionary<EQUIPMENTTYPE, Transform> buttons;
    //플레이어 장비아이템 리스트
    private List<Equipment> pEquip;
    //제작 아이템 리스트
    private void Start()
    {
        Button[] temp = EquipButtonGroup.GetComponentsInChildren<Button>();
        for (int i = 0; i < temp.Length; i++)
        {
            buttons.Add((EQUIPMENTTYPE)i, temp[i].transform);
        }
        pEquip = DataManager.Instance.EquipInvenData.CurrentEquipmentList;
    }
    //장비 아이템 버튼 이미지 조정
    public void SetEquipItem()
    {
        for (int i = 0; i < pEquip.Count; i++)
        {
            Image itemGradeImage = buttons[pEquip[i].equipmentType].GetChild(0).GetComponent<Image>();
            buttons[pEquip[i].equipmentType].GetChild(1).GetComponent<Image>().sprite = Inventory.Instance.itemImages[pEquip[i].itemScriptID];
            switch (pEquip[i].itemGrade)
            {
                case 1:
                    itemGradeImage.color = Color.gray;
                    break;
                case 2:
                    itemGradeImage.color = Color.blue ;
                    break;
                case 3:
                    itemGradeImage.color = Color.red;
                    break;
            }
        }
    }
    public void SetCraftTree(Equipment e)
    {

    }
}
