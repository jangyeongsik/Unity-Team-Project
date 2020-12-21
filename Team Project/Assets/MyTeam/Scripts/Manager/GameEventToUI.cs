using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventToUI : Singleton<GameEventToUI>
{


    #region "카운터 게이지 플레이어 따라가기"
    public event System.Action<Vector2> FollowPlayerUI;

    public void OnFollowPlayerUI(Vector2 playerPos)
    {
        if (FollowPlayerUI != null)
            FollowPlayerUI(playerPos);
    }
    #endregion

}
