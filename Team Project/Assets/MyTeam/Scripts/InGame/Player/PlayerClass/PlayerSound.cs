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
        GameData.Instance.player.currentHp = 1;
        GameEventToUI.Instance.OnPlayerHp_Increase(GameData.Instance.player.hp*2,100);
        //상태 초기화
        GetComponent<Animator>().Play("Idle");
        GameData.Instance.PlayerSave();
        SceneMgr.Instance.LoadScene(GameData.Instance.player.SaveSceneName, GameData.Instance.player.SavePortalName);
    }
}
