using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    Animator animator;
    public KeyValuePair<AnimationClip, GameObject>[] skills;

    public AnimationClip[] skillClips;
    public GameObject[] skillEffects;

    private void Start()
    {
        skills = new KeyValuePair<AnimationClip, GameObject>[skillClips.Length];
        for(int i = 0; i < skillClips.Length; ++i)
        {
            skills[i] = new KeyValuePair<AnimationClip, GameObject>(skillClips[i], skillEffects[i]);
        }

        animator = transform.GetChild(0).GetComponent<Animator>();

        GameData.Instance.player.skill = this;

        if (GameData.Instance.player.overrideController == null)
            GameData.Instance.player.overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        AnimationClip[] clips = GameData.Instance.player.overrideController.animationClips;
        GameData.Instance.player.orgList.Clear();
        GameData.Instance.player.aniList.Clear();
        for (int i = 0; i < clips.Length; ++i)
        {
            if (clips[i].name.Contains("Skill"))
            {
                GameData.Instance.player.orgList.Add(clips[i]);
                GameData.Instance.player.aniList.Add(clips[i]);
            }
        }
    }

    public void SkillEffectActive(AnimationClip clip)
    {
        for(int i = 0; i < skills.Length; ++i)
        {
            if(skills[i].Key.name == clip.name)
            {
                if (skills[i].Value.activeSelf == true)
                    skills[i].Value.SetActive(false);
                skills[i].Value.SetActive(true);
            }
        }
    }
}
