using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossTartos : MonoBehaviour
{
    public Transform target;

    BossData tartos;
    Monster monster;

    public GameObject tartosPatton1_1;
    public GameObject tartosPatton1_2;
    public GameObject tartosPatton1_3;
    public GameObject tartosPatton1_4;
    public GameObject tartosPatton1_5;

    public GameObject tartosPatton2_1;
    public GameObject tartosPatton2_2;
    public GameObject tartosPatton2_3;
    public GameObject tartosPatton2_4;
    public GameObject tartosPatton2_5;
    public GameObject tartosPatton2_6;
    public GameObject tartosPatton2_7;
    public GameObject tartosPatton2_8;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monster.position = transform;
        tartos = GetComponent<BossData>();
        tartos.navigation = GetComponent<NavMeshAgent>();
        tartos.animator = transform.GetChild(0).GetComponent<Animator>();
        tartos.position = transform;
    }


    
}

