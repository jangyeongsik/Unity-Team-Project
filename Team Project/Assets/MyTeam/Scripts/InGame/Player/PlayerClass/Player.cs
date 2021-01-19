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

    public int id;                              //아이디 값
    public int hp;                              //채력
    public float movespeed;                     //움직임 속도 
    public float criticalpercent;               //크리티컬 확률
    public float criticaldamage;                //크리티컬 대미지
    public int damage;                          //공격력
    public float attackspeed;                   //공격속도
    public float defence;                       //방어
    public int cylinderCounter;                 //실린더 개수
    public int keyCounter;                      //키 개수
    public int cylinderPercent;                 //실린더 게이지 저장
    public int stamina;                         //스태미너
    public float counter_judgement;             //카운터 판결
    public string p_name;                       //이름
    public bool tutorial;                       //튜토리얼 대사 출력 여부
    public int presetID;                        //뭔지 모르겠음
    public bool bossClear;                      //중간보스 클리어여부
    public int[] skillIdx;                      //현재 선택한 스킬
    public string SaveSceneName;                //체크포인트 씬
    public string SavePortalName;               //저장한 씬으로 갈 포탈이름
    public bool[] Talk_Box;                     //대화 시스템 관리
    public List<StageData> stageData;           //스테이지 데이터
    public float currentHp;                       //현재 체력

    public PlayerData(int slot)
    {
        CreateNewPlayer(slot, "");
    }

    //플레이어 데이터에 있는것들을 플레이어에 넣어준다
    public Player WriteData(in Player player)
    {

        player.id = id;
        player.hp = hp;
        player.currentHp = currentHp;
        player.movespeed = movespeed;
        player.criticalpercent = criticalpercent;
        player.criticaldamage = criticaldamage;
        player.damage = damage;
        player.attackspeed = attackspeed;
        player.defence = defence;
        player.cylinderCounter = cylinderCounter;
        player.keyCounter = keyCounter;
        player.cylinderPercent = cylinderPercent;
        player.stamina = stamina;
        player.counter_judgement = counter_judgement;
        player.p_name = p_name;
        player.tutorial = tutorial;
        player.presetID = presetID;
        player.bossClear = bossClear;
        player.skillIdx = skillIdx;
        player.SaveSceneName = SaveSceneName;
        player.SavePortalName = SavePortalName;
        player.Talk_Box = Talk_Box;
        player.stageData = stageData;
        player.D_stageData.Clear();
        for(int i = 0; i < stageData.Count; ++i)
        {
            if(!player.D_stageData.ContainsKey(stageData[i].key))
            {
                Debug.Log("왜");
                player.D_stageData.Add(stageData[i].key, stageData[i].value);
            }
        }

        return player;
    }

    //새로운 플레이어 데이터를 만든다
    public PlayerData CreateNewPlayer(int slot, string name)
    {

        id = slot;
        hp = 4;
        currentHp = 4;
        movespeed = 1;
        criticalpercent = 1;
        criticaldamage = 1;
        damage = 1;
        attackspeed = 1;
        defence = 1;
        cylinderCounter = 0;
        keyCounter = 0;
        cylinderPercent = 0;
        stamina = 40;
        counter_judgement = 1;
        p_name = name;
        tutorial = false;
        presetID = slot;
        bossClear = false;
        skillIdx = new int[3] { 1, 2, 3 };
        SaveSceneName = "MAP001";
        SavePortalName = "MAP001";
        Talk_Box = new bool[28];
        stageData = new List<StageData>();

        return this;
    }
    //플레이어 데이터를 초기화한다
    public PlayerData DeleteData(int slot)
    {

        id = slot;
        hp = 4;
        currentHp = 4;
        movespeed = 1;
        criticalpercent = 1;
        criticaldamage = 1;
        damage = 1;
        attackspeed = 1;
        defence = 1;
        cylinderCounter = 0;
        keyCounter = 0;
        cylinderPercent = 0;
        stamina = 40;
        counter_judgement = 1;
        p_name = "";
        tutorial = false;
        presetID = slot;
        bossClear = false;
        skillIdx = new int[3] { 1, 2, 3 };
        SaveSceneName = "MAP001";
        SavePortalName = "MAP001";
        Talk_Box = new bool[28];
        stageData = new List<StageData>();

        return this;
    }
    //플레이어에 있는것들을 플레이어 데이터에 넣는다
    public void CopyPlayer(Player player)
    {

        id = player.id;
        hp = player.hp;
        currentHp = player.currentHp;
        movespeed = player.movespeed;
        criticalpercent = player.criticalpercent;
        criticaldamage = player.criticaldamage;
        damage = player.damage;
        attackspeed = player.attackspeed;
        defence = player.defence;
        cylinderCounter = player.cylinderCounter;
        keyCounter = player.keyCounter;
        cylinderPercent = player.cylinderPercent;
        stamina = player.stamina;
        counter_judgement = player.counter_judgement;
        p_name = player.p_name;
        tutorial = player.tutorial;
        presetID = player.presetID;
        bossClear = player.bossClear;
        skillIdx = player.skillIdx;
        SaveSceneName = player.SaveSceneName;
        SavePortalName = player.SavePortalName;
        Talk_Box = player.Talk_Box;
        stageData = player.stageData;

    }
}

