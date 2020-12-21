using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventToUI : SingletonMonobehaviour<GameEventToUI>
{
    public event System.Action<Vector2> FollowPlayerUI;

    public void OnFollowPlayerUI(Vector2 playerPos)
    {
        if (FollowPlayerUI != null)
            FollowPlayerUI(playerPos);
    }
}
