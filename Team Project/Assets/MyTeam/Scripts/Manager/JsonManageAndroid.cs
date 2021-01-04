using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonManageAndroid : Singleton<JsonManageAndroid>
{
    public void CreateJsonFile(string fileName,object obj )
    {
        File.WriteAllText(Application.persistentDataPath + "/Datas/" + fileName + ".json", JsonUtility.ToJson(obj,true));
    }

    public T LoadJsonFile<T>(string fileName)
    {
        return JsonUtility.FromJson<T>(File.ReadAllText(Application.persistentDataPath + "/Datas/" + fileName + ".json"));
    }
}
