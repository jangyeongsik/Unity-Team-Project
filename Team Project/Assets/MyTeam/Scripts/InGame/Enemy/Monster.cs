using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : character
{
    public int skillid;                         //스킬 아이디
    public float counter_reslstance;            //카운터 저항
    public float attack_aware_distance;         //공격 거리
    public float target_notice_distance;        //타겟 감지 거리 

    public State.MonsterKind monsterKind;       //몬스터 종류
    public State.MonsterState monsterState;     //몬스터 상태

    private GameObject target;                  //타겟 (플레이어)

    public int dropItem_Key = 1;                //몬스터년이 드롭하는 아이템. (default 값 1로)
    public bool isGet;
    public bool monsterDead;
    public NavMeshAgent navigation;
    public Animator animator;

    public bool counterjudgement;

    public event System.Action EnemyHitEvent;

    public event System.Action EnemyDeadEvent;

    public Rigidbody rigid;


    private void Start()
    {
        target = GameData.Instance.player.position.gameObject;

        rigid = GetComponent<Rigidbody>();
    }

    public void OnEnemyHitEvent()
    {
        EnemyHitEvent?.Invoke();
    }
    public void OnEnemyDeadEvent()
    {
        EnemyDeadEvent?.Invoke();
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
        rigid.isKinematic = true;

        rigid.useGravity = true;

        gameObject.SetActive(false);

        GameEventToUI.Instance.OnEventDropItemMentBoxOnOff();

        GameEventToUI.Instance.OnEventMonsterDrop(dropItem_Key);

        GameEventToUI.Instance.OnPlayerHp_Increase(1, 15);
    }
}
