using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : SingletonMonobehaviour<Inventory>
{
    public static bool inventoryActivated = false;
    //슬롯들
    public Slot.SlotAddition[] slots;
    public Transform slotHolder;
    public int slotCount;
    //아이템 이미지 리스트
    public IntSprite itemImages;

    public bool isVisible;
    //0 = 장비, 1 = 재료, 2 = 기타
    public int InvenTabNum;
    public GameObject InvenUI;
    public Toggle[] InvenTabs;
    //드롭다운 메뉴 (정렬)
    public GameObject menu;
    private TMP_Dropdown dropDown; 
    bool CoroutineIsRunning = false;
    private void Start()
    {
        itemImages = GameData.Instance.itemImages;
        //인벤토리 탭 번호 (0 = 장비, 1 = 재료, 2 = 기타)
        InvenTabNum = 0;
        InvenTabs = InvenUI.GetComponentsInChildren<Toggle>();
        //인벤토리 드롭다운 메뉴 초기화
        dropDown = menu.GetComponent<TMP_Dropdown>();
        //슬롯초기화
        slots = slotHolder.GetComponentsInChildren<Slot.SlotAddition>(); 
        if (DataManager.Instance.AllInvenData == null){ slotCount = 0; }
        else { slotCount = DataManager.Instance.AllInvenData.EquipmentList.Count; }
        SetSlotNumber();
        SlotChange(slotCount);

        //시작할 때 높은 등급 순으로 정렬
        if (DataManager.Instance.AllInvenData != null)
        {
            DataManager.Instance.SortByIDDecending(InvenTabNum);
            SetImage();
        }
        isVisible = InvenUI.activeSelf;
    }
    private void Update()
    {
        //인벤에 아이템 추가
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AddEquipment(1000);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            AddEquipment(1004);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            AddEquipment(1002);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            AddIngredient(101);
            AddIngredient(102);
            AddIngredient(103);
            AddIngredient(104);
            AddIngredient(105);
            AddIngredient(108);
            AddIngredient(111);
        }
        isVisible = InvenUI.activeSelf; 
        if (isVisible)
        {
            if (!CoroutineIsRunning)
            {
                if (DataManager.Instance.AllInvenData != null)
                {
                    CoroutineIsRunning = true;
                    StartCoroutine(RefreshCoroutine());
                }
            }
        }
    }
    IEnumerator RefreshCoroutine()
    {
        switch (InvenTabNum)
        {
            case 0:
                for (int i = 0; i < DataManager.Instance.AllInvenData.EquipmentList.Count; i++)
                {
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                }
                break;
            case 1:
                for (int i = 0; i < DataManager.Instance.AllInvenData.IngredientList.Count; i++)
                {
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                }
                break;
            case 2:
                for (int i = 0; i < DataManager.Instance.AllInvenData.MiscList.Count; i++)
                {
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                }
                break;
        }
        AddSlot();
        SetImage();
        yield return new WaitForSecondsRealtime(0.2f);
        CoroutineIsRunning = false;
    }
    //스타트 씬으로 돌아가면 파괴
    public void Destroy()
    {
        Destroy(gameObject);
    }
    //=====================================
    //아이템 슬롯 관련
    //슬롯버튼 사용가능으로 바꾸기
    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < val) slots[i].GetComponent<Button>().interactable = true;
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
                slots[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    //슬롯버튼 한개 잠금해제
    public void AddSlot()
    {
        if (DataManager.Instance.AllInvenData == null)
        {
            slotCount = 0;
            SlotChange(slotCount);
            return;
        }
        switch (InvenTabNum)
        {
            case 0:
                slotCount = DataManager.Instance.AllInvenData.EquipmentList.Count;
                break;
            case 1:
                slotCount = DataManager.Instance.AllInvenData.IngredientList.Count;
                break;
            case 2:
                slotCount = DataManager.Instance.AllInvenData.MiscList.Count;
                break;
        }
        SlotChange(slotCount);
    }
    //각 슬롯에 번호 붙여주기
    public void SetSlotNumber()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<ItemInfo>().SetSlotNum(i);
        }
    }
    //=====================================

    //=====================================
    //아이템 관련
    //아이템 이미지 찾아서 추가
    public void SetImage()
    {
        switch (InvenTabNum)
        {
            case 0:
                #region 장비 인벤토리 아이템 개수 따라 표시
                for (int i = 0; i <= DataManager.Instance.AllInvenData.EquipmentList.Count; i++)
                {
                    if (i >= DataManager.Instance.AllInvenData.EquipmentList.Count)
                    {
                        if (i < slots.Length)
                        {
                            for (int j = i; j < slots.Length; j++)
                            {
                                slots[j].transform.GetChild(0).gameObject.SetActive(false);
                                slots[j].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                        break;
                    }
                    slots[i].transform.GetChild(0).gameObject.SetActive(true);
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = itemImages[DataManager.Instance.AllInvenData.EquipmentList[i].itemScriptID]; ;
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                    slots[i].transform.GetChild(2).gameObject.SetActive(true);
                    SetGradeColor(DataManager.Instance.AllInvenData.EquipmentList[i].itemGrade, i);
                }
                #endregion
                break;
            case 1:
                #region 재료 인벤토리 아이템 개수 따라 표시
                for (int i = 0; i <= DataManager.Instance.AllInvenData.IngredientList.Count; i++)
                {
                    if (i >= DataManager.Instance.AllInvenData.IngredientList.Count)
                    {
                        if (i < slots.Length)
                        {
                            for (int j = i; j < slots.Length; j++)
                            {
                                slots[j].transform.GetChild(0).gameObject.SetActive(false);
                                slots[j].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                        break;
                    }
                    slots[i].transform.GetChild(0).gameObject.SetActive(true);
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = itemImages[DataManager.Instance.AllInvenData.IngredientList[i].itemScriptID]; ;
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                    SetGradeColor(DataManager.Instance.AllInvenData.IngredientList[i].itemGrade, i);
                }
                #endregion
                break;
            case 2:
                #region 기타 인벤토리 아이템 개수 따라 표시
                for (int i = 0; i <= DataManager.Instance.AllInvenData.MiscList.Count; i++)
                {
                    if (i >= DataManager.Instance.AllInvenData.MiscList.Count)
                    {
                        if (i < slots.Length)
                        {
                            for (int j = i; j < slots.Length; j++)
                            {
                                slots[j].transform.GetChild(0).gameObject.SetActive(false);
                                slots[j].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                        break;
                    }
                    slots[i].transform.GetChild(0).gameObject.SetActive(true);
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = itemImages[DataManager.Instance.AllInvenData.MiscList[i].itemScriptID]; ;
                    slots[i].GetComponent<ItemInfo>().RefreshCount(true);
                    SetGradeColor(DataManager.Instance.AllInvenData.MiscList[i].itemGrade, i);
                }
                #endregion
                break;
        }
        
    }
    //아이템 추가
    public void AddEquipment(int itemID, int count = 1)
    {
        DataManager.Instance.AddEquipmentData(GameData.Instance.FindEquipmentByID(itemID), count);
    }
    public void AddIngredient(int itemID, int count = 1)
    {
        DataManager.Instance.AddIngredientData(GameData.Instance.FindIngredientByID(itemID), count);
    }
    //아이템 삭제
    public void RemoveEquipment(int itemID)
    {
        DataManager.Instance.RemoveEquipmentData(GameData.Instance.FindEquipmentByID(itemID));
    }
    public void RemoveIngredient(int itemID, int count = 1)
    {
        DataManager.Instance.RemoveIngredientData(GameData.Instance.FindIngredientByID(itemID), count);
    }
    //등급 이미지(색상) 교체
    public void SetGradeColor(int itemGrade, int index)
    {
        switch (itemGrade)
        {
            case 1:
                slots[index].transform.GetChild(2).GetComponent<Image>().color = Color.gray;
                break;
            case 2:
                slots[index].transform.GetChild(2).GetComponent<Image>().color = Color.blue;
                break;
            case 3:
                slots[index].transform.GetChild(2).GetComponent<Image>().color = Color.red;
                break;
        }
    }
    //=====================================
    //아이템탭 교체
    //=====================================
    public void CheckIsSelected(int val)
    {
        for (int i = 0; i < InvenTabs.Length; i++)
        {
            if (i != val)
            {
                InvenTabs[i].gameObject.GetComponent<Animator>().ResetTrigger("Selected");
                InvenTabs[i].gameObject.GetComponent<Animator>().SetTrigger("Deselected");
            }
        }
        InvenTabs[val].isOn = true;
        InvenTabs[val].gameObject.GetComponent<Animator>().ResetTrigger("Deselected");
        InvenTabs[val].gameObject.GetComponent<Animator>().SetTrigger("Selected");
    }
    //=====================================
    //아이템 탭에 따라 슬롯 아이템 교체
    //=====================================
    public void ChangeTab(int val)
    {
        InvenTabNum = val;
        CheckIsSelected(val);
        AddSlot();
        SetImage();
    }
    //=====================================
    //정렬 탭에 따라 정렬
    //=====================================
    public void Sort()
    {
        switch (dropDown.value)
        {
            case 0:
                DataManager.Instance.SortByIDDecending(InvenTabNum);
                break;
            case 1:
                DataManager.Instance.SortByIDAscending(InvenTabNum);
                break;
            case 2:
                DataManager.Instance.SortByName(InvenTabNum);
                break;
        }
        DataManager.Instance.AllInvenData = DataManager.Instance.AllInvenData;
        SetImage();
    }
    //=====================================
    //인벤에서 아이템 검색
    //=====================================
    public Equipment FindEquipment(int itemID)
    {
        for (int i = 0; i < DataManager.Instance.AllInvenData.EquipmentList.Count; i++)
        {
            if (DataManager.Instance.AllInvenData.EquipmentList[i].ID == itemID)
            {
                return DataManager.Instance.AllInvenData.EquipmentList[i];
            }
        }
        return null;
    }
    public bool IsEquipmentExist(int itemID)
    {
        for (int i = 0; i < DataManager.Instance.AllInvenData.EquipmentList.Count; i++)
        {
            if (DataManager.Instance.AllInvenData.EquipmentList[i].ID == itemID)
            {
                return true;
            }
        }
        return false;
    }
    public Ingredient FindIngredient(int itemID)
    {
        for (int i = 0; i < DataManager.Instance.AllInvenData.IngredientList.Count; i++)
        {
            if (DataManager.Instance.AllInvenData.IngredientList[i].ID == itemID)
            {
                return DataManager.Instance.AllInvenData.IngredientList[i];
            }
        }
        return null;
    }
    public bool IsIngredientExist(int itemID)
    {
        for (int i = 0; i < DataManager.Instance.AllInvenData.IngredientList.Count; i++)
        {
            if (DataManager.Instance.AllInvenData.IngredientList[i].ID == itemID)
            {
                return true;
            }
        }
        return false;
    }
}
