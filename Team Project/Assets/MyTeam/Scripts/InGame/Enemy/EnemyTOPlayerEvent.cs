using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTOPlayerEvent : Singleton<EnemyTOPlayerEvent>
{
    public System.Action EnemyTargeting;

    public void onEnemyTargeting()
    {
        EnemyTargeting();
    }
}
