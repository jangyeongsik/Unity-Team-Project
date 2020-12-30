using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingMenu : MonoBehaviour
{
    public void CloseMiniMap()
    {
        GameEventToUI.Instance.OnEventMinimapOnOff(false);
    }

    public void CloseBadge()
    {
        GameEventToUI.Instance.OnBadgeOnOff(false);
    }
}
