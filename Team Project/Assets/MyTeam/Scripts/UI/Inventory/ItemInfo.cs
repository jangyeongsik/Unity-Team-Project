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
    public Canvas ingredientInfoScreen;
    public Image ISImage;
    public Image IISImage;
    public Transform infoUI;
    public TMP_Text ISName;
    public TMP_Text ISCategory;
    public TMP_Text ISStat;
    public TMP_Text ISGrade;
    public TMP_Text ISDescription;
    public TMP_Text IISName;
    public TMP_Text IISCategory;
    public TMP_Text IISStat;
    public TMP_Text IISGrade;
    public TMP_Text IISDescription;
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
    #region 아이템 설명 넣어주기
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
    public void SetIngredientDescription(StringBuilder sb, int itemID)
    {
        sb.Clear();
        switch (itemID)
        {
            case 101:
                sb.Append("평범한 장비를 만들수 있는 코드다.");
                break;
            case 102:
                sb.Append("평범한 장비를 만들수 있는 코드다.");
                break;
            case 103:
                sb.Append("잘 맹근 장비를 만들수 있는 코드다.");
                break;
            case 104:
                sb.Append("잘 맹근 장비를 만들수 있는 코드다.");
                break;
            case 105:
                sb.Append("크리퍼 갑옷을 만들수 있는 특수코드이다.");
                break;
            case 106:
                sb.Append("바이럿 갑옷을 만들수 있는 특수코드이다.");
                break;
            case 107:
                sb.Append("트로이잔 갑옷을 만들수 있는 특수코드이다.");
                break;
            case 108:
                sb.Append("크리퍼 신발을 만들수 있는 특수코드이다.");
                break;
            case 109:
                sb.Append("바이럿 신발을 만들수 있는 특수코드이다.");
                break;
            case 110:
                sb.Append("트로이잔 신발을 만들수 있는 특수코드이다.");
                break;
            case 111:
                sb.Append("크리퍼 투구를 만들수 있는 특수코드이다.");
                break;
            case 112:
                sb.Append("바이럿 투구를 만들수 있는 특수코드이다.");
                break;
            case 113:
                sb.Append("트로이잔 투구를 만들수 있는 특수코드이다.");
                break;
            case 114:
                sb.Append("크리퍼 장갑을 만들수 있는 특수코드이다.");
                break;
            case 115:
                sb.Append("바이럿 장갑을 만들수 있는 특수코드이다.");
                break;
            case 116:
                sb.Append("트로이잔 장갑을 만들수 있는 특수코드이다.");
                break;
            case 117:
                sb.Append("크리퍼 대검을 만들수 있는 특수코드이다.");
                break;
            case 118:
                sb.Append("바이럿 대검을 만들수 있는 특수코드이다.");
                break;
            case 119:
                sb.Append("트로이잔 대검을 만들수 있는 특수코드이다.");
                break;
        }
    }
    #endregion
    #region 아이템 설명 창 띄우기
    public void ShowItemInfo()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        switch (InvenTabNum)
        {
            //EQUIPMENT
            case 0:
                #region 장비 리스트
                if (!infoScreen.gameObject.activeSelf)
                {
                    infoScreen.gameObject.SetActive(true);
                }
                if (slotNum < DataManager.Instance.AllInvenData.EquipmentList.Count)
                {
                    StringBuilder sb = new StringBuilder();
                    Equipment e = DataManager.Instance.AllInvenData.EquipmentList[slotNum];
                    //아이템 이미지 받아와 넣기
                    ISName.text = e.name;
                    sb.Append("장비");
                    ISCategory.text = sb.ToString();
                    ISImage.sprite = Inventory.Instance.slots[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
                    sb.Clear();
                    sb.AppendFormat("공격력 : {0}\n", e.damage);
                    sb.AppendFormat("속도 : {0}\n", e.speed);
                    sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
                    ISStat.text = sb.ToString();
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
                    ISGrade.text = sb.ToString();
                    SetEquipmentDescription(sb, e.ID);
                    ISDescription.text = sb.ToString();
                    //슬롯넘버 넘기기
                    infoScreen.GetComponent<ItemInfoScreen>().slotNum = slotNum;
                }
                #endregion
                break;
            //INGREDIENT
            case 1:
                #region 재료 리스트
                if (!ingredientInfoScreen.gameObject.activeSelf)
                {
                    ingredientInfoScreen.gameObject.SetActive(true);
                }
                if (slotNum < DataManager.Instance.AllInvenData.IngredientList.Count)
                {
                    StringBuilder sb = new StringBuilder();
                    Ingredient e = DataManager.Instance.AllInvenData.IngredientList[slotNum];
                    //아이템 이미지 받아와 넣기
                    IISName.text = e.name;
                    sb.Append("재료");
                    IISCategory.text = sb.ToString();
                    IISImage.sprite = Inventory.Instance.slots[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
                    sb.Clear();
                    if (e.itemGrade != 3)
                    {
                        sb.AppendFormat("신전에서 구할 수 있습니다.");
                    }
                    else
                    {
                        sb.AppendFormat("백신 야영지에서 구할 수 있습니다.");
                    }
                    IISStat.text = sb.ToString();
                    sb.Clear();
                    sb.Append(" ");
                    IISGrade.text = sb.ToString();
                    SetIngredientDescription(sb, e.ID);
                    IISDescription.text = sb.ToString();
                    //슬롯넘버 넘기기
                    infoScreen.GetComponent<ItemInfoScreen>().slotNum = slotNum;
                }
                #endregion
                break;
            //MISC
            case 2:
                #region 기타 리스트
                if (slotNum < DataManager.Instance.AllInvenData.MiscList.Count)
                {
                    StringBuilder sb = new StringBuilder();
                    Misc e = DataManager.Instance.AllInvenData.MiscList[slotNum];
                    //아이템 이미지 받아와 넣기
                    ISName.text = e.name;
                    sb.Append("열쇠");
                    ISCategory.text = sb.ToString();
                    ISImage.sprite = Inventory.Instance.slots[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
                    sb.Clear();
                    sb.Append("사냥으로 구할 수 있습니다.");
                    ISStat.text = sb.ToString();
                    sb.Clear();
                    sb.Append(" ");
                    ISGrade.text = sb.ToString();
                    //슬롯넘버 넘기기
                    infoScreen.GetComponent<ItemInfoScreen>().slotNum = slotNum;
                }
                #endregion
                break;
        }
    }
    #endregion
    #region 아이템 개수 새로고침 (실시간 반영)
    public void RefreshCount(bool isAdded)
    {
        InvenTabNum = Inventory.Instance.InvenTabNum;
        if (isAdded)
        {
            switch (InvenTabNum)
            {
                case 0:
                    gameObject.GetComponent<Slot.SlotAddition>().SetCountText(DataManager.Instance.AllInvenData.EquipmentList[slotNum].count.ToString());
                    break;
                case 1:
                    gameObject.GetComponent<Slot.SlotAddition>().SetCountText(DataManager.Instance.AllInvenData.IngredientList[slotNum].count.ToString());
                    break;
                case 2:
                    gameObject.GetComponent<Slot.SlotAddition>().SetCountText(DataManager.Instance.AllInvenData.EquipmentList[slotNum].count.ToString());
                    break;
            }
        }
    }
    #endregion
}
