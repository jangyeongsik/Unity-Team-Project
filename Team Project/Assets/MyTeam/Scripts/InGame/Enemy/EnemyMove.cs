using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public LayerMask targetlayer;

    public  GameObject target;
    private NavMeshAgent navigation;

    private Monster enemy;

    private bool hasTarget
    {
        get
        {
            if(target != null)
            {
                return true;
            }
            return false;
        }
    }

    private void  monsterSetting()
    {
        navigation = GetComponent<NavMeshAgent>();
        enemy = new Monster(gameObject.transform);
        enemy.hp = 10;
        enemy.damage = 10.0f;
        enemy.movespeed = 4.0f;
        enemy.m_state = State.MonsterState.M_Idle;
        navigation.speed = enemy.movespeed;

    }
    private void Awake()
    {
        monsterSetting();
    }
    private void Start()
    {
        StartCoroutine(UpdatePath());

    }

    private IEnumerator UpdatePath()
    {
        
        while (enemy.m_state != State.MonsterState.M_Dead) {
            if (target)
            {
                navigation.isStopped = false;
                navigation.SetDestination(target.transform.position);

            }
            else
            {
                navigation.isStopped = true;
               
            }
        }
        yield return new WaitForSeconds(0.25f);
    }
}
