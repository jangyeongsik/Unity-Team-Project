using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonobehaviour<DataManager>
{
    public PlayerInven AllInvenData { get; set; }

    readonly JsonManager JsonInstance = JsonManager.Instance;

    private void Start()
    {
        ParsingInvenList();
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
    public void AddEquipmentData(Equipment _pInven)
    {
        if (!CheakDuplicateEquipmentData(_pInven))
        {
            AllInvenData.EquipmentList.Add(_pInven);
        }

        JsonInstance.CreateJsonFile(Application.dataPath, "/MyTeam/Resources/PlayerInvenData", AllInvenData);
    }
    bool CheakDuplicateEquipmentData(Equipment _pInven)
    {
        for (int i = 0; i < AllInvenData.EquipmentList.Count; ++i)
        {
            Equipment temp;
            temp = AllInvenData.EquipmentList[i];
            if (temp == null)
                continue;

            if (temp.ID == _pInven.ID)
            {
                AllInvenData.EquipmentList[i].count++;
                return true;
            }
        }
        return false;
    }
}