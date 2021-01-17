﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    public Transform portalName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("aa");
            GameData.Instance.player.isSceneMove = true;
            GameEventToUI.Instance.OnPlayerHp_Decrease(2);
            StartCoroutine(GameData.Instance.player.PlayerMovePosition(portalName.position));
        }
    }
}
