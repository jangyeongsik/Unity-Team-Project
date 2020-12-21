using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkdataTest : SingletonMonobehaviour<talkdataTest>
{
    public List<NPCReader.NPCTalk> data;
    private void Start()
    {
        data = CSVReaderNPC.CSVReaderNPC.FileRead("talkdata");
    }
}
