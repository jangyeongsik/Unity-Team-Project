using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTeleporterPad : MonoBehaviour
{
    #region "재현이형 코드"
    int padNum;
    bool isChecking = false;
    bool isCancelActivate = false;
    [SerializeField]
    int teleportationHeightOffset;
    public int TPHeightOffset
    {
        get { return teleportationHeightOffset; }
    }
    public bool isActivated = false;
    private void Awake()
    {
        teleportationHeightOffset = 0;
    }
    private void Start()
    {
        UIEventToGame.Instance.TPActivate += Activate;
        UIEventToGame.Instance.CancelActivate += CancelActivation;
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(OnOffPopupCoroutine(other, true));
    }
    //텔레포터를 밟고 있을 때 1초에 한번씩 검사
    private void OnTriggerStay(Collider other)
    {
        if (!isChecking && !isCancelActivate)
        {
            StartCoroutine(CheckCoroutine(other, true));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isCancelActivate = false;
        StartCoroutine(OnOffPopupCoroutine(other, false));
    }
    //isActivated 인지 체크, 맞는 팝업 띄우기
    IEnumerator CheckCoroutine(Collider other, bool isOn)
    {
        isChecking = true;
        if (other.CompareTag("Player"))
        {
            if (isActivated)
            {
                GameEventToUI.Instance.OnEventTPOpearteOnOff(!isOn);
                GameEventToUI.Instance.OnEventTPCanvasOnOff(isOn);
            }
            else
            {
                GameEventToUI.Instance.OnEventTPOpearteOnOff(isOn);
                GameEventToUI.Instance.OnEventTPCanvasOnOff(!isOn);
            }
        }
        yield return new WaitForSecondsRealtime(1.0f);
        isChecking = false;
    }
    IEnumerator OnOffPopupCoroutine(Collider other, bool isOn)
    {
        if (other.CompareTag("Player"))
        {
            if (isActivated)
            {
                GameEventToUI.Instance.OnEventTPCanvasOnOff(isOn);
            }
            else
            {
                GameEventToUI.Instance.OnEventTPOpearteOnOff(isOn);
            }
        }
        yield return new WaitForSecondsRealtime(1.0f);
    }
    void Activate(bool isOn)
    {
        isActivated = isOn;
    }
    void CancelActivation(bool isOn)
    {
        isCancelActivate = isOn;
    }
    private void OnDestroy()
    {
        UIEventToGame.Instance.TPActivate -= Activate;
    }
    #endregion

    public string ConnectPortalName;
}
