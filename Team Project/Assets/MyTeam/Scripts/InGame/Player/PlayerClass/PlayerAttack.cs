﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    public AnimationClip clips;

    bool Attack_Success = false; 

    private void Start()
    {
        UIEventToGame.Instance.playerAttack += playerAttack;
        stateEventManager.Instance.Attack_SuccessEvent += Attack_SuccessEvent;

        //AnimatorOverrideController con = new AnimatorOverrideController(animator.runtimeAnimatorController);
        //var list = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        //foreach(AnimationClip a in con.animationClips)
        //{
        //    if(a.name.Contains("Attack1"))
        //    list.Add(new KeyValuePair<AnimationClip, AnimationClip>(a,clips));
        //    else
        //    list.Add(new KeyValuePair<AnimationClip, AnimationClip>(a,a));
        //}
        //con.ApplyOverrides(list);
        //animator.runtimeAnimatorController = con;


        //AnimatorOverrideController c = new AnimatorOverrideController(animator.runtimeAnimatorController);
        //foreach (AnimationClip a in c.animationClips)
        //{
        //    Debug.Log(a.name);
        //}

    }

    private void Update()
    {
        if (GameData.Instance.player.counterTime < 1.1f && stateEventManager.Instance.OnPlayer_AttackEvent() &&
           GameData.Instance.player.m_state == State.PlayerState.P_Guard)
        {
            animator.SetTrigger("NextSkill");
            GameEventToUI.Instance.OnSkillGaugeActive(true);
            Attack_Success = true;
        }

        if (GameData.Instance.player.m_state == State.PlayerState.P_Delay)
            Attack_Success = false;
    }

    private void OnDestroy()
    {
        UIEventToGame.Instance.playerAttack += playerAttack;
    }

    void playerAttack(float time, COLORZONE color)
    {
        switch (GameData.Instance.player.m_state)
        {
            case State.PlayerState.P_1st_Skill:
                switch (color)
                {
                    case COLORZONE.NONE:
                        GameEventToUI.Instance.OnSkillGaugeActive(false);
                        break;
                    case COLORZONE.GREEN:
                    case COLORZONE.YELLOW:
                    case COLORZONE.RED:
                        animator.SetTrigger("NextSkill");
                        GameEventToUI.Instance.OnSkillGaugeActive(false);
                        GameEventToUI.Instance.OnSkillGaugeActive(true);
                        break;
                }
                break;
            case State.PlayerState.P_2nd_Skill:
                switch (color)
                {
                    case COLORZONE.NONE:
                        GameEventToUI.Instance.OnSkillGaugeActive(false);
                        break;
                    case COLORZONE.GREEN:
                    case COLORZONE.YELLOW:
                    case COLORZONE.RED:
                        animator.SetTrigger("NextSkill");
                        GameEventToUI.Instance.OnSkillGaugeActive(false);
                        break;
                }
                break;
            case State.PlayerState.P_3rd_Skill:
                break;
            case State.PlayerState.P_Delay:
                break;
            default:
                animator.SetTrigger("Guard");
                break;
        }
    }

    public bool Attack_SuccessEvent()
    {
        return Attack_Success;
    }

}
