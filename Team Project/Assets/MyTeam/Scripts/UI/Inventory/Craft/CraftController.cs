using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
public class CraftController : MonoBehaviour
{
    //재료 정보 팝업창
    public GameObject ingredientInfoScreen;
    public GameObject equipmentInfoScreen;
    public GameObject specialIngScreen;
    public GameObject forgeButton;
    public GameObject forgeBar;
    IngredientInfoScreen ingInfo;
    EquipmentInfoScreen eqInfo;
    SpecialIngScreen spIng;
    //장비 아이템 리스트
    public GameObject EquipButtonGroup;
    private Dictionary<EQUIPMENTTYPE, Transform> equipButtons;
    [SerializeField]
    Button[] btns;

    //플레이어 장비아이템 리스트
    private List<Equipment> pEquip;

    //제작 아이템 리스트
    public GameObject CraftButtonGroup;
    [SerializeField]
    Button[] cBtns;
    private Dictionary<string, Transform> craftButtons;
    private Dictionary<string, TMP_Text> ingredientTexts;
    public GameObject blockingLayer;
    public GameObject menuButtonBlockingLayer;

    //텍스트 변경용 StringBuilder
    StringBuilder sb;

    //현재 떠있는 production
    Production currentProd = null;

    //현재 선택되어 있는 특수재료
    int currentUniqueIngID = 0;
    int currentUniqueIngCount = 0;

