using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerDataList
{
    public List<PlayerData> datas = new List<PlayerData>();
}

[Serializable]
public class PlayerData
{
    public int slotID;
    public string name;
    public int damage;
    public float moveSpeed;
    public float criticalPercent;
    public float criticalDamage;
    public float attackSpeed;
    public int hp;
    public int stamina;
    public int defence;
    public float counterJudgement;
    public int presetID;
    public int cylinderCounter;

    public Player WriteData(in Player player)
    {
        player.id = slotID;
        player.p_name = name;
        player.damage = damage;
        player.movespeed = moveSpeed;
        player.criticalpercent = criticalPercent;
        player.criticaldamage = criticalDamage;
        player.attackspeed = attackSpeed;
        player.hp = hp;
        player.stamina = stamina;
        player.defence = defence;
        player.counter_judgement = counterJudgement;
        player.presetID = presetID;
        player.m_state = State.PlayerState.P_Idle;
        player.counterTime = 0f;
        return player;
    }

    public PlayerData CreateNewPlayer(int slot, string name)
    {
        slotID = slot;
        this.name = name;
        damage = 0;
        moveSpeed = 0;
        criticalPercent = 0;
        criticalDamage = 0;
        attackSpeed = 0;
        hp = 0;
        stamina = 0;
        defence = 0;
        counterJudgement = 0;
        presetID = 0;
        return this;
    }
    public PlayerData DeleteData(int slot)
    {
        slotID = slot;
        this.name = "";
        damage = 0;
        moveSpeed = 0;
        criticalPercent = 0;
        criticalDamage = 0;
        attackSpeed = 0;
        hp = 0;
        stamina = 0;
        defence = 0;
        counterJudgement = 0;
        presetID = 0;
        return this;
    }
}

public class Player : character
{
    public int cylinderCounter;         //실린더 개수
    public float stamina;               //스태미너
    public float counter_judgement;     //카운터 판결
    public string p_name;               //이름
    public State.PlayerState m_state;  //상태
    public int presetID;

    public float counterTime;           //카운터 판정 타임

    public Player() { }
    Player(Transform t)                 //생성자
    {
        position = t;
    }

    

}
