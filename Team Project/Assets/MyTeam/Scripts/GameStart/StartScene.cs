using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text.RegularExpressions;

public class StartScene : MonoBehaviour 
{
    public Text[] values;

    public RectTransform[] Slots;
    public bool[] isSlotEmpty;
    public RectTransform Select;
    public StartSceneEquipment sse;

    int slotIdx = 0;

    public GameObject gameLogo;
    public GameObject startErrorPopup;
    public GameObject errorPopup;
    InputField inputField;

    string pattern = @"^[a-zA-Z0-9가-힣]*$";

    private void Start()
    {
        isSlotEmpty = new bool[3];
        for(int i = 0; i < isSlotEmpty.Length; i++)
        {
            isSlotEmpty[i] = false;
        }

        for(int i = 0; i < GameData.Instance.playerData.Count; ++i)
        {
            if(GameData.Instance.playerData[i].p_name == "")
            {
                Slots[i].GetChild(0).GetComponent<Text>().text = "비어있음";
                isSlotEmpty[i] = true;
            }
            else
            {
                Slots[i].GetChild(0).GetComponent<Text>().text = GameData.Instance.playerData[i].p_name;
                isSlotEmpty[i] = false;
            }
        }

        SlotSelect(Slots[0].gameObject);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gameLogo.SetActive(false);
        }
    }

    //씬 로드
    public void GoToMainGameScene()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭1");
        if (GameData.Instance.player.p_name == "")
        {
            startErrorPopup.SetActive(true);
            return;
        }
        LoadingProgress.LoadScene("UI Scene");
        GameData.Instance.PlayerSave();
        if (!GameData.Instance.playerData[GameData.Instance.playerIdx].tutorial)
        {
            DataManager.Instance.DeleteData();
        }
    }
    //Debug.Log(GameData.Instance.player.stamina);
    //SceneManager.LoadScene("Loading");}

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
                DataManager.Instance.slotIdx = slotIdx;
                DataManager.Instance.SetString();
                DataManager.Instance.InvenLoad();
                DataManager.Instance.EquipLoad();

                //슬롯 데이터 읽기
                GameData.Instance.LoadFromPlayerSlot(i);

                //스킬 적용해놓기
                GameData.Instance.player.SetAniList();

                //스텟창에 텍스트 띄우기
                setPlayerStat(i);

                StartCoroutine(sse.SetImageCoroutine());
            }
        }
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭1");
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
            //values[5].text = GameData.Instance.player.defence.ToString();
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
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭1");
        if (inputField == null)
            inputField = obj.transform.Find("InputField").GetComponent<InputField>();
        //입력 검사
        string text = inputField.text;
        if(!Regex.IsMatch(text,pattern) || text == "")
        {
            inputField.text = "";
            obj.SetActive(false);
            errorPopup.SetActive(true);
            return;
        }
            //슬롯 새로생성
        //GameData.Instance.CreateNewPlayerSlot(slotIdx, obj.transform.Find("InputField").GetComponent<InputField>().text);
        GameData.Instance.CreateNewPlayerSlot(slotIdx,text);
        obj.SetActive(false);

        //스킬 읽어오기
        GameData.Instance.player.SetAniList();

        //슬롯 텍스트 초기화 (없으면 이전에 입력했던거 유지됌@.@)
        obj.transform.Find("InputField").GetComponent<InputField>().text = "";

        //슬롯이름 변경
        Slots[slotIdx].GetChild(0).GetComponent<Text>().text = GameData.Instance.playerData[slotIdx].p_name;
        isSlotEmpty[slotIdx] = false;

        //스탯 텍스트 설정
        setPlayerStat(slotIdx);
    }

    //슬롯 데이터 삭제
    public void DeleteSlot(GameObject obj)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭1");
        GameData.Instance.DeletePlayerData(slotIdx);
        obj.SetActive(false);

        Slots[slotIdx].GetChild(0).GetComponent<Text>().text = "비어있음";
        isSlotEmpty[slotIdx] = true;
        setPlayerStat(slotIdx);
        DataManager.Instance.DeleteData();
        StartCoroutine(sse.SetImageCoroutine());

        GameData.Instance.PlayerSave();
    }

    //obj 팝업 액티브
    public void Popup(GameObject obj)
    {
        obj.SetActive(true);
    }

}
