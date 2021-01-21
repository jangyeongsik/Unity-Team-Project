using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReader;
using System.IO;
using UnityEngine.SceneManagement;

//아이템 이미지 딕셔너리
[Serializable]
public class IntSprite : SerializableDictionary<int, Sprite>
{ }
public class GameData : SingletonMonobehaviour<GameData>
{
    //아이템 이미지 딕셔너리
    public IntSprite itemImages;

    private PlayerDataList playerDataList;
    public int playerIdx = 0;
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
    public int Talk_Find_index(int id)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].id == id) return i;
        }
        return 999;
    }
    public void Print()
    {
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < data[i].talk_name.Count; j++)
            {
                //Debug.Log(data[i].talk_name[j]);
            }
            for (int c = 0; c < data[i].talk.Count; c++)
            {
                //Debug.Log(data[i].talk[c]);
            }
        }
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

        return EquipmentDeepCopy(equipmentData[i]);
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
        
        return IngredientDeepCopy(ingredientData[i]);
    }
    private Ingredient IngredientDeepCopy(Ingredient a)
    {
        Ingredient temp = new Ingredient();
        temp.ID = a.ID;
        temp.name = a.name;
        temp.itemCategory = a.itemCategory;
        temp.itemGrade = a.itemGrade;
        temp.itemScriptID = a.itemScriptID;
        temp.count = a.count;
        return temp;
    }
    private Equipment EquipmentDeepCopy(Equipment e)
    {
        Equipment temp = new Equipment();
        temp.ID = e.ID;
        temp.name = e.name;
        temp.itemCategory = e.itemCategory;
        temp.equipmentType = e.equipmentType;
        temp.itemGrade = e.itemGrade;
        temp.itemScriptID = e.itemScriptID;
        temp.damage = e.damage;
        temp.speed = e.speed;
        temp.critPercent = e.critPercent;
        temp.critDamage = e.critDamage;
        temp.HPAdd = e.HPAdd;
        temp.staminaAdd = e.staminaAdd;
        temp.counterJudgement = e.counterJudgement;
        temp.count = e.count;
        temp.productionID = e.productionID;
        return temp;
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
        player = playerData[slot].WriteData(player);
        PlayerSave();
    }

    //플레이어 슬롯 새로만들기
    public void CreateNewPlayerSlot(int slot, string name)
    {
        playerData[slot].CreateNewPlayer(slot, name);
        player = playerData[slot].WriteData(player);
        PlayerSave();
    }

    public void DeletePlayerData(int slot)
    {
        playerData[slot].DeleteData(slot);
        playerData[slot].WriteData(in player);
        player.DeletePlayer();
        PlayerSave();
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

    public void PlayerSave()
    {
        playerData[playerIdx].CopyPlayer(player);
        playerDataList.datas = playerData;
        JsonManageAndroid.Instance.SaveJsonFile("PlayerData", playerDataList);
    }
    void PlayerLoad()
    {
        if (!File.Exists(playerFilePath))
        {
            CreateAllPlayerData();
            return;
        }
        playerDataList = JsonManageAndroid.Instance.LoadJsonFile<PlayerDataList>("PlayerData");
        playerData = playerDataList.datas;
    }
    #endregion
}
