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

        for(int i = 0; i < GameData.Instance.playerData.Length; ++i)
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

    public void GoToMainGameScene()
    {
        LoadingProgress.LoadScene("MainGameScene");
    }

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

    public void Cancle(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void CreatePlayer(GameObject obj)
    {
        //슬롯 새로생성
        GameData.Instance.CreateNewPlayerSlot(slotIdx, obj.transform.Find("InputField").GetComponent<InputField>().text);
        obj.SetActive(false);

        //슬롯이름 변경
        Slots[slotIdx].GetChild(0).GetComponent<Text>().text = GameData.Instance.playerData[slotIdx].name;
        isSlotEmpty[slotIdx] = false;
    }

    public void DeleteSlot(GameObject obj)
    {
        GameData.Instance.DeletePlayerData(slotIdx);
        obj.SetActive(false);

        Slots[slotIdx].GetChild(0).GetComponent<Text>().text = "비어있음";
        isSlotEmpty[slotIdx] = true;
    }

    public void Popup(GameObject obj)
    {
        obj.SetActive(true);
    }
}
