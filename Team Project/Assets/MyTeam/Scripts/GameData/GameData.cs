using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVReader;

public class GameData : SingletonMonobehaviour<GameData>
{
    public PlayerData[] playerData;
    public int playerSlot;
    public Player player;
    public List<NPCReader.NPCTalk> data;
    public List<Equipment> equipmentData;

    private void Start()
    {
        //data = CSVReaderNPC.CSVReaderNPC.FileRead("talkdata");
        Table table = CSVReader.Reader.ReadCSVToTable("EquipmentData");
        equipmentData = table.TableToList<Equipment>();
        //System.GC.Collect();

        player = new Player();

        Table t = CSVReader.Reader.ReadCSVToTable("PlayerDataCSV");
        playerData = t.TableToArray<PlayerData>();
        player = playerData[0].WriteData(player);
    }

    public void Print()
    {
        foreach(Equipment a in equipmentData)
        {
            Debug.Log(a.ID + " " + a.Name + " " + a.itemGrade);
        }
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Print();
        }
    }

    //아이템 매니저 함수들
    //===================================================================
    public Equipment findEquipment(string itemname)
    {
        int i;
        for (i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i].Name.Equals(itemname))
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
    public PlayerInventory findEquipmentAsPlayerInventoryItem(string itemname)
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
        p.itemGrade = equipmentData[i].itemGrade;
        p.itemCategory = ITEMCATEGORY.EQUIPMENT;
        p.count = 1;
        return p;
    }

    //===================================================================

}
