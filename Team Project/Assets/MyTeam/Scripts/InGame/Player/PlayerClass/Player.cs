using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : character
{
    public float stamina;               //스태미너
    public float counter_judgement;     //카운터 판결
    public string p_name;               //이름
    public State.PlayerState m_state;  //상태


    public Player() { }
    Player(Transform t)                 //생성자
    {
        position = t;
    }

    

}
