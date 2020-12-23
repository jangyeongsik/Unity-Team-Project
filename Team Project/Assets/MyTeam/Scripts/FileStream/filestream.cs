using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class filestream : Singleton<filestream>
{
    public void Save()
    {
        FileStream fs = File.Create(@"Assets/MyTeam/Resources/PlayerData1.csv");
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("slotID,Name,DAMAGE,123,123,123,123,HP,123,123,123,PresetID");
        
        sw.Close();
        fs.Close();
    }
    
}
