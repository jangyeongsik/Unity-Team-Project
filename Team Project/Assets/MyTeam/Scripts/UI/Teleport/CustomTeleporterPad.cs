using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTeleporterPad : MonoBehaviour
{
    int padNum;

    [SerializeField]
    int teleportationHeightOffset;
    public int TPHeightOffset
    {
        get { return teleportationHeightOffset; }
    }

    [SerializeField]
    bool isActivated = false;
    private void Awake()
    {
        teleportationHeightOffset = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isActivated)
            {
                GameEventToUI.Instance.OnEventTPCanvasOnOff(true);
            }
            else
            {
                GameEventToUI.Instance.OnEventTPOpearteOnOff(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isActivated)
            {
                GameEventToUI.Instance.OnEventTPCanvasOnOff(false);
            }
            else
            {
                GameEventToUI.Instance.OnEventTPOpearteOnOff(false);
            }
        }
    }
}
