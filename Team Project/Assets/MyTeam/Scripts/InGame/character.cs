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
    public enum PlayerState
    {
        None

    }

    public enum MonsterState
    {
        None

    }

}
