using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

public class DataManager : SingletonMonobehaviour<DataManager>
{
    public PlayerInven AllInvenData { get; set; }
    public PlayerEquipment EquipInvenData { get; set; }

    public List<Equipment> e;
    public List<Ingredient> i;
    public List<Misc> m;
    public List<Equipment> pE;
    public int slotIdx = 0;
    private string currentInvenDataName;
    private string currentEquipDataName;

    private void Start()
    {
        SetString();
        ParsingInvenList();
        ParsingEquipInvenList();
        e = AllInvenData.EquipmentList;
        i = AllInvenData.IngredientList;
        m = AllInvenData.MiscList;
        pE = EquipInvenData.CurrentEquipmentList;
    }
    public void SetString()
    {
        switch (slotIdx)
        {
            case 0:
                currentInvenDataName = "PlayerInvenData0";
                currentEquipDataName = "PlayerEquipData0";
                break;
            case 1:
                currentInvenDataName = "PlayerInvenData1";
                currentEquipDataName = "PlayerEquipData1";
                break;
            case 2:
                currentInvenDataName = "PlayerInvenData2";
                currentEquipDataName = "PlayerEquipData2";
                break;
        }
    }
    public void DeleteData()
    {
        AllInvenData.EquipmentList.Clear();
        AllInvenData.IngredientList.Clear();
        AllInvenData.MiscList.Clear();
        EquipInvenData.CurrentEquipmentList.Clear();
        InvenSave();
        InvenLoad();
        EquipSave();
        EquipLoad();
    }

