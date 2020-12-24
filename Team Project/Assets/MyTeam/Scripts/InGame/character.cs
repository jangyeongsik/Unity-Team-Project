using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public int        id;                        //아이디 값
    public int        hp;                        //채력
    public float      movespeed;                 //움직임 속도 
    public float      criticalpercent;           //크리티컬 확률
    public float      criticaldamage;            //크리티컬 대미지
    public float      damage;                    //공격력
    public float      attackspeed;               //공격속도
    public float      defence;                   //방어

    public Transform  position;                  //위치       

}

namespace State
{
    public enum PlayerState : int
    {
        P_Idle, P_Run, P_Dash, P_Guard, P_1st_Skill, P_2nd_Skill, P_3rd_Skill, P_Delay
    }

    public enum MonsterState
    {
        M_None,
        M_Idle,
        M_Move,
        M_Dead

    }

}
