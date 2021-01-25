using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    void MoveSound()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_PlayerMove, "Chapter Move1");
    }

    void PlayerGameOver()
    {
        GameEventToUI.Instance.OnGameOver();
    }
}
