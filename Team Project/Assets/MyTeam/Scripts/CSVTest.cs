using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVTest : MonoBehaviour
{
    void Awake()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("data");
       

        for (var i = 0; i < data.Count; i++)
        {
            for (var k = 0; k < data[i].Count; k++)
            {
                Debug.Log(data[i]);
            }
            print("name " + data[i]["id"] + " " +
                   "age " + data[i]["age"] + " " +
                   "speed " + data[i]["speed"] + " " +
                   "desc " + data[i]["description"]);
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
