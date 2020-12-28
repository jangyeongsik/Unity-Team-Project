using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerInven AllInvenData { get; set; }

    readonly JsonManager JsonInstance = JsonManager.Instance;

    private void Start()
    {
        ParsingInvenList();
    }
    void ParsingInvenList()
    {
        AllInvenData = JsonInstance.LoadJsonFile<PlayerInven>(Application.dataPath, "InvenData/playerInvenData");
        if (AllInvenData != null)
        {
            for (int i = 0; i < AllInvenData.ListData.Count; ++i)
            {
                PlayerInventory data = AllInvenData.ListData[i];
                if (data == null)
                    Debug.Log(string.Format("{0} 번째 인벤토리 정보가 널값임", i));
            }
        }
    }
    public void AddItemData(PlayerInventory _pInven)
    {
        if (CheakDuplicateTranineeData(_pInven))
        {
            AllInvenData.ListData.Add(_pInven);
        }

        JsonInstance.CreateJsonFile(Application.dataPath, "InvenData/playerInvenData", AllInvenData);
    }
    bool CheakDuplicateTranineeData(PlayerInventory _pInven)
    {
        for (int i = 0; i < AllInvenData.ListData.Count; ++i)
        {
            PlayerInventory temp;
            temp = AllInvenData.ListData[i];
            if (temp == null)
                continue;

            if (temp.ID == _pInven.ID)
            {
                AllInvenData.ListData[i].count++;
                return true;
            }
        }

        return false;
    }
}