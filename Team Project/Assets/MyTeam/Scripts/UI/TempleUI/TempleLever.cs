using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleLever : MonoBehaviour
{
    public string leverName;
    public GameObject templePortal;
    public bool isActivated;
    public bool isClear;
    public StageManager sM;

    private void Start()
    {
        UIEventToGame.Instance.ActivateTemplePortal += ActivateTemplePortal;
        UIEventToGame.Instance.ActivateTemple += ActivateTemple;
        isActivated = false;
        isClear = sM.isClear;
    }
    private void Update()
    {
        isClear = sM.isClear;
    }

    private void OnDestroy()
    {
        UIEventToGame.Instance.ActivateTemplePortal -= ActivateTemplePortal;
        UIEventToGame.Instance.ActivateTemple -= ActivateTemple;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isClear && !templePortal.activeSelf)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameEventToUI.Instance.OnTemplePopup(true, isActivated, leverName);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isClear)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameEventToUI.Instance.OnTemplePopup(false, isActivated, leverName);
            }
        }
    }
    private void ActivateTemplePortal(bool isOn)
    {
        templePortal.SetActive(isOn);
    }
    private void ActivateTemple(bool _isActivated)
    {
        isActivated = _isActivated;
    }
}
