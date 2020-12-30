using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningMenu : MonoBehaviour
{
    public void OpenMiniMap()
    {
        GameEventToUI.Instance.OnEventMinimapOnOff(true);
    }

    public void OpenBadge()
    {
        GameEventToUI.Instance.OnBadgeOnOff(true);
    }
}
