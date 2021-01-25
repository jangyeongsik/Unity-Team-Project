﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class SelectiveTeleporterTpScreen : MonoBehaviour
{
    public int curTPPointIdx;
    public int destTPPointIdx;
    public bool[] tpPointsIsActivated;
    public Sprite[] tpPointSprites;
    public Image[] tpPointImages;
    public GameObject marker;
    StringBuilder sb;
    string DestMapName;

    private void Start()
    {
        sb = new StringBuilder();
        tpPointsIsActivated = new bool[3];
        //포인트 불러오기
        tpPointImages = transform.GetChild(3).GetComponentsInChildren<Image>();
        //플레이어 데이터에서 읽어와서 이미지 변경
        for (int i = 0; i < 3; i++)
        {
            tpPointsIsActivated[i] = GameData.Instance.player.tpActivate[i];
            if (GameData.Instance.player.tpActivate[i])
            {
                tpPointImages[i].sprite = tpPointSprites[1];
            }
            else
            {
                tpPointImages[i].sprite = tpPointSprites[0];
            }
        }
    }
    private void Update()
    {
        if (marker.transform.position != tpPointImages[destTPPointIdx].transform.position)
        {
            float x = 0f;
            float y = 0f;
            x = Mathf.Lerp(marker.transform.position.x, tpPointImages[destTPPointIdx].transform.position.x, 0.3f);
            y = Mathf.Lerp(marker.transform.position.y, tpPointImages[destTPPointIdx].transform.position.y, 0.3f);
            marker.transform.position = new Vector3(x, y, 0f);
            if ((marker.transform.position - tpPointImages[destTPPointIdx].transform.position).magnitude <= 0.1f)
            {
                marker.transform.position = tpPointImages[destTPPointIdx].transform.position;
            }
        }
    }
    public void Warp()
    {
        sb.Clear();
        switch (destTPPointIdx)
        {
            case 0:
                sb.Append("MAP001");
                DestMapName = sb.ToString();
                if (GameData.Instance.player.tpActivate[destTPPointIdx] && destTPPointIdx != curTPPointIdx)
                {
                    SceneMgr.Instance.LoadScene(DestMapName, "STPPoint001");
                }
                break;
            case 1:
                sb.Append("MAP014");
                DestMapName = sb.ToString();
                if (GameData.Instance.player.tpActivate[destTPPointIdx] && destTPPointIdx != curTPPointIdx)
                {
                    SceneMgr.Instance.LoadScene(DestMapName, "STPPoint014");
                }
                break;
            case 2:
                sb.Append("MAP021");
                DestMapName = sb.ToString();
                if (GameData.Instance.player.tpActivate[destTPPointIdx] && destTPPointIdx != curTPPointIdx)
                {
                    SceneMgr.Instance.LoadScene(DestMapName, "STPPoint021");
                }
                break;
        }
        
        
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}