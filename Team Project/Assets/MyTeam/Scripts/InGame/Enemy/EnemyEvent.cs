using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : SingletonMonobehaviour<EnemyEvent>
{
    public event System.Action EnemyResetTime;

    public void OnEnemyResetTime()
    {
        EnemyResetTime();
    }
}
