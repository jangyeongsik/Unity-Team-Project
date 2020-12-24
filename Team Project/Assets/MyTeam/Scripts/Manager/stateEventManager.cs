using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateEventManager : Singleton<stateEventManager>
{
    public delegate bool AttackEnvent();
    public AttackEnvent Player_Attack;

    public delegate bool Attack_success();
    public Attack_success Attack_SuccessEvent;

    public bool OnPlayer_AttackEvent()
    {
        return Player_Attack.Invoke();
    }

    public bool OnAttack_SuccessEvent()
    {
        return Attack_SuccessEvent.Invoke();
    }




}
