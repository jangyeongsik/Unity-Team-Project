using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : character
{
    public Animator animator;
    public NavMeshAgent navigation;

    public State.NpcState npcState;
}