    private void Awake()
    {
        ingInfo = ingredientInfoScreen.GetComponent<IngredientInfoScreen>();
        eqInfo = equipmentInfoScreen.GetComponent<EquipmentInfoScreen>();
        spIng = specialIngScreen.GetComponent<SpecialIngScreen>();
        #region 장비 아이템 버튼들을 장비타입 (HELM, WEAPON 등등)을 키값으로 검색할 수 있게 딕셔너리로 생성
        equipButtons = new Dictionary<EQUIPMENTTYPE, Transform>();
        btns = EquipButtonGroup.GetComponentsInChildren<Button>();
        for (int i = 0; i < btns.Length; i++)
        {
            equipButtons.Add((EQUIPMENTTYPE)i + 1, btns[i].transform);
        }
        #endregion
        #region 제작 아이템 버튼들을 이름으로 검색할 수 있게 Dictionary화
        //아이템 버튼
        craftButtons = new Dictionary<string, Transform>();
        cBtns = CraftButtonGroup.GetComponentsInChildren<Button>();
        craftButtons.Add("명작장비 슬롯 1", cBtns[0].transform);
        craftButtons.Add("명작장비 슬롯 2", cBtns[1].transform);
        craftButtons.Add("명작장비 슬롯 3", cBtns[2].transform);
        craftButtons.Add("특수재료 슬롯", cBtns[3].transform);
        craftButtons.Add("걸작장비 슬롯", cBtns[4].transform);
        craftButtons.Add("레어재료 슬롯 1", cBtns[5].transform);
        craftButtons.Add("레어재료 슬롯 2", cBtns[6].transform);
        craftButtons.Add("평작장비 슬롯", cBtns[7].transform);
        craftButtons.Add("일반재료 슬롯 1", cBtns[8].transform);
        craftButtons.Add("일반재료 슬롯 2", cBtns[9].transform);
        //버튼에 이름 넣기
        SetCraftButtonName();
        //필요개수 텍스트
        sb = new StringBuilder();
        ingredientTexts = new Dictionary<string, TMP_Text>();
        ingredientTexts.Add("특수재료 슬롯", craftButtons["특수재료 슬롯"].GetChild(2).GetComponent<TMP_Text>());
        ingredientTexts.Add("레어재료 슬롯 1", craftButtons["레어재료 슬롯 1"].GetChild(2).GetComponent<TMP_Text>());
        ingredientTexts.Add("레어재료 슬롯 2", craftButtons["레어재료 슬롯 2"].GetChild(2).GetComponent<TMP_Text>());
        ingredientTexts.Add("일반재료 슬롯 1", craftButtons["일반재료 슬롯 1"].GetChild(2).GetComponent<TMP_Text>());
        ingredientTexts.Add("일반재료 슬롯 2", craftButtons["일반재료 슬롯 2"].GetChild(2).GetComponent<TMP_Text>());
        #endregion
    }
    private void Start()
    {
        SetEquipItem();
        //정보창 떠있으면 끄기
        if (ingredientInfoScreen.activeSelf) { ingredientInfoScreen.SetActive(false); }
        if (equipmentInfoScreen.activeSelf) { equipmentInfoScreen.SetActive(false); }
        if (specialIngScreen.activeSelf) { specialIngScreen.SetActive(false); }
        SetCraftTree(10005);
    }
    public void SetCraftButtonName()
    {
        List<string> keyList = new List<string>(craftButtons.Keys);
        for (int i = 0; i < keyList.Count; i++)
        {
            craftButtons[keyList[i]].GetComponent<CraftButton>().btnName = keyList[i];
        }
    }
    #region 장비 아이템 버튼 이미지 조정
    public void SetEquipItem()
    {
        //플레이어가 장비하고 있는 아이템 불러오기
        pEquip = DataManager.Instance.EquipInvenData.CurrentEquipmentList;
        pEquip.Sort((a, b) => a.equipmentType.CompareTo(b.equipmentType));
        if (pEquip == null)
        {
            return;
        }
        for (int i = 0; i < pEquip.Count; i++)
        {
            Image itemGradeImage = equipButtons[pEquip[i].equipmentType].GetChild(0).GetComponent<Image>();
            equipButtons[pEquip[i].equipmentType].GetChild(1).GetComponent<Image>().sprite = Inventory.Instance.itemImages[pEquip[i].itemScriptID];
            switch (pEquip[i].itemGrade)
            {
                case 1:
                    itemGradeImage.color = Color.gray;
                    break;
                case 2:
                    itemGradeImage.color = Color.blue;
                    break;
                case 3:
                    itemGradeImage.color = Color.red;
                    break;
            }
        }
        if (currentProd != null)
        {
            SetCraftTree(currentProd.productionID);
        }
    }
    #endregion
    #region 제작 아이템 버튼 이미지 조정
    public void SetCraftTree(Equipment e)
    {
        //e 로 그 묶음 찾아오기
        Production p = new Production();
        for (int i = 0; i < GameData.Instance.productionData.Count; i++)
        {
            if (GameData.Instance.productionData[i].productionID == e.productionID)
            {
                p = GameData.Instance.productionData[i];
                currentProd = p;
                currentUniqueIngID = p.unique_Ingredient_1_ID;
                currentUniqueIngCount = p.unique_Ingredient_1_Count;
                OnOffUniqueEquipmentButton(1);
                break;
            }
        }
        SetItemImages(p);
        SetAllText(p);
    }
    public void SetCraftTree(int productionID)
    {
        //e 의 productionID 로 그 묶음 찾아오기
        Production p = new Production();
        for (int i = 0; i < GameData.Instance.productionData.Count; i++)
        {
            if (GameData.Instance.productionData[i].productionID == productionID)
            {
                p = GameData.Instance.productionData[i];
                currentProd = p;
                currentUniqueIngID = p.unique_Ingredient_1_ID;
                currentUniqueIngCount = p.unique_Ingredient_1_Count;
                OnOffUniqueEquipmentButton(1);
                break;
            }
        }
        SetItemImages(p);
        SetAllText(p);
    }
    //가독성 때문에 따로 빼줌
    public void SetItemImages(Production p)
    {
        craftButtons["평작장비 슬롯"].GetChild(1).GetComponent<Image>().sprite = SetEquipmentImage(p.normal_ItemID);
        craftButtons["일반재료 슬롯 1"].GetChild(1).GetComponent<Image>().sprite = SetIngredientImage(p.normal_Ingredient_1_ID);
        craftButtons["일반재료 슬롯 2"].GetChild(1).GetComponent<Image>().sprite = SetIngredientImage(p.normal_Ingredient_2_ID);
        craftButtons["걸작장비 슬롯"].GetChild(1).GetComponent<Image>().sprite = SetEquipmentImage(p.rare_Item_ID);
        craftButtons["레어재료 슬롯 1"].GetChild(1).GetComponent<Image>().sprite = SetIngredientImage(p.rare_Ingredient_1_ID);
        craftButtons["레어재료 슬롯 2"].GetChild(1).GetComponent<Image>().sprite = SetIngredientImage(p.rare_Ingredient_2_ID);
        craftButtons["특수재료 슬롯"].GetChild(1).GetComponent<Image>().sprite = SetIngredientImage(currentUniqueIngID);
        craftButtons["명작장비 슬롯 1"].GetChild(1).GetComponent<Image>().sprite = SetEquipmentImage(p.unique_Item_1_ID);
        craftButtons["명작장비 슬롯 2"].GetChild(1).GetComponent<Image>().sprite = SetEquipmentImage(p.unique_Item_2_ID);
        craftButtons["명작장비 슬롯 3"].GetChild(1).GetComponent<Image>().sprite = SetEquipmentImage(p.unique_Item_3_ID);

        craftButtons["평작장비 슬롯"].GetComponent<CraftButton>().itemID = p.normal_ItemID;
        craftButtons["일반재료 슬롯 1"].GetComponent<CraftButton>().itemID = p.normal_Ingredient_1_ID;
        craftButtons["일반재료 슬롯 2"].GetComponent<CraftButton>().itemID = p.normal_Ingredient_2_ID;
        craftButtons["걸작장비 슬롯"].GetComponent<CraftButton>().itemID = p.rare_Item_ID;
        craftButtons["레어재료 슬롯 1"].GetComponent<CraftButton>().itemID = p.rare_Ingredient_1_ID;
        craftButtons["레어재료 슬롯 2"].GetComponent<CraftButton>().itemID = p.rare_Ingredient_2_ID;
        craftButtons["특수재료 슬롯"].GetComponent<CraftButton>().itemID = currentUniqueIngID;
        craftButtons["명작장비 슬롯 1"].GetComponent<CraftButton>().itemID = p.unique_Item_1_ID;
        craftButtons["명작장비 슬롯 2"].GetComponent<CraftButton>().itemID = p.unique_Item_2_ID;
        craftButtons["명작장비 슬롯 3"].GetComponent<CraftButton>().itemID = p.unique_Item_3_ID;
    }
    public Sprite SetEquipmentImage(int itemID)
    {
        return Inventory.Instance.itemImages[FindEquipmentItem(itemID).itemScriptID];
    }
    public Sprite SetIngredientImage(int itemID)
    {
        return Inventory.Instance.itemImages[FindIngredientItem(itemID).itemScriptID];
    }

