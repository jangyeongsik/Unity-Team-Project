using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossData : character
{
    public int skillid;                 //스킬 아이디
    public float counter_reslstance;    //카운터 저항
    public float attack_aware_distance;   //공격 거리

<<<<<<< HEAD
    public Transform target;
    public Transform pattonTarget;
=======
    public Transform target;            //공격 타겟
>>>>>>> 20d6c6daf325569702c665b85a32ac857afecd8b

    public State.BossState bossState;
    public State.BossState lastAttack;
    public int chargeNum;

    public NavMeshAgent navigation;

    public Animator animator;

    public void PlayerHit()
    {
        GameEventToUI.Instance.OnPlayerBossHit(position, damage, bossState);
    }
}
