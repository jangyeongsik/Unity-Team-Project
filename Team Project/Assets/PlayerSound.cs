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
        //플레이어 체력회복
        GameEventToUI.Instance.playerHP_Increase(GameData.Instance.player.hp);
        //상태 초기화
        GetComponent<Animator>().Play("Idle");
        GameData.Instance.PlayerSave();
        SceneMgr.Instance.LoadScene(GameData.Instance.player.SaveSceneName, GameData.Instance.player.SavePortalName);
    }
}