[Serializable]
public class Player
{
    //저장할 데이터 변수들
    public int id;                              //아이디 값
    public int hp;                              //채력
    public float movespeed;                     //움직임 속도 
    public float criticalpercent;               //크리티컬 확률
    public float criticaldamage;                //크리티컬 대미지
    public int damage;                          //공격력
    public float attackspeed;                   //공격속도
    public float defence;                       //방어
    public int cylinderCounter;                 //실린더 개수
    public int keyCounter;                      //키 개수
    public int cylinderPercent;                 //실린더 게이지 저장
    public int stamina;                         //스태미너
    public float counter_judgement;             //카운터 판결
    public string p_name;                       //이름
    public bool tutorial;                       //튜토리얼 대사 출력 여부
    public int presetID;                        //뭔지 모르겠음
    public bool bossClear;                      //중간보스 클리어여부
    public int[] skillIdx;                      //현재 선택한 스킬
    public string SaveSceneName;                //체크포인트 씬
    public string SavePortalName;               //저장한 씬으로 갈 포탈이름
    public bool[] Talk_Box;                     //대화 시스템 관리
    public List<StageData> stageData;           //스테이지 데이터
    public float currentHp;                     //현재 체력

    //저장 안할 변수들
    public Transform position;              //위치       
    public State.PlayerState m_state;       //상태
    public float counterTime;               //카운터 판정 타임
    public float gravity;
    public bool isDashPossible;
    public PlayerSkill skill;
    public CharacterController controller;
    public Animator animator;
    public AnimatorOverrideController overrideController;
    public List<AnimationClip> orgList = new List<AnimationClip>();
    public List<AnimationClip> aniList = new List<AnimationClip>();
    public List<KeyValuePair<AnimationClip, AnimationClip>> applyList = new List<KeyValuePair<AnimationClip, AnimationClip>>();
    public bool isSceneMove = false;
    public string curSceneName;
    public Dictionary<string, bool> D_stageData = new Dictionary<string, bool>();

    public void SetGravity(float gr)
    {
        gravity = gr;
    }

    public IEnumerator PlayerMovePosition(Vector3 pos)
    {
        if (animator.enabled == false)
            animator.enabled = true;
        controller.enabled = false;
        position.position = pos;
        isSceneMove = true;
        yield return new WaitForSeconds(0.2f);
        controller.enabled = true;
        gravity = 1.5f;
    }
}

[Serializable]
public class StageData
{
    public string key;
    public bool value;
    
    public StageData(string st, bool b)
    {
        key = st;
        value = b;
    }
}