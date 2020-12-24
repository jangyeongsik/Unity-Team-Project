using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReader;

public class GameData : SingletonMonobehaviour<GameData>
{
    public Player player;
    public List<NPCReader.NPCTalk> data;
    public List<Equipment> equipmentData;
    private void Start()
    {
        Table table = CSVReader.Reader.ReadCSVToTable("EquipmentData");
        data = CSVReaderNPC.CSVReaderNPC.FileRead("talkdata");
        equipmentData = table.TableToList<Equipment>();
        System.GC.Collect();

        player = new Player();

        Table t = CSVReader.Reader.ReadCSVToTable("PlayerDataCSV");
        PlayerData[] playerDatas = t.TableToArray<PlayerData>();
        player = playerDatas[0].WriteData(player);
    }

    public void Print()
    {
        foreach(Equipment a in equipmentData)
        {
            Debug.Log(a.ID + " " + a.Name + " " + a.counterJudgement);
        }
       
    }

    //아이템 매니저 함수들
    //===================================================================

    //
    public PlayerInventory findEquipment(string itemname)
    {
        int i;
        for (i = 0;i < equipmentData.Count; i++)
        {
            if (equipmentData[i].Name.Equals(itemname))
            {
                break;
            }        
        }
        if (i >= equipmentData.Count)
            return null;

        PlayerInventory p = new PlayerInventory();
        p.ID = equipmentData[i].ID;
        p.name = equipmentData[i].Name;
        p.scriptName = equipmentData[i].itemScriptID;
        p.itemCategory = ItemCategory.Equipment;
        p.count = 1;
        return p;
    }

    //===================================================================
}
