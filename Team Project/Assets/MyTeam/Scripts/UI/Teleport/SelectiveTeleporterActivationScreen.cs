using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectiveTeleporterActivationScreen : MonoBehaviour
{
    public GameObject TpScreen;
    public int TPPointIdx;
    [SerializeField]
    private int currentActivatedTpPointsCount;
    public TMP_Text cylinderRequired;
    private int requiredCount;
    
    private void OnEnable()
    {
        currentActivatedTpPointsCount = 0;
        for (int i = 0; i < 3; i++)
        {
            if (GameData.Instance.player.tpActivate[i])
            {
                currentActivatedTpPointsCount++;
            }
        }
        switch (currentActivatedTpPointsCount)
        {
            case 0:
                cylinderRequired.text = "0";
                requiredCount = 0;
                break;
            case 1:
                cylinderRequired.text = "20";
                requiredCount = 20;
                break;
            case 2:
                cylinderRequired.text = "40";
                requiredCount = 40;
                break;
        }
    }
    public void ActivateTpPoint()
    {
        if (GameData.Instance.player.cylinderCounter >= requiredCount)
        {
            //플레이어 실린더 개수 줄이기, 저장
            GameData.Instance.player.tpActivate[TPPointIdx] = true;
            GameData.Instance.player.cylinderCounter -= requiredCount;
            GameData.Instance.PlayerSave();

            //팝업 끄기, TPScreen키기
            gameObject.SetActive(false);
            TpScreen.SetActive(true);

            //TPScreen에 idx 넘겨주기
            TpScreen.GetComponent<SelectiveTeleporterTpScreen>().curTPPointIdx = TPPointIdx;
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
