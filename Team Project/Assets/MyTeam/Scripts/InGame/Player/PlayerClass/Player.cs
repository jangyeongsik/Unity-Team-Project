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
    public float currentHp;                     //현재 체력
    public bool[] skillShop;                    //스킬 구매 여부
    public bool[] tpActivate;                   //선택형 텔레포터 활성화

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
        player.skillShop = skillShop;
        player.D_stageData.Clear();
        for(int i = 0; i < stageData.Count; ++i)
        {
            if(!player.D_stageData.ContainsKey(stageData[i].key))
            {
                player.D_stageData.Add(stageData[i].key, stageData[i].value);
            }
        }
        player.tpActivate = tpActivate;

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
        SavePortalName = "STPPoint001";
        Talk_Box = new bool[28];
        stageData = new List<StageData>();
        skillShop = new bool[8];
        for(int i = 0; i < skillShop.Length; ++i)
        {
            if (i <= 2)
                skillShop[i] = true;
            else
                skillShop[i] = false;
        }
        tpActivate = new bool[3];

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
        for (int i = 0; i < skillIdx.Length; ++i)
            skillIdx[i] = i + 1;
        SaveSceneName = "MAP001";
        SavePortalName = "STPPoint001";
        for (int i = 0; i < Talk_Box.Length; ++i)
            Talk_Box[i] = false;
        stageData.Clear();
        for (int i = 0; i < skillShop.Length; ++i)
        {
            if (i <= 2)
                skillShop[i] = true;
            else
                skillShop[i] = false;
        }
        for (int i = 0; i < 3; i++)
        {
            tpActivate[i] = false;
        }
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
        skillShop = player.skillShop;
        tpActivate = player.tpActivate;
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
    public bool[] skillShop;                    //스킬 구매 여부
    public bool[] tpActivate;                   //선택형 텔레포터 활성화

    //저장 안할 변수들
    public Transform position;              //위치       
    public State.PlayerState m_state;       //상태
    public float counterTime;
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
    public List<GameObject> enemyData = new List<GameObject>();

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
        if(!(curSceneName == "MAP000"))
            isSceneMove = true;

        Debug.Log(pos);
        yield return new WaitForSeconds(0.2f);
        controller.enabled = true;
        gravity = 10f;
    }

    public void SetAniList()
    {
        for(int i = 0; i < skillIdx.Length; ++i)
        {
            aniList[i] = skill.skillClips[skillIdx[i]-1];
        }

        applyList.Clear();
        for (int i = 0; i < orgList.Count; ++i)
        {
            applyList.Add(new KeyValuePair<AnimationClip, AnimationClip>(orgList[i], aniList[i]));
        }
        overrideController.ApplyOverrides(applyList);
        animator.runtimeAnimatorController = overrideController;
    }

    public void DeletePlayer()
    {
        m_state = State.PlayerState.P_Idle;
        gravity = 0f;
        counterTime = 0f;
        isDashPossible = true;
        for (int i = 0; i < orgList.Count; ++i)
            aniList[i] = orgList[i];
        applyList.Clear();
        isSceneMove = false;
        curSceneName = "";
        D_stageData.Clear();
        enemyData.Clear();
    }

    public void PopEnemyData(GameObject obj)
    {
        enemyData.Remove(obj);
    }

    public void PlayerGameOver()
    {
        //플레이어 체력회복
        currentHp = 1;
        GameEventToUI.Instance.OnPlayerHp_Increase(GameData.Instance.player.hp * 2, 100);
        //상태 초기화
        animator.Play("Idle");
        GameData.Instance.PlayerSave();
        SceneMgr.Instance.LoadScene(GameData.Instance.player.SaveSceneName, GameData.Instance.player.SavePortalName);
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