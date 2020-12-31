using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    bool Attack_Success = false; 

    private void Start()
    {
        UIEventToGame.Instance.playerAttack += playerAttack;
        stateEventManager.Instance.Attack_SuccessEvent += Attack_SuccessEvent;

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
            case State.PlayerState.P_Idle:
            case State.PlayerState.P_Run:
                if (stateEventManager.Instance.OnPlayer_AttackEvent())
                    animator.Play("First_Skill");
                else
                animator.SetTrigger("Guard");
                break;
            case State.PlayerState.P_Dash:
                break;
            case State.PlayerState.P_Guard:
                break;
            case State.PlayerState.P_1st_Skill:
                break;
            case State.PlayerState.P_2nd_Skill:
                break;
            case State.PlayerState.P_3rd_Skill:
                break;
            case State.PlayerState.P_Delay:
                break;
            default:
                break;
        }
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
                
                break;
        }
    }

    public bool Attack_SuccessEvent()
    {
        return Attack_Success;
    }

}