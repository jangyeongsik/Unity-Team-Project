using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCount : Singleton<EnemyAttackCount>
{
    public bool counterjudgement;

    float attackTime;

    float attckCountMin = 0.7f;
    float attckCountMax = 1.5f;
    void attackCount()
    {
        attackTime += Time.deltaTime;

        if (attackTime > attckCountMin && attackTime < attckCountMax)
        {
            EnemyAttackCount.Instance.counterjudgement = true;
        }
        else
        {
            EnemyAttackCount.Instance.counterjudgement = false;
        }
    }
}
