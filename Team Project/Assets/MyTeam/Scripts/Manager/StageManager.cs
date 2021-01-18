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
    public bool isClear = false;
    public bool isRegen = true;

    static Dictionary<string, bool> NoneRegenMap = new Dictionary<string, bool>();

    Monster[] enemys;

    TeleportMaster[] portals;

    private void Start()
    {
        if (enemys == null)
            enemys = GameObject.FindObjectsOfType<Monster>();
        if (portals == null)
            portals = GameObject.FindObjectsOfType<TeleportMaster>();

        if (NoneRegenMap.TryGetValue(GameData.Instance.player.curSceneName, out bool value) && isRegen == false)
        {
            if (value)
            {
                for (int i = 0; i < enemys.Length; ++i)
                {
                    enemys[i].gameObject.SetActive(false);
                }
            }
        }

        AudioClip clip;
        SoundManager.Instance.D_BGMS.TryGetValue(stageKind.ToString(),out clip);
        Debug.Log(clip.name);
        if(SoundManager.Instance.BGM_Audio.clip != clip)
        {
            SoundManager.Instance.BGM_Audio.clip = clip;
            SoundManager.Instance.BGM_Audio.Play();
        }
    }

    private void Update()
    {
        if (isClear == false)
        {
            if (GameObject.FindObjectOfType<Monster>() == null)
            {
                isClear = true;
                if (!isRegen)
                {
                    NoneRegenMap.Add(GameData.Instance.player.curSceneName, !isRegen);
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