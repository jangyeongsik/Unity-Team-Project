using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPController : MonoBehaviour
{
    public int destTPIdx;
    [SerializeField]
    Transform[] destPads;
    private void Awake()
    {
        destPads = gameObject.GetComponentsInChildren<Transform>();
        destTPIdx = TeleportController.destTPIdx;
    }
    void Start()
    {
        destTPIdx = TeleportController.destTPIdx;
        if (TeleportController.isTeleporting)
        {
            GameEventToUI.Instance.OnEventTPCanvasOnOff(false);
            Vector3 destination = destPads[destTPIdx].position + new Vector3(0, destPads[destTPIdx].GetComponent<CustomTeleporterPad>().TPHeightOffset, 0);
            Vector3 dir = destination - GameData.Instance.player.position.position;
            dir.y = 0;
            GameData.Instance.player.controller.Move(dir);
            TeleportController.isTeleporting = false;
        }
    }
}
