using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class filestream : Singleton<filestream>
{
    public void PlayerSave()
    {
        //FileStream fs = File.Create(@"Assets/MyTeam/Resources/PlayerDataCSV.csv");
        //StreamWriter sw = new StreamWriter(fs);
        //sw.WriteLine("slotID,Name,DAMAGE,MOVESPEED,CRITICALPERCENT,CRITICALDAMAGE,ATTACKSPEED,HP,STAMINA,DEFENCE,COUNTERJUDGEMENT,PresetID");
        //sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", GameData.Instance.playerData[0].slotID, GameData.Instance.playerData[0].name, GameData.Instance.playerData[0].damage, GameData.Instance.playerData[0].moveSpeed,
        //    GameData.Instance.playerData[0].criticalPercent, GameData.Instance.playerData[0].criticalDamage, GameData.Instance.playerData[0].attackSpeed, GameData.Instance.playerData[0].hp,
        //      GameData.Instance.playerData[0].stamina, GameData.Instance.playerData[0].defence, GameData.Instance.playerData[0].counterJudgement, GameData.Instance.playerData[0].presetID);
        //sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", GameData.Instance.playerData[1].slotID, GameData.Instance.playerData[1].name, GameData.Instance.playerData[1].damage, GameData.Instance.playerData[1].moveSpeed,
        //    GameData.Instance.playerData[1].criticalPercent, GameData.Instance.playerData[1].criticalDamage, GameData.Instance.playerData[1].attackSpeed, GameData.Instance.playerData[1].hp,
        //      GameData.Instance.playerData[1].stamina, GameData.Instance.playerData[1].defence, GameData.Instance.playerData[1].counterJudgement, GameData.Instance.playerData[1].presetID);
        //sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", GameData.Instance.playerData[2].slotID, GameData.Instance.playerData[2].name, GameData.Instance.playerData[2].damage, GameData.Instance.playerData[2].moveSpeed,
        //    GameData.Instance.playerData[2].criticalPercent, GameData.Instance.playerData[2].criticalDamage, GameData.Instance.playerData[2].attackSpeed, GameData.Instance.playerData[2].hp,
        //      GameData.Instance.playerData[2].stamina, GameData.Instance.playerData[2].defence, GameData.Instance.playerData[2].counterJudgement, GameData.Instance.playerData[2].presetID);
        //sw.Close();
        //fs.Close();
    }
    
}
