using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Monster : character
{
    public int skillid;                 //스킬 아이디
    public float counter_reslstance;    //카운터 저항
    public float user_aware_distance;   //유저 인식 거리

    public State.MonsterState monsterState;
    public NavMeshAgent navigation;

    public Animator animator;


    //public Monster(Transform t)                 //생성자
    //{
    //    position = t;

    //}

}
