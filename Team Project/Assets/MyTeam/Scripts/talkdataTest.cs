using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkdataTest : SingletonMonobehaviour<talkdataTest>
{
    public List<NPCReader.NPCTalk> data;
    private void Start()
    {
        data = CSVReaderNPC.CSVReaderNPC.FileRead("talkdata");
        for (int i = 0; i < data.Count; i++)
        {
            Debug.Log(data[i].id + "\n" + data[i].name + "\n");
            for (int l = 0; l < data[i].talk.Count; l++)
            {
                Debug.Log(data[i].talk[l] + " ");
            }
        }
    }
}
