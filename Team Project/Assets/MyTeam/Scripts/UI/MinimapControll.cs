using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapControll : MonoBehaviour
{
    private bool miniMapOnOff = false;

    public void ExitFromMiniMap()
    {
        BetweenPlayerAndShop.Instance.OnEventMinimapOnOff(miniMapOnOff);
    }
}
