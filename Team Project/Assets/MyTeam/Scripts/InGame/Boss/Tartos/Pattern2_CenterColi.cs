﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2_CenterColi : MonoBehaviour
{
    BossData bossData;
    float timer;
    float dotTime = 1.5f;

    private void Start()
    {
        bossData = transform.parent.parent.parent.GetComponent<BossData>();
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dotTime)
        {
            GameEventToUI.Instance.OnPlayerBossHit(bossData.position, 1, bossData.bossState);
            timer = 0;
        }
    }

}
