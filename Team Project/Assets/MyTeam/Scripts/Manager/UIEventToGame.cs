using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIEventToGame : Singleton<UIEventToGame>
{
    
    #region"플레이어 조이스틱"
    public event System.Action<Vector2, float> PlayerMove;
    public event System.Action<bool> PlayerDash;

    public void OnPlayerMove(Vector2 direction , float amount)
    {
        if (PlayerMove != null)
            PlayerMove(direction, amount);
    }

    public void OnPlayerDash(bool isDash)
    {
        if (PlayerDash != null)
            PlayerDash(isDash);
    }
    #endregion
}
