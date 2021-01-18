using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoMonsters : MonoBehaviour
{
    private Monster viper;

    private void Start()
    {
        viper = GetComponent<Monster>();
        viper.position = transform;
        viper.monsterKind = State.MonsterKind.M_Viper;
    }

    private void EnemyVipersSet()
    {
        viper.monsterState = State.MonsterState.M_Idle;
        viper.animator = GetComponent<Animator>();
    }
}
