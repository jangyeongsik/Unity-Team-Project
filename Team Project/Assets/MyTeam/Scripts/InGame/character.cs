using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class character : MonoBehaviour
{
    public int        id;                        //아이디 값
    public int        hp;                        //체력
    public float      movespeed;                 //움직임 속도 
    public float      criticalpercent;           //크리티컬 확률
    public float      criticaldamage;            //크리티컬 대미지
    public int        damage = 1;                //공격력

    public float      attackspeed;               //공격속도
    public float      defence;                   //방어

    public Transform  position;                  //위치       

}

namespace State
{
    public enum PlayerState : int
    {
        P_Idle, P_Run, P_Dash, P_Guard, P_1st_Skill, P_2nd_Skill, P_3rd_Skill, P_Delay, P_Hit, P_Dead
    }

    public enum MonsterState
    {
        M_None,
        M_Idle,
        M_Move,
        M_Dead,
        M_Groar,
        M_Attack,
        M_Return,
        M_Damage,
        M_Dash
    }

    public enum NpcState
    {
        N_Idle
    }

    public enum MonsterKind
    {
        M_Warrier,M_Archer,M_Viper,M_Wolf,M_SkullKing,M_Vishop,M_Bat,M_Boss

    }

    public enum BossState
    {
        B_Idle, B_Move, B_Attack, B_SkillChargeOne, B_SkillChargeTwo, B_SkillChargeThree, B_SkillOne, B_SkillTwo, B_SkillThree, B_Hit, B_Dead, B_AttackTwo
    }


}
