using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReader;
using System.IO;

public class GameData : SingletonMonobehaviour<GameData>
{
    private PlayerDataList playerDataList;
    int playerIdx = 0;
    public List<PlayerData> playerData;
    public Player player;
    public List<NPCReader.NPCTalk> data;
    public List<Equipment> equipmentData;
    public List<Ingredient> ingredientData;
    public List<Production> productionData;
    string playerFilePath;

    private void Start()
    {
        //data = CSVReaderNPC.CSVReaderNPC.FileRead("talkdata");
        Table table = CSVReader.Reader.ReadCSVToTable("EquipmentData");
        equipmentData = table.TableToList<Equipment>();
        Table ingred = CSVReader.Reader.ReadCSVToTable("IngredientData");
        ingredientData = ingred.TableToList<Ingredient>();
        Table prod = CSVReader.Reader.ReadCSVToTable("ProductionDB");
        productionData = prod.TableToList<Production>();
        //System.GC.Collect();
        data = CSVReaderNPC.CSVReaderNPC.FileRead("talkdata");

        playerFilePath = Application.persistentDataPath + "/PlayerData.json";
        PlayerLoad();
        System.GC.Collect();

    }

    public void Print()
    {
        foreach(Equipment a in equipmentData)
        {
            Debug.Log(a.ID + " " + a.name + " " + a.itemGrade);
        }
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    Print();
        //}

    }

    //아이템 매니저 함수들
    //===================================================================
    public Equipment FindEquipment(string itemname)
    {
        int i;
        for (i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i].name.Equals(itemname))
            {
                break;
            }
        }
        if (i >= equipmentData.Count)
            return null;
    
        Equipment p = new Equipment();
        p = equipmentData[i];

        return p;
    }
    public Equipment FindEquipmentByID(int _ID)
    {
        int i;
        for (i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i].ID.Equals(_ID))
            {
                break;
            }
        }
        if (i >= equipmentData.Count)
            return null;

        Equipment p = new Equipment();
        p = equipmentData[i];

        return p;
    }
    public Ingredient FindIngredientByID(int _ID)
    {
        int i;
        for (i = 0; i < ingredientData.Count; i++)
        {
            if (ingredientData[i].ID.Equals(_ID))
            {
                break;
            }
        }
        if (i >= ingredientData.Count)
            return null;

        Ingredient p = new Ingredient();
        p = ingredientData[i];

        return p;
    }
    //===================================================================
    //게임 끝날때 저장
    #region 플레이어 데이터 관련
    private void OnDestroy()
    {
        PlayerSave();
    }

    //플레이어 슬롯에서 데이터 읽기
    public void LoadFromPlayerSlot(int slot)
    {
        playerIdx = slot;
        playerData[slot].WriteData(player);
    }

    //플레이어 슬롯 새로만들기
    public void CreateNewPlayerSlot(int slot, string name)
    {
        playerData[slot].CreateNewPlayer(slot, name);
        PlayerSave();
    }

    public void DeletePlayerData(int slot)
    {
        playerData[slot].DeleteData(slot);
    }

    void CreateAllPlayerData()
    {
        playerDataList = new PlayerDataList();
        playerDataList.datas.Add(new PlayerData(0));
        playerDataList.datas.Add(new PlayerData(1));
        playerDataList.datas.Add(new PlayerData(2));
        playerData = playerDataList.datas;
        PlayerSave();
        PlayerLoad();
    }

    void PlayerSave()
    {
        playerData[playerIdx].CopyPlayer(player);
        playerDataList.datas = playerData;
        JsonManageAndroid.Instance.SaveJsonFile("PlayerData", playerDataList);
    }

    void PlayerLoad()
    {
        if(JsonManageAndroid.Instance.LoadJsonFile<PlayerDataList>("PlayerData") == null)
        {
            CreateAllPlayerData();
            return;
        }
        playerDataList = JsonManageAndroid.Instance.LoadJsonFile<PlayerDataList>("PlayerData");
        playerData = playerDataList.datas;
    }
    #endregion
}