    public void InvenLoad()
    {
        if (JsonManageAndroid.Instance.LoadJsonFile<PlayerInven>(currentInvenDataName) == null)
        {
            CreateInvenData();
        }
        else
        {
            AllInvenData = JsonManageAndroid.Instance.LoadJsonFile<PlayerInven>(currentInvenDataName);
        }
        e = AllInvenData.EquipmentList;
        i = AllInvenData.IngredientList;
        m = AllInvenData.MiscList;
    }
    private void InvenSave()
    {
        JsonManageAndroid.Instance.SaveJsonFile(currentInvenDataName, AllInvenData);
    }
    private void CreateInvenData()
    {
        AllInvenData = new PlayerInven();
        AllInvenData.EquipmentList = new List<Equipment>();
        AllInvenData.IngredientList = new List<Ingredient>();
        AllInvenData.MiscList = new List<Misc>();
        InvenSave();
        InvenLoad();
    }
    public void EquipLoad()
    {
        if (JsonManageAndroid.Instance.LoadJsonFile<PlayerEquipment>(currentEquipDataName) == null)
        {
            CreateEquipData();
        }
        else
        {
            EquipInvenData = JsonManageAndroid.Instance.LoadJsonFile<PlayerEquipment>(currentEquipDataName);
        }
        pE = EquipInvenData.CurrentEquipmentList;
    }
    private void EquipSave()
    {
        JsonManageAndroid.Instance.SaveJsonFile(currentEquipDataName, EquipInvenData);
    }
    private void CreateEquipData()
    {
        EquipInvenData = new PlayerEquipment();
        EquipInvenData.CurrentEquipmentList = new List<Equipment>();
        EquipSave();
        EquipLoad();
    }
    private void ParsingEquipInvenList()
    {
        EquipLoad();
        if (EquipInvenData != null)
        {
            for (int i = 0; i < EquipInvenData.CurrentEquipmentList.Count; ++i)
            {
                Equipment data = EquipInvenData.CurrentEquipmentList[i];
                if (data == null)
                    Debug.Log(string.Format("{0} 번째 장비 정보가 널값임", i));
            }
        }
    }
    private void ParsingInvenList()
    {
        InvenLoad();
        if (AllInvenData != null)
        {
            for (int i = 0; i < AllInvenData.EquipmentList.Count; ++i)
            {
                Equipment data = AllInvenData.EquipmentList[i];
                if (data == null)
                    Debug.Log(string.Format("{0} 번째 장비 정보가 널값임", i));
            }
            for (int i = 0; i < AllInvenData.IngredientList.Count; ++i)
            {
                Ingredient data = AllInvenData.IngredientList[i];
                if (data == null)
                    Debug.Log(string.Format("{0} 번째 재료 정보가 널값임", i));
            }
            for (int i = 0; i < AllInvenData.MiscList.Count; ++i)
            {
                Misc data = AllInvenData.MiscList[i];
                if (data == null)
                    Debug.Log(string.Format("{0} 번째 기타 정보가 널값임", i));
            }
        }
    }
    public int FindEquipmentScriptID(EQUIPMENTTYPE e)
    {
        for (int i = 0; i < EquipInvenData.CurrentEquipmentList.Count; i++)
        {
            if (EquipInvenData.CurrentEquipmentList[i].equipmentType == e)
            {
                return EquipInvenData.CurrentEquipmentList[i].itemScriptID;
            }
        }
        return 0;
    }
    public Equipment FindEquipment(EQUIPMENTTYPE e)
    {
        for (int i = 0; i < EquipInvenData.CurrentEquipmentList.Count; i++)
        {
            if (EquipInvenData.CurrentEquipmentList[i].equipmentType == e)
            {
                return EquipInvenData.CurrentEquipmentList[i];
            }
        }
        return null;
    }
    public void AddEquipmentData(Equipment _pItem, int count)
    {
        if (!CheakDuplicateEquipmentData(_pItem, count))
        {
            AllInvenData.EquipmentList.Add(_pItem);
            if (count - 1 > 0)
            {
                AddEquipmentData(_pItem, count - 1);
            }
        }
        JsonManageAndroid.Instance.SaveJsonFile(currentInvenDataName, AllInvenData);
        //JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerInvenData", AllInvenData);
    }
    public void AddIngredientData(Ingredient _pItem, int count)
    {
        if (!CheakDuplicateIngredientData(_pItem, count))
        {
            AllInvenData.IngredientList.Add(_pItem);
            if (count - 1 > 0)
            {
                AddIngredientData(_pItem, count - 1);
            }
        }
        JsonManageAndroid.Instance.SaveJsonFile(currentInvenDataName, AllInvenData);
        InvenLoad();
        //JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerInvenData", AllInvenData);
    }
    public void RemoveEquipmentData(Equipment _pItem)
    {
        for (int i = 0; i < AllInvenData.EquipmentList.Count; i++)
        {
            Equipment temp;
            temp = AllInvenData.EquipmentList[i];
            if (temp == null) continue;
            if (temp.ID == _pItem.ID)
            {
                if (AllInvenData.EquipmentList[i].count - 1 <= 0)
                {
                    AllInvenData.EquipmentList.RemoveAt(i);
                }
                else
                {
                    AllInvenData.EquipmentList[i].count--;
                }
                break;
            }
        }
        JsonManageAndroid.Instance.SaveJsonFile(currentInvenDataName, AllInvenData);
        //JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerInvenData", AllInvenData);
    }
    public void RemoveIngredientData(Ingredient _pItem, int count = 1)
    {
        for (int i = 0; i < AllInvenData.IngredientList.Count; i++)
        {
            Ingredient temp;
            temp = AllInvenData.IngredientList[i];
            if (temp == null) continue;
            if (temp.ID == _pItem.ID)
            {
                if (AllInvenData.IngredientList[i].count - count <= 0)
                {
                    AllInvenData.IngredientList.RemoveAt(i);
                }
                else
                {
                    AllInvenData.IngredientList[i].count -= count;
                }
                break;
            }
        }
        JsonManageAndroid.Instance.SaveJsonFile(currentInvenDataName, AllInvenData);
        //JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerInvenData", AllInvenData);
    }
    //장비리스트
    public void AddEquipInvenData(Equipment _pItem)
    {
        EquipInvenData.CurrentEquipmentList.Add(_pItem);
        JsonManageAndroid.Instance.SaveJsonFile(currentEquipDataName, EquipInvenData);
        //JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerEquipData", EquipInvenData);
    }
    public void RemoveEquipInvenData(Equipment _pItem)
    {
        for (int i = 0; i < EquipInvenData.CurrentEquipmentList.Count; i++)
        {
            if (EquipInvenData.CurrentEquipmentList[i].equipmentType == _pItem.equipmentType)
            {
                EquipInvenData.CurrentEquipmentList.Remove(EquipInvenData.CurrentEquipmentList[i]);
            }
        }
        JsonManageAndroid.Instance.SaveJsonFile(currentEquipDataName, EquipInvenData);
        //JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerEquipData", EquipInvenData);
    }
    public bool IsEquipmentExist(int itemID)
    {
        Equipment _pItem = GameData.Instance.FindEquipmentByID(itemID);
        for (int i = 0; i < EquipInvenData.CurrentEquipmentList.Count; i++)
        {
            if (EquipInvenData.CurrentEquipmentList[i].ID == _pItem.ID)
            {
                return true;
            }
        }
        return false;
    }
    bool CheakDuplicateEquipmentData(Equipment _pInven, int count)
    {
        for (int i = 0; i < AllInvenData.EquipmentList.Count; i++)
        {
            Equipment temp;
            temp = AllInvenData.EquipmentList[i];
            if (temp == null)
                continue;

            if (temp.ID == _pInven.ID)
            {
                AllInvenData.EquipmentList[i].count += count;
                return true;
            }
        }
        return false;
    }
    bool CheakDuplicateIngredientData(Ingredient _pInven, int count)
    {
        foreach(Ingredient temp in AllInvenData.IngredientList)
        {
            if (temp == null)
                continue;

            if (temp.ID == _pInven.ID)
            {
                temp.count += count;
                return true;
            }
        }
        return false;
    }
    #region 정렬
    //내림차순 (큰 수 부터 작은 수)
    public void SortByIDDecending(int _InvenTabNum)
    {
        switch (_InvenTabNum)
        {
            case 0:
                AllInvenData.EquipmentList = AllInvenData.EquipmentList.OrderByDescending(x => x.ID).ToList();
                break;
            case 1:
                AllInvenData.IngredientList = AllInvenData.IngredientList.OrderByDescending(x => x.ID).ToList();
                break;
            case 2:
                AllInvenData.MiscList = AllInvenData.MiscList.OrderByDescending(x => x.ID).ToList();
                break;
        }
    }
    //오름차순 (작은 수 부터 큰 수)
    public void SortByIDAscending(int _InvenTabNum)
    {
        switch (_InvenTabNum)
        {
            case 0:
                AllInvenData.EquipmentList = AllInvenData.EquipmentList.OrderBy(x => x.itemGrade).ToList();
                break;
            case 1:
                AllInvenData.IngredientList = AllInvenData.IngredientList.OrderBy(x => x.ID).ToList();
                break;
            case 2:
                AllInvenData.MiscList = AllInvenData.MiscList.OrderBy(x => x.ID).ToList();
                break;
        }
    }
    //이름 정렬 (ABC순)
    public void SortByName(int _InvenTabNum)
    {
        switch (_InvenTabNum)
        {
            case 0:
                AllInvenData.EquipmentList = AllInvenData.EquipmentList.OrderBy(x => x.name).ToList();
                break;
            case 1:
                AllInvenData.IngredientList = AllInvenData.IngredientList.OrderBy(x => x.name).ToList();
                break;
            case 2:
                AllInvenData.MiscList = AllInvenData.MiscList.OrderBy(x => x.name).ToList();
                break;
        }
    }
    #endregion
}