using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TempleItemDropCanvas : MonoBehaviour
{
    public GameObject slots;
    public List<GameObject> slotList;
    public List<Image> imageList;
    public List<TMP_Text> countTextList;
    public PrayBtn pBtn;
    Dictionary<int, int> rewards;
    int rewardCount;
    bool isAdded;

    private void Start()
    {
        //initialize variables
        GameEventToUI.Instance.TempleItemDropInfo += OnItemDropInfo;
        gameObject.SetActive(false);
        rewards = new Dictionary<int, int>();
        slotList = new List<GameObject>();
        imageList = new List<Image>();
        countTextList = new List<TMP_Text>();
        isAdded = false;
        //전에 켜진적이 없으면 리스트에 슬롯, 이미지 담아주기
        // 켜진적이 있으면 스킵
        if (!isAdded)
        {
            for (int i = 0; i < slots.transform.childCount; i++)
            {
                slotList.Add(slots.transform.GetChild(i).gameObject);
                slotList[i].SetActive(false);
            }
            for (int i = 0; i < slotList.Count; i++)
            {
                imageList.Add(slotList[i].transform.GetChild(0).GetComponent<Image>());
                countTextList.Add(slotList[i].transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>());
            }

            isAdded = true;
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        //열쇠 2개 = 보상 1개, 열쇠 5개 = 보상 3개, 열쇠 10 개 = 보상 7개
        //보상 갯수 설정
        rewardCount = (pBtn.count == 5) ? 1 : (pBtn.count == 10) ? 3 : (pBtn.count == 20) ? 7 : 0;
        //101 ~ 104 딕셔너리에 기본으로 0 집어넣기
        for (int i = 101; i < 105; i++)
        {
            if (rewards != null)
            {
                if (!rewards.ContainsKey(i))
                {
                    rewards.Add(i, 0);
                }
                else
                {
                    rewards[i] = 0;
                }
            }
        }
    }
    public void Close()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        GameEventToUI.Instance.TempleItemDropInfo -= OnItemDropInfo;
    }

    void OnItemDropInfo(bool value, int MapIdx)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "보상획득");
        gameObject.SetActive(value);
        //스위치 문은 추후에 신전 추가되면 활성화 시키기
        //switch (MapIdx)
        //{
        //    case 007:
                RandomDrop();
                for (int i = 0; i < 4; i++)
                {
                    //재료 번호 설정 (101 ~ 104)
                    int ingredientID = i + 101;
                    //개수가 0개면 (하나도 드랍이 안되면) 스킵
                    if (rewards[ingredientID] == 0) { continue; }
                    //슬롯 켜기
                    slotList[i].SetActive(true);
                    //이미지 설정
                    imageList[i].sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(ingredientID).itemScriptID];
                    //랜덤드랍 받은 개수만큼 인벤토리에 넣어줌
                    for (int j = 0; j < rewards[ingredientID]; j++)
                    {
                        Inventory.Instance.AddIngredient(ingredientID);
                    }
                    //개수 텍스트 변경
                    countTextList[i].text = rewards[ingredientID].ToString();
                }
        //        break;
        //}
    }
    void RandomDrop()
    {
        int num;
        
        for (int i = 0; i < rewardCount; i++)
        {
            num = Random.Range(101, 105);
            rewards[num]++;
        }
    }
}
