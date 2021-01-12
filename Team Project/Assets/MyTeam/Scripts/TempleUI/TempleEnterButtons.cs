using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleEnterButtons : MonoBehaviour
{
    public void Enter()
    {
        UIEventToGame.Instance.OnActivateTemplePortal(true);
        gameObject.SetActive(false);
    }
    public void Revoke()
    {
        gameObject.SetActive(false);
    }
}
