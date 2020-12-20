using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    static T s_instance = null;

    public static T Instance
    {
        get
        {
            if (s_instance == null)
            {
                CreateInstance();
            }

            return s_instance;
        }
    }

    static void CreateInstance()
    {
        if (s_instance == null)
        {
            s_instance = new T();
        }
    }
}