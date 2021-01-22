using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Slider effectSetting;
    public Slider bgmSetting;

    private void Start()
    {
        effectSetting.onValueChanged.AddListener(delegate { SetEffectVolume(); });
        bgmSetting.onValueChanged.AddListener(delegate { SetBGMVolume(); });
        effectSetting.value = SoundManager.Instance.Effect_Audio.volume;
        bgmSetting.value = SoundManager.Instance.BGM_Audio.volume;
    }

    void SetEffectVolume()
    {
        SoundManager.Instance.Effect_Audio.volume = effectSetting.value;
    }

    void SetBGMVolume()
    {
        SoundManager.Instance.BGM_Audio.volume = bgmSetting.value;
    }
}
