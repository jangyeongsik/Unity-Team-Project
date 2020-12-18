using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashBtn : MonoBehaviour
{
   public void OnDash()
    {
        UIEventToGame.Instance.OnPlayerDash(true);
    }
}
