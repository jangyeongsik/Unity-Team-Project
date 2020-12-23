using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}


public class Player : character
{
    public float stamina;               //스태미너
    public float counter_judgement;     //카운터 판결
    public string p_name;               //이름
    public State.PlayerState m_state;  //상태


    public Player() { }
    Player(Transform t)                 //생성자
    {
        position = t;
    }

    

}
