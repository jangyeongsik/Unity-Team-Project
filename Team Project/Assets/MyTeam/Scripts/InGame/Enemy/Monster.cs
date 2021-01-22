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

    public int dropItem_Key = 1;                //몬스터가 드롭하는 아이템. (default 값 1로)
    public bool isGet;
    public bool monsterDead;
    public NavMeshAgent navigation;
    public Animator animator;
    public Rigidbody rigid;

    public bool counterjudgement;

    public event System.Action<int> EnemyHitEvent;

    public event System.Action EnemyDeadEvent;

    private void Start()
    {
        target = GameData.Instance.player.position.gameObject;

        rigid = GetComponent<Rigidbody>();
    }

    public void OnEnemyHitEvent(int damage)
    {
        EnemyHitEvent?.Invoke(damage);
    }
    public void OnEnemyDeadEvent()
    {
        EnemyDeadEvent?.Invoke();
    }

    #region 몬스터들 공격(데미지 관련)함수.
    public void OnPlayerHit()
    {
         if (monsterState == State.MonsterState.M_Attack)
            GameEventToUI.Instance.OnPlayerHit(transform, damage);
    }
    #endregion

    #region 플레이어(타겟)과의 거리 구하는 함수. 
    public float DistacneWithTarget()
    {
        float distance = (transform.position - target.transform.position).magnitude;
        return distance;
    }
    #endregion

    #region 몬스터들 겹치는거 해소하는 함수.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            rigid.isKinematic = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        rigid.isKinematic = false;
    }
    #endregion

    #region 몬스터들 죽으면 사라지고 아이템(신전 키) 떨구는 함수.
    public void SetGameObjectFale()
    {
        rigid.isKinematic = true;

        gameObject.SetActive(false);

        GameEventToUI.Instance.OnEventDropItemMentBoxOnOff();

        GameEventToUI.Instance.OnEventMonsterDrop(dropItem_Key);

        GameEventToUI.Instance.OnPlayerHp_Increase(1, 30);

        //현재 스테이지 정보에 본인을 뺀다
        GameData.Instance.player.PopEnemyData(position.gameObject);
    }
    #endregion

    #region 카운터 공격 가능한 이벤트 호출용 함수.
    public void TurnOnCautionPoint()
    {
        counterjudgement = true;
    }

    public void TurnOffCautionPoint()
    {
        counterjudgement = false;
    }
    #endregion

    #region 에너미 피격모션 빠지나오는 이벤트 호출용 함수.
    public void ExitHit()
    {
        if (monsterState == State.MonsterState.M_Damage)
            monsterState = State.MonsterState.M_Idle;
    }
    #endregion

    #region 에너미 타겟(플레이어) 바라보게 함.
    public void PlayerLookAt()
    {
        if (gameObject.activeSelf == true)
            transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
    }
    #endregion

}
