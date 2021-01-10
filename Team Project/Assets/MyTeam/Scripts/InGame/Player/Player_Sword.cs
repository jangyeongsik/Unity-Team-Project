﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sword : MonoBehaviour
{
    public GameObject[] Swords;

    private void Start()
    {
        UIEventToGame.Instance.SwordChangeEvent += OnSwordChange;
        for (int i = 1; i < Swords.Length; i++)
        {
            Swords[i].SetActive(false);
        }
    }
    public void OnSwordChange(int index)
    {
        index -= 1;
        for (int i = 0;i < Swords.Length; i++)
        {
            Swords[i].SetActive(false);
        }
        Swords[index].SetActive(true);
    }
}