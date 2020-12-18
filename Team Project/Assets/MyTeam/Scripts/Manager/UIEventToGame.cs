using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIEventToGame : SingletonMonobehaviour<UIEventToGame>
{
    /*
         ui에서 실행시킬 함수들을 넣어주세요 인게임에서 실행시키는건 GameEventToUI로....
         각 구분은 region으로 하는게 좋을거같습니다
      */

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
