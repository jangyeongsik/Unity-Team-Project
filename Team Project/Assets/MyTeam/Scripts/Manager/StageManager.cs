using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum STAGEKIND
{
    Temple,Safe_Zone,Chapter1_Normal,Boss
}

public class StageManager : MonoBehaviour
{
    public STAGEKIND stageKind;
    public bool isClear = true;
    public bool isRegen = true;
    private bool isAble = false;

    Monster[] enemys;

    TeleportMaster[] portals;

    private void Awake()
    {
        if (enemys == null)
            enemys = GameObject.FindObjectsOfType<Monster>();

        if (portals == null)
            portals = GameObject.FindObjectsOfType<TeleportMaster>();

        //리젠 불가능한 스테이지 일때 플레이어 데이터에 스테이지정보가 없으면 추가한다
        if (!isRegen)
        {
            bool val = true;
            for (int i = 0; i < GameData.Instance.player.stageData.Count; ++i)
            {
                if (GameData.Instance.player.stageData[i].key.Equals(GameData.Instance.player.curSceneName))
                {
                    val = false;
                    break;
                }
            }
            if (val)
            {
                StageData st = new StageData(GameData.Instance.player.curSceneName, false);
                GameData.Instance.player.stageData.Add(st);
                GameData.Instance.player.D_stageData.Add(st.key, st.value);
            }
        }

        //이전에 클리어한 데이터가 있다면 바로 에너미를 꺼버린다
        if (GameData.Instance.player.D_stageData.TryGetValue(GameData.Instance.player.curSceneName, out bool value) && !isRegen)
        {
            if (value)
            {
                for (int i = 0; i < enemys.Length; ++i)
                    enemys[i].gameObject.SetActive(false);
                isClear = true;
                isAble = true;
            }
        }

        GameData.Instance.player.enemyData.Clear();
        for (int i = 0; i < enemys.Length; ++i)
        {
            if (enemys[i].gameObject.activeSelf)
                GameData.Instance.player.enemyData.Add(enemys[i].gameObject);
        }
    }

    private void Start()
    {
        AudioClip clip;
        SoundManager.Instance.D_BGMS.TryGetValue(stageKind.ToString(), out clip);
        if (SoundManager.Instance.BGM_Audio.clip != clip)
        {
            SoundManager.Instance.BGM_Audio.clip = clip;
            SoundManager.Instance.BGM_Audio.Play();
        }

        if (stageKind == STAGEKIND.Temple || (stageKind == STAGEKIND.Chapter1_Normal && !isRegen))
        {
            isClear = false;
        }
        else if (stageKind == STAGEKIND.Boss)
        {

        }
        else
        {
            isClear = true;
        }

    }
    private void Update()
    {
        //맵 클리어 이전일때
        if (isClear == false)
        {
            //맵에 몬스터가 남아있지않다면
            if (GameData.Instance.player.enemyData.Count == 0)
            {
                isClear = true;
                //백신 클리어하면 아이템추가
                if (!isAble)
                {
                    if (stageKind == STAGEKIND.Chapter1_Normal && !isRegen)
                    {
                        string str = GameData.Instance.player.curSceneName;
                        string id = str.Substring(3);
                        GameEventToUI.Instance.OnItemDropInfo(true, int.Parse(id));
                    }
                }
                if (stageKind == STAGEKIND.Temple)
                {
                    string str = GameData.Instance.player.curSceneName;
                    string id = str.Substring(3);
                    GameEventToUI.Instance.OnTempleItemDropInfo(true, int.Parse(id));
                }
                //리젠을 하지않는 맵이라면
                if (!isRegen)
                {
                    //저장하기위한 값변경
                    for (int i = 0; i < GameData.Instance.player.stageData.Count; ++i)
                    {
                        if (GameData.Instance.player.stageData[i].key.Equals(GameData.Instance.player.curSceneName))
                        {
                            GameData.Instance.player.stageData[i].value = true;
                        }
                    }
                    //인게임중 확인할 값변경
                    GameData.Instance.player.D_stageData[GameData.Instance.player.curSceneName] = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < portals.Length; ++i)
            {
                portals[i].isPossibleMove = true;
                if (portals[i].Active != null)
                    portals[i].mesh.mesh = portals[i].Active;
            }
        }
    } 
}