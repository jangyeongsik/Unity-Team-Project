using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class filestream : Singleton<filestream>
{
    public void PlayerSave()
    {
        FileStream fs = File.Create(@"Assets/MyTeam/Resources/PlayerDataCSV.csv");
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("slotID,Name,DAMAGE,MOVESPEED,CRITICALPERCENT,CRITICALDAMAGE,ATTACKSPEED,HP,STAMINA,DEFENCE,COUNTERJUDGEMENT,PresetID");
        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", GameData.Instance.player.id, GameData.Instance.player.p_name, GameData.Instance.player.damage, GameData.Instance.player.movespeed,
            GameData.Instance.player.criticalpercent, GameData.Instance.player.criticaldamage, GameData.Instance.player.attackspeed, GameData.Instance.player.hp,
              GameData.Instance.player.stamina, GameData.Instance.player.defence, GameData.Instance.player.counter_judgement, GameData.Instance.player.presetID);
        sw.Close();
        fs.Close();
    }
    
}
