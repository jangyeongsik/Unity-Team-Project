using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : character
{
    public int skillid;                     //스킬 아이디
    public float counter_reslstance;        //카운터 저항
    public float attack_aware_distance;     //공격 거리
    public float target_notice_distance;    //타겟 감지 거리 

    public State.MonsterKind monsterKind;   //몬스터 종류
    public State.MonsterState monsterState;


    private GameObject target;              //타겟 (플레이어)


    public NavMeshAgent navigation;
    public Animator animator;

    public bool counterjudgement;

    public event System.Action EnemyHitEvent;


    private void Start()
    {
        target = GameData.Instance.player.position.gameObject;
    }
    public void OnEnemyHitEvent()
    {
        EnemyHitEvent?.Invoke();
    }

    public void OnPlayerHit()
    {
        if(monsterState == State.MonsterState.M_Attack)
            GameEventToUI.Instance.OnPlayerHit(transform, damage);
    }

    public float DistacneWithTarget()
    {
        float distance = (transform.position - target.transform.position).magnitude;
        return distance;
    }

    public void SetGameObjectFale()
    {
        gameObject.SetActive(false);
    }

}
