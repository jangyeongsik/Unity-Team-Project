using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenPlayerAndShop : SingletonMonobehaviour<BetweenPlayerAndShop>
{
    public event System.Action<bool> onOff;
    public event System.Action<bool> miniOnOff;
    public event System.Action<bool, int, string> talk;

    public void OnEventShopOnOff(bool isOn)
    {
         onOff(isOn);
    }

    public void OnEventMinimapOnOff(bool isOn)
    {
        miniOnOff(isOn);
    }

    public void OnEventTalkOnOff(bool isOn, int id, string npcName)
    {
        talk(isOn, id, npcName);
    }

   
}
