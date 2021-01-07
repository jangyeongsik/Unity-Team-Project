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
    public float defence;
    public float counterJudgement;
    public int presetID;
    public int cylinderCounter;
    public int cylinderPercent;
    public string[] playerSkillset = new string[3];

    public PlayerData(int slot)
    {
        CreateNewPlayer(slot,"");
    }

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
        player.cylinderCounter = cylinderCounter;
        player.cylinderPercent = cylinderPercent;
        player.playerSkillset = playerSkillset;
        return player;
    }

    public PlayerData CreateNewPlayer(int slot, string name)
    {
        slotID = slot;
        this.name = name;
        damage = 1;
        moveSpeed = 1;
        criticalPercent = 1;
        criticalDamage = 1;
        attackSpeed = 1;
        hp = 4;
        stamina = 40;
        defence = 0;
        counterJudgement = 1;
        presetID = 0;
        cylinderCounter = 0;
        cylinderPercent = 0;
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
        cylinderCounter = 0;
        cylinderPercent = 0;
        return this;
    }
    public void CopyPlayer(Player player)
    {
        slotID = player.id;
        name = player.p_name;
        damage = player.damage;
        moveSpeed = player.movespeed;
        criticalPercent = player.criticalpercent;
        criticalDamage = player.criticaldamage;
        attackSpeed = player.attackspeed;
        hp = player.hp;
        stamina = player.stamina;
        defence = player.defence;
        counterJudgement = player.counter_judgement;
        presetID = player.presetID;
        cylinderCounter = player.cylinderCounter;
        cylinderPercent = player.cylinderPercent;
        playerSkillset = player.playerSkillset;
    }
}

[Serializable]
public class Player
{
    public int id;                          //아이디 값
    public int hp;                          //채력
    public float movespeed;                 //움직임 속도 
    public float criticalpercent;           //크리티컬 확률
    public float criticaldamage;            //크리티컬 대미지
    public int damage;                      //공격력
    public float attackspeed;               //공격속도
    public float defence;                   //방어
    public Transform position;              //위치       
    public int cylinderCounter;             //실린더 개수
    public int cylinderPercent;             //실린더 게이지 저장
    public int stamina;                     //스태미너
    public float counter_judgement;         //카운터 판결
    public string p_name;                   //이름
    public State.PlayerState m_state;       //상태
    public int presetID;
    public float counterTime;               //카운터 판정 타임
    public float gravity;
    public bool isDashPossible;
    public PlayerSkill skill;
    public string[] playerSkillset = new string[3];
    public CharacterController controller;
    public Animator animator;
    public AnimatorOverrideController overrideController;
    public List<AnimationClip> orgList = new List<AnimationClip>();
    public List<AnimationClip> aniList = new List<AnimationClip>();
    public List<KeyValuePair<AnimationClip, AnimationClip>> applyList = new List<KeyValuePair<AnimationClip, AnimationClip>>();

    public Player() {
        id = 0;
        p_name = "";
        damage = 0;
        movespeed = 0;
        criticalpercent = 0;
        criticaldamage = 0;
        hp = 4;
        stamina = 0;
        defence = 0;
        counter_judgement = 0;
        presetID = 0;
        m_state = State.PlayerState.P_Idle;
        counterTime = 0;

    }
    Player(Transform t)                 //생성자
    {
        position = t;
    }
    public void SetGravity(float gr)
    {
        gravity = gr;
    }

    public void PlayerMovePosition(Vector3 pos)
    {
        Vector3 dir = pos - position.position;
        Debug.Log(dir + position.position);
        controller.Move(dir);
        Debug.Log(position.position);
        gravity = 0.9f;
    }
}
