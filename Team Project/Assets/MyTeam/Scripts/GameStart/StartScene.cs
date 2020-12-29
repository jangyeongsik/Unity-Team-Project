using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartScene : MonoBehaviour 
{
    public Text[] values;

    public RectTransform[] Slots;
    public bool[] isSlotEmpty;
    public RectTransform Select;

    int slotIdx = 0;

    private void Start()
    {
        isSlotEmpty = new bool[3];
        for(int i = 0; i < isSlotEmpty.Length; i++)
        {
            isSlotEmpty[i] = false;
        }

        for(int i = 0; i < GameData.Instance.playerData.Count; ++i)
        {
            if(GameData.Instance.playerData[i].name == "")
            {
                Slots[i].GetChild(0).GetComponent<Text>().text = "비어있음";
                isSlotEmpty[i] = true;
            }
            else
            {
                Slots[i].GetChild(0).GetComponent<Text>().text = GameData.Instance.playerData[i].name;
                isSlotEmpty[i] = false;
            }
        }
    }

    private void Update()
    {
        
    }

    //씬 로드
    public void GoToMainGameScene()
    {
        LoadingProgress.LoadScene("MainGameScene");
    }

    //슬롯 선택
    public void SlotSelect(GameObject obj)
    {
        for(int i = 0; i < Slots.Length; ++i)
        {
            if (Slots[i].gameObject == obj)
            {
                if (Select.gameObject.activeSelf == false)
                    Select.gameObject.SetActive(true);
                Select.position = Slots[i].position;

                //현재슬롯 번호
                slotIdx = i;

                //슬롯 데이터 읽기
                GameData.Instance.LoadFromPlayerSlot(i);

                //스텟창에 텍스트 띄우기
                setPlayerStat(i);
            }
        }
    }

    //스텟 텍스트 설정
    void setPlayerStat(int slot)
    {
        if(isSlotEmpty[slot] == true)
        {
            for(int i = 0; i < values.Length; ++i)
            {
                values[i].text = "-";
            }
        }
        else
        {
            values[0].text = GameData.Instance.player.damage.ToString();
            values[1].text = GameData.Instance.player.counter_judgement.ToString();
            values[2].text = GameData.Instance.player.movespeed.ToString();
            values[3].text = GameData.Instance.player.hp.ToString();
            values[4].text = GameData.Instance.player.stamina.ToString();
            values[5].text = GameData.Instance.player.defence.ToString();
        }
    }

    //obj 액티브 끄기
    public void Cancle(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void InputPopupCancle(GameObject obj)
    {
        //슬롯 텍스트 초기화 (없으면 이전에 입력했던거 유지됌@.@)
        obj.transform.Find("InputField").GetComponent<InputField>().text = "";
        obj.SetActive(false);
    }

    //슬롯 생성
    public void CreatePlayer(GameObject obj)
    {
        //슬롯 새로생성
        GameData.Instance.CreateNewPlayerSlot(slotIdx, obj.transform.Find("InputField").GetComponent<InputField>().text);
        obj.SetActive(false);

        //슬롯 텍스트 초기화 (없으면 이전에 입력했던거 유지됌@.@)
        obj.transform.Find("InputField").GetComponent<InputField>().text = "";

        //슬롯이름 변경
        Slots[slotIdx].GetChild(0).GetComponent<Text>().text = GameData.Instance.playerData[slotIdx].name;
        isSlotEmpty[slotIdx] = false;
    }

    //슬롯 데이터 삭제
    public void DeleteSlot(GameObject obj)
    {
        GameData.Instance.DeletePlayerData(slotIdx);
        obj.SetActive(false);

        Slots[slotIdx].GetChild(0).GetComponent<Text>().text = "비어있음";
        isSlotEmpty[slotIdx] = true;
    }

    //obj 팝업 액티브
    public void Popup(GameObject obj)
    {
        obj.SetActive(true);
    }

}
