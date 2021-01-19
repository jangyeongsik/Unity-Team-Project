using UnityEngine;
using System.IO;
using System.Text;

public class JsonManager : Singleton<JsonManager>
{
    #region 데이터 형변환용
    public string ObjectToJson(object _obj)
    {
        return JsonUtility.ToJson(_obj, true);
    }
    public T JsonToObject<T>(string _jsonData)
    {
        return JsonUtility.FromJson<T>(_jsonData);
    }
    #endregion
    #region Json파일 읽기 / 쓰기
    public void CreateJsonFile(string _createPath, string _fileName, object _obj)
    {
        CreateJsonFile(_createPath, _fileName, ObjectToJson(_obj));
    }
    public void CreateJsonFile(string _createPath, string _fileName, string _jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", _createPath, _fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(_jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
    public T LoadJsonFile<T>(string _loadPath, string _fileName)
    {
        if (File.Exists(string.Format("{0}/{1}.json", _loadPath, _fileName)))
        {
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", _loadPath, _fileName), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(jsonData);
        }
        else
        {
            return default;
        }
    }
    #endregion
}
