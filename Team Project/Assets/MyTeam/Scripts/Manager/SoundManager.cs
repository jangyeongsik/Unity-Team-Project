using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public enum SoundKind
{
    Sound_PlayerMove,
    Sound_PlayerSkill,
    Sound_Bat,
    Sound_Bone,
    Sound_Chapter1_Boss,
    Sound_Chapter2_Boss,
    Sound_Priest,
    Sound_Viper,
    Sound_WolfSound,
    Sound_UISound
}

public class SoundManager : SingletonMonobehaviour<SoundManager>
{
    [HideInInspector]
    public AudioSource Effect_Audio;
    [HideInInspector]
    public AudioSource BGM_Audio;


    #region"사운드 배열들"
    [Header("플레이어 사운드")]
    public Sound[] PlayerMoveSound;
    public Sound[] PlayerSkillSound;

    [Header("에너미 사운드")]
    public Sound[] BatSound;
    public Sound[] BoneSound;
    public Sound[] Chapter1_BossSound;
    public Sound[] Chapter2_BossSound;
    public Sound[] PriestsSound;
    public Sound[] ViperSound;
    public Sound[] WolfSound;

    [Header("BGM")]
    public Sound[] BGMS;

    [Header("제작 사운드")]
    public Sound[] ForgeSounds;

    [Header("UI 사운드")]
    public Sound[] UISounds;
    #endregion

    #region"사운드 딕셔너리들"
    public Dictionary<string, AudioClip> D_PlayerMoveSound = new Dictionary<string,AudioClip>();
    public Dictionary<string, AudioClip> D_PlayerSkillSound = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_BatSound = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_BoneSound = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_Chapter1_BossSound = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_Chapter2_BossSound = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_PriestsSound = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_ViperSound = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_WolfSound = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_BGMS = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_ForgeSounds = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> D_UISounds = new Dictionary<string, AudioClip>();
    #endregion

    private void Start()
    {
        Effect_Audio = GetComponent<AudioSource>();
        BGM_Audio = GetComponents<AudioSource>()[1];
        //배열 딕셔너리로 옮기기
        SetDictionary();

        AudioClip c;
        D_BGMS.TryGetValue("Temple",out c);
        BGM_Audio.clip = c;
        BGM_Audio.loop = true;
        BGM_Audio.Play();
    }

    //타입과 이름을 적으세용 이름은 인스펙터에서 확인가능합니당
    public void OnPlayOneShot(SoundKind kind,string name)
    {
        AudioClip audioClip = null;
        switch (kind)
        {
            case SoundKind.Sound_PlayerMove:
                D_PlayerMoveSound.TryGetValue(name, out audioClip);
                break;
            case SoundKind.Sound_PlayerSkill:
                D_PlayerSkillSound.TryGetValue(name, out audioClip);
                break;
            case SoundKind.Sound_Bat:
                D_BatSound.TryGetValue(name, out audioClip);
                break;
            case SoundKind.Sound_Bone:
                D_BoneSound.TryGetValue(name, out audioClip);
                break;
            case SoundKind.Sound_Chapter1_Boss:
                D_Chapter1_BossSound.TryGetValue(name, out audioClip);
                break;
            case SoundKind.Sound_Chapter2_Boss:
                D_Chapter2_BossSound.TryGetValue(name, out audioClip);
                break;
            case SoundKind.Sound_Priest:
                D_PriestsSound.TryGetValue(name, out audioClip);
                break;
            case SoundKind.Sound_Viper:
                D_ViperSound.TryGetValue(name, out audioClip);
                break;
            case SoundKind.Sound_WolfSound:
                D_WolfSound.TryGetValue(name, out audioClip);
                break;
            case SoundKind.Sound_UISound:
                D_UISounds.TryGetValue(name, out audioClip);
                break;
        }
        if (audioClip != null)
            Effect_Audio.PlayOneShot(audioClip);
    }

    //배열데이터 딕셔너리로 옮겨주자
    void SetDictionary()
    {
        for (int i = 0; i < PlayerMoveSound.Length; ++i)
        {
            D_PlayerMoveSound.Add(PlayerMoveSound[i].name, PlayerMoveSound[i].clip);
        }

        for (int i = 0; i < PlayerSkillSound.Length; ++i)
        {
            D_PlayerSkillSound.Add(PlayerSkillSound[i].name, PlayerSkillSound[i].clip);
        }

        for (int i = 0; i < BatSound.Length; ++i)
        {
            D_BatSound.Add(BatSound[i].name, BatSound[i].clip);
        }

        for (int i = 0; i < BoneSound.Length; ++i)
        {
            D_BoneSound.Add(BoneSound[i].name, BoneSound[i].clip);
        }

        for (int i = 0; i < Chapter1_BossSound.Length; ++i)
        {
            D_Chapter1_BossSound.Add(Chapter1_BossSound[i].name, Chapter1_BossSound[i].clip);
        }

        for (int i = 0; i < Chapter2_BossSound.Length; ++i)
        {
            D_Chapter2_BossSound.Add(Chapter2_BossSound[i].name, Chapter2_BossSound[i].clip);
        }

        for (int i = 0; i < PriestsSound.Length; ++i)
        {
            D_PriestsSound.Add(PriestsSound[i].name, PriestsSound[i].clip);
        }

        for (int i = 0; i < ViperSound.Length; ++i)
        {
            D_ViperSound.Add(ViperSound[i].name, ViperSound[i].clip);
        }

        for (int i = 0; i < WolfSound.Length; ++i)
        {
            D_WolfSound.Add(WolfSound[i].name, WolfSound[i].clip);
        }

        for (int i = 0; i < WolfSound.Length; ++i)
        {
            D_BGMS.Add(BGMS[i].name, BGMS[i].clip);
        }

        for (int i = 0; i < ForgeSounds.Length; i++)
        {
            D_ForgeSounds.Add(ForgeSounds[i].name, ForgeSounds[i].clip);
        }
        for (int i = 0; i < UISounds.Length; i++)
        {
            D_UISounds.Add(UISounds[i].name, UISounds[i].clip);
        }
    }
    public void PlayForgeSound(string name)
    {
        Effect_Audio.PlayOneShot(D_ForgeSounds[name]);
    }
}