    #endregion
    #region 재료 개수 표시
    public void SetAllText(Production p)
    {
        SetRequiredNum(ingredientTexts["일반재료 슬롯 1"], p.normal_Ingredient_1_Count, p.normal_Ingredient_1_ID);
        SetRequiredNum(ingredientTexts["일반재료 슬롯 2"], p.normal_Ingredient_2_Count, p.normal_Ingredient_2_ID);
        SetRequiredNum(ingredientTexts["레어재료 슬롯 1"], p.rare_Ingredient_1_Count, p.rare_Ingredient_1_ID);
        SetRequiredNum(ingredientTexts["레어재료 슬롯 2"], p.rare_Ingredient_2_Count, p.rare_Ingredient_2_ID);
        SetRequiredNum(ingredientTexts["특수재료 슬롯"], p.unique_Ingredient_1_Count, currentUniqueIngID);
    }
    public void SetRequiredNum(TMP_Text t, int requiredCount, int itemID)
    {
        int currentCount;
        if (Inventory.Instance.FindIngredient(itemID) != null) { currentCount = Inventory.Instance.FindIngredient(itemID).count; }
        else { currentCount = 0; }

        sb.Clear();
        sb.Append(currentCount.ToString());
        sb.Append(" / ");
        sb.Append(requiredCount.ToString());
        t.color = (currentCount >= requiredCount ? Color.white : Color.red);
        t.text = sb.ToString();
    }
    #endregion
    #region GameData에서 (CSV 에서) 아이템 검색
    public Equipment FindEquipmentItem(int itemID)
    {
        return GameData.Instance.FindEquipmentByID(itemID);
    }
    public Ingredient FindIngredientItem(int itemID)
    {
        return GameData.Instance.FindIngredientByID(itemID);
    }
    #endregion
    #region 장비중 아이템 클릭 시 
    public void ChangeTreeByEquipItem(int eT)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        Equipment e = new Equipment();
        bool isFound = false;
        for (int i = 0; i < pEquip.Count; i++)
        {
            if (pEquip[i].equipmentType == (EQUIPMENTTYPE)eT)
            {
                e = pEquip[i];
                isFound = true;
                break;
            }
        }
        if (!isFound)
        {
            if (!GameData.Instance.player.curSceneName.Equals("MAP000"))
            {
                switch (eT)
                {
                    case 1:
                        SetCraftTree(10005);
                        break;
                    case 2:
                        SetCraftTree(10003);
                        break;
                    case 3:
                        SetCraftTree(10001);
                        break;
                    case 4:
                        SetCraftTree(10004);
                        break;
                    case 5:
                        SetCraftTree(10002);
                        break;
                }
            }
            return;
        }
        SetCraftTree(e);
    }
    #endregion
    #region 아이템 정보창 관련
    //재료 정보 화면 출력
    public void OnIngredientInfoScreen(string btnName, int itemID)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        ActiveIngredientInfoScreen();
        ingInfo.itemImage.sprite = SetIngredientImage(itemID);
        ingInfo.itemName.text = FindIngredientItem(itemID).name;
        ingInfo.countText.text = ingredientTexts[btnName].text;
        ingInfo.countText.color = ingredientTexts[btnName].color;
        
        sb.Clear();
        sb.Append("신전에서\n구할 수 있습니다.");
        ingInfo.description.text = sb.ToString();
    }
    //장비 정보 화면 출력
    public void OnEquipmentInfoScreen(string btnName, int itemID)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        Equipment e = FindEquipmentItem(itemID);
        ActiveEquipmentInfoScreen();
        eqInfo.itemImage.sprite = SetEquipmentImage(itemID);
        eqInfo.itemName.text = e.name;
        SetIsAbleText(btnName);
        SetEquipmentDescription(e);
        eqInfo.btnName = btnName;
        eqInfo.itemID = itemID;

    }
    #region 장비아이템 제작가능 텍스트 판정
    public void SetIsAbleText(string btnName)
    {
        if (btnName.Contains("명작장비"))
        {
            IsAbleToForge(ingredientTexts["특수재료 슬롯"].color == Color.white
                && (Inventory.Instance.IsEquipmentExist(currentProd.rare_Item_ID) || DataManager.Instance.IsEquipmentExist(currentProd.rare_Item_ID)));
        }
        else if (btnName.Contains("걸작장비"))
        {
            IsAbleToForge(ingredientTexts["레어재료 슬롯 1"].color == Color.white
                && ingredientTexts["레어재료 슬롯 2"].color == Color.white
                && (Inventory.Instance.IsEquipmentExist(currentProd.normal_ItemID) || DataManager.Instance.IsEquipmentExist(currentProd.normal_ItemID)));
        }
        else if (btnName.Contains("평작장비"))
        {
            IsAbleToForge(ingredientTexts["일반재료 슬롯 1"].color == Color.white
                && ingredientTexts["일반재료 슬롯 2"].color == Color.white);
        }
    }
    public void IsAbleToForge(bool isAble)
    {
        if (isAble)
        {
            eqInfo.SetForgeButtonInteractable(true);
            sb.Clear();
            sb.Append("제작 가능");
            eqInfo.ableText.text = sb.ToString();
            eqInfo.ableText.color = Color.white;
        }
        else
        {
            eqInfo.SetForgeButtonInteractable(false);
            sb.Clear();
            sb.Append("재료 부족");
            eqInfo.ableText.text = sb.ToString();
            eqInfo.ableText.color = Color.red;
        }
    }
    #endregion
    #region 장비아이템 정보 텍스트 설정
    public void SetEquipmentDescription(Equipment e)
    {
        sb.Clear();
        sb.AppendFormat("공격력 : {0}\n", e.damage);
        sb.AppendFormat("속도 : {0}\n", e.speed);
        sb.AppendFormat("카운터 판정 : {0}\n", e.counterJudgement);
        eqInfo.description.text = sb.ToString();
    }
    #endregion
    #region 재료 정보창 On/Off
    public void ActiveIngredientInfoScreen()
    {
        if (!ingredientInfoScreen.activeSelf)
        {
            SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
            ingredientInfoScreen.SetActive(true);
        }
    }
    public void InactiveIngredientInfoScreen()
    {
        if (ingredientInfoScreen.activeSelf)
        {
            SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
            ingredientInfoScreen.SetActive(false);
        }
    }
    #endregion
    #region 장비 아이템 정보창 On/Off
    public void ActiveEquipmentInfoScreen()
    {
        if (!equipmentInfoScreen.activeSelf)
        {
            SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
            equipmentInfoScreen.SetActive(true);
        }
    }
    public void InactiveEquipmentInfoScreen()
    {
        if (equipmentInfoScreen.activeSelf)
        {
            SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
            equipmentInfoScreen.SetActive(false);
        }
    }
    #endregion
    #endregion
    #region 장비 제작
    public void Forge()
    {
        int itemID = eqInfo.itemID;
        string btnName = eqInfo.btnName;
        forgeButton.SetActive(false);
        forgeBar.SetActive(true);
        StartCoroutine(ForgeCoroutine());
        StartCoroutine(ForgeSoundCoroutine());
        if (btnName.Contains("명작장비"))
        {
            CutIngredientCount(currentUniqueIngID, currentUniqueIngCount);
            CutEquipmentCount(itemID, currentProd.rare_Item_ID);
        }
        else if (btnName.Contains("걸작장비"))
        {
            CutIngredientCount(currentProd.rare_Ingredient_1_ID, currentProd.rare_Ingredient_1_Count, currentProd.rare_Ingredient_2_ID, currentProd.rare_Ingredient_2_Count);
            CutEquipmentCount(itemID, currentProd.normal_ItemID);
        }
        else if (btnName.Contains("평작장비"))
        {
            CutIngredientCount(currentProd.normal_Ingredient_1_ID, currentProd.normal_Ingredient_1_Count, currentProd.normal_Ingredient_2_ID, currentProd.normal_Ingredient_2_Count);
            CutEquipmentCount(itemID);
        }
        SetCraftTree(currentProd.productionID);
    }
    //재료 개수 감소/삭제
    public void CutIngredientCount(int ing_1_ID, int ing_1_count, int ing_2_ID = 0, int ing_2_count = 0)
    {
        Inventory.Instance.RemoveIngredient(ing_1_ID, ing_1_count);
        if (ing_2_ID != 0)
        {
            Inventory.Instance.RemoveIngredient(ing_2_ID, ing_2_count);
        }
    }
    public void CutEquipmentCount(int targetItemID, int forgingItemID = 0)
    {
        //장비 개수 감소/삭제/삽입 
        bool isFound = false;
        if (forgingItemID == 0)
        {
            Inventory.Instance.AddEquipment(targetItemID);
        }
        else
        {
            for (int i = 0; i < pEquip.Count; i++)
            {
                if (pEquip[i].ID == forgingItemID)
                {
                    isFound = true;
                    DataManager.Instance.RemoveEquipInvenData(GameData.Instance.FindEquipmentByID(forgingItemID));
                    DataManager.Instance.AddEquipInvenData(GameData.Instance.FindEquipmentByID(targetItemID));
                    SetEquipItem();

                    
                    GameData.Instance.player.damage -= (int)GameData.Instance.FindEquipmentByID(forgingItemID).damage;
                    GameData.Instance.player.damage += (int)GameData.Instance.FindEquipmentByID(targetItemID).damage;

                    GameData.Instance.player.movespeed -= (int)GameData.Instance.FindEquipmentByID(forgingItemID).speed;
                    GameData.Instance.player.movespeed += (int)GameData.Instance.FindEquipmentByID(targetItemID).speed;

                    GameData.Instance.player.counter_judgement -= (int)GameData.Instance.FindEquipmentByID(forgingItemID).counterJudgement;
                    GameData.Instance.player.counter_judgement += (int)GameData.Instance.FindEquipmentByID(targetItemID).counterJudgement;

                    break;
                }
            }
            if (!isFound)
            {
                Inventory.Instance.RemoveEquipment(forgingItemID);
                Inventory.Instance.AddEquipment(targetItemID);
            }
        }
        pEquip = DataManager.Instance.EquipInvenData.CurrentEquipmentList;
    }
    IEnumerator ForgeCoroutine()
    {
        //BlockingLayer 켜기
        blockingLayer.SetActive(true);
        menuButtonBlockingLayer.SetActive(true);
        do
        {
            forgeBar.GetComponent<Slider>().value += 0.01f;
            yield return new WaitForSecondsRealtime(0.01f);
        } while (forgeBar.GetComponent<Slider>().value < forgeBar.GetComponent<Slider>().maxValue);
        forgeBar.GetComponent<Slider>().value = 0f;
        forgeBar.SetActive(false);
        forgeButton.SetActive(true);
        //BlockingLayer 끄기
        blockingLayer.SetActive(false);
        menuButtonBlockingLayer.SetActive(false);
        OnEquipmentInfoScreen(eqInfo.btnName, eqInfo.itemID);
    }
    IEnumerator ForgeSoundCoroutine()
    {
        SoundManager.Instance.PlayForgeSound("Hammer1");
        yield return new WaitForSecondsRealtime(1f);
        SoundManager.Instance.PlayForgeSound("Hammer2");
        yield return new WaitForSecondsRealtime(1f);
        SoundManager.Instance.PlayForgeSound("Hammer3");
    }
    #endregion
    #region 특수 재료 선택 창
    public void SelectSpecialIngredient(int num)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        if (currentProd != null)
        {
            switch (num)
            {
                case 0:
                    currentUniqueIngID = currentProd.unique_Ingredient_1_ID;
                    currentUniqueIngCount = currentProd.unique_Ingredient_1_Count;
                    OnOffUniqueEquipmentButton(1);
                    break;
                case 1:
                    currentUniqueIngID = currentProd.unique_Ingredient_2_ID;
                    currentUniqueIngCount = currentProd.unique_Ingredient_2_Count;
                    OnOffUniqueEquipmentButton(2);
                    break;
                case 2:
                    currentUniqueIngID = currentProd.unique_Ingredient_3_ID;
                    currentUniqueIngCount = currentProd.unique_Ingredient_3_Count;
                    OnOffUniqueEquipmentButton(3);
                    break;
            }
            SetItemImages(currentProd);
            SetAllText(currentProd);
            specialIngScreen.SetActive(false);
        }
    }
    public void SetSpecialIngredientImages()
    {
        ActiveSpecialIngScreen();
        spIng.itemImage_1.sprite = Inventory.Instance.itemImages[GameData.Instance.FindIngredientByID(currentProd.unique_Ingredient_1_ID).itemScriptID];
        spIng.itemImage_2.sprite = Inventory.Instance.itemImages[GameData.Instance.FindIngredientByID(currentProd.unique_Ingredient_2_ID).itemScriptID];
        spIng.itemImage_3.sprite = Inventory.Instance.itemImages[GameData.Instance.FindIngredientByID(currentProd.unique_Ingredient_3_ID).itemScriptID];
        SetSpecialIngCountText(currentProd);
    }
    public void OnOffUniqueEquipmentButton(int num)
    {
        switch (num)
        {
            case 1:
                craftButtons["명작장비 슬롯 1"].GetComponent<Button>().interactable = true;
                craftButtons["명작장비 슬롯 2"].GetComponent<Button>().interactable = false;
                craftButtons["명작장비 슬롯 3"].GetComponent<Button>().interactable = false;
                break;
            case 2:
                craftButtons["명작장비 슬롯 1"].GetComponent<Button>().interactable = false;
                craftButtons["명작장비 슬롯 2"].GetComponent<Button>().interactable = true;
                craftButtons["명작장비 슬롯 3"].GetComponent<Button>().interactable = false;
                break;
            case 3:
                craftButtons["명작장비 슬롯 1"].GetComponent<Button>().interactable = false;
                craftButtons["명작장비 슬롯 2"].GetComponent<Button>().interactable = false;
                craftButtons["명작장비 슬롯 3"].GetComponent<Button>().interactable = true;
                break;
        }
    }
    #region 특수재료 개수 텍스트 설정
    public void SetSpecialIngCountText(Production p)
    {
        //1번 특수재료 설정
        sb.Clear();
        if (Inventory.Instance.IsIngredientExist(p.unique_Ingredient_1_ID)) 
        { 
            sb.AppendFormat("{0} / {1}", Inventory.Instance.FindIngredient(p.unique_Ingredient_1_ID).count, p.unique_Ingredient_1_Count); 
            SetSpecialIngCountColor(spIng.countText_1, Inventory.Instance.FindIngredient(p.unique_Ingredient_1_ID).count, p.unique_Ingredient_1_Count);
        }
        else
        { 
            sb.AppendFormat("{0} / {1}", 0, p.unique_Ingredient_1_Count);
            spIng.countText_1.color = Color.red;
        }
        spIng.countText_1.text = sb.ToString();

        //2번 특수재료 설정
        sb.Clear();
        if (Inventory.Instance.IsIngredientExist(p.unique_Ingredient_2_ID)) 
        { 
            sb.AppendFormat("{0} / {1}", Inventory.Instance.FindIngredient(p.unique_Ingredient_2_ID).count, p.unique_Ingredient_2_Count);
            SetSpecialIngCountColor(spIng.countText_2, Inventory.Instance.FindIngredient(p.unique_Ingredient_2_ID).count, p.unique_Ingredient_2_Count);
        }
        else 
        { 
            sb.AppendFormat("{0} / {1}", 0, p.unique_Ingredient_2_Count);
            spIng.countText_2.color = Color.red;
        }
        spIng.countText_2.text = sb.ToString();

        //3번 특수재료 설정
        sb.Clear();
        if (Inventory.Instance.IsIngredientExist(p.unique_Ingredient_3_ID)) 
        { 
            sb.AppendFormat("{0} / {1}", Inventory.Instance.FindIngredient(p.unique_Ingredient_3_ID).count, p.unique_Ingredient_3_Count);
            SetSpecialIngCountColor(spIng.countText_3, Inventory.Instance.FindIngredient(p.unique_Ingredient_3_ID).count, p.unique_Ingredient_3_Count);
        }
        else 
        {
            sb.AppendFormat("{0} / {1}", 0, p.unique_Ingredient_3_Count);
            spIng.countText_3.color = Color.red;
        }
        spIng.countText_3.text = sb.ToString();
    }
    public void SetSpecialIngCountColor(TMP_Text text, int currentCount, int reqCount)
    {
        text.color = Compare(currentCount, reqCount) ? Color.white : Color.red;
    }
    public bool Compare(int a, int b)
    {
        if (a >= b)
        {
            return true;
        }
        return false;
    }
    #endregion
    #region 특수재료 선택창 On/Off
    public void ActiveSpecialIngScreen()
    {
        if (!specialIngScreen.activeSelf)
        {
            SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
            specialIngScreen.SetActive(true);
        }
    }
    public void InactiveSpecialIngScreen()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        if (specialIngScreen.activeSelf)
        {
            specialIngScreen.SetActive(false);
        }
    }
    #endregion
    #endregion
    #region 팝업이 떠있으면 끄기
    private void OnDisable()
    {
        //정보창 떠있으면 끄기
        if (ingredientInfoScreen.activeSelf) { ingredientInfoScreen.SetActive(false); }
        if (equipmentInfoScreen.activeSelf) { equipmentInfoScreen.SetActive(false); }
        if (specialIngScreen.activeSelf) { specialIngScreen.SetActive(false); }
    }
    #endregion
}