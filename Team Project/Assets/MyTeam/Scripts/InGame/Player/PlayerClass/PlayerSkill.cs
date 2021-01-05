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
    }

    public void SkillEffectActive(AnimationClip clip)
    {
        for(int i = 0; i < skills.Length; ++i)
        {
            if(skills[i].Key.name == clip.name)
            {
                skills[i].Value.SetActive(true);
            }
        }
    }
}
