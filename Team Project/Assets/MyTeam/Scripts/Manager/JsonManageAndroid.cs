using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonManageAndroid : Singleton<JsonManageAndroid>
{
    public void SaveJsonFile(string fileName,object obj )
    {
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", JsonUtility.ToJson(obj,true));
    }

    public T LoadJsonFile<T>(string fileName)
    {
        if (!File.Exists(Application.persistentDataPath + "/" + fileName + ".json")) return default(T);
        return JsonUtility.FromJson<T>(File.ReadAllText(Application.persistentDataPath + "/" + fileName + ".json"));
    }
    public void DeleteJsonFile(string fileName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".json"))
        {
            File.Delete(Application.persistentDataPath + "/" + fileName + ".json");
        }
    }
}
