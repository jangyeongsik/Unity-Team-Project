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
        Table table = CSVReader.Reader.ReadCSVToTable("equipmentData");
        data = CSVReaderNPC.CSVReaderNPC.FileRead("talkdata");
        equipmentData = table.TableToList<Equipment>();
        System.GC.Collect();
        string a = "abc";
        string b = "abc";
        if (a.Equals(b))
        {
            Debug.Log("A");
        }
        //this.Print();

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
}
