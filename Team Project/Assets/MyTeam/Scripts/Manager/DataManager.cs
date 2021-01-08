﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataManager : SingletonMonobehaviour<DataManager>
{
    public PlayerInven AllInvenData { get; set; }
    public PlayerEquipment EquipInvenData { get; set; }

    readonly JsonManager JsonInstance = JsonManager.Instance;

    private void Start()
    {
        ParsingInvenList();
        ParsingEquipInvenList();
    }

    private void ParsingEquipInvenList()
    {
        EquipInvenData = JsonInstance.LoadJsonFile<PlayerEquipment>(Application.dataPath, "/MyTeam/Resources/PlayerEquipData");
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

    void ParsingInvenList()
    {
        AllInvenData = JsonInstance.LoadJsonFile<PlayerInven>(Application.dataPath, "/MyTeam/Resources/PlayerInvenData");
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
    public void AddEquipmentData(Equipment _pItem, int count)
    {
        if (!CheakDuplicateEquipmentData(_pItem, count))
        {
            AllInvenData.EquipmentList.Add(_pItem);
            AddEquipmentData(_pItem, count - 1);
        }

        //JsonManageAndroid.Instance.SaveJsonFile("PlayerInvenData", AllInvenData);
        JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerInvenData", AllInvenData);
    }
    public void AddIngredientData(Ingredient _pItem, int count)
    {
        if (!CheakDuplicateIngredientData(_pItem, count))
        {
            AllInvenData.IngredientList.Add(_pItem);
            AddIngredientData(_pItem, count - 1);
        }
        //JsonManageAndroid.Instance.SaveJsonFile("PlayerInvenData", AllInvenData);
        JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerInvenData", AllInvenData);
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
                if (AllInvenData.EquipmentList[i].count > 1)
                {
                    AllInvenData.EquipmentList[i].count--;
                    break;
                }
                else
                {
                    AllInvenData.EquipmentList.Remove(AllInvenData.EquipmentList[i]);
                    break;
                }
            }
        }
        JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerInvenData", AllInvenData);
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
                if (AllInvenData.IngredientList[i].count > 1)
                {
                    AllInvenData.IngredientList[i].count -= count;
                    break;
                }
                else
                {
                    AllInvenData.IngredientList.Remove(AllInvenData.IngredientList[i]);
                    break;
                }
            }
        }
        JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerInvenData", AllInvenData);
    }
    //장비리스트
    public void AddEquipInvenData(Equipment _pItem)
    {
        EquipInvenData.CurrentEquipmentList.Add(_pItem);
        JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerEquipData", EquipInvenData);
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
        JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerEquipData", EquipInvenData);
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
        for (int i = 0; i < AllInvenData.IngredientList.Count; i++)
        {
            Ingredient temp;
            temp = AllInvenData.IngredientList[i];
            if (temp == null)
                continue;

            if (temp.ID == _pInven.ID)
            {
                AllInvenData.IngredientList[i].count += count;
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