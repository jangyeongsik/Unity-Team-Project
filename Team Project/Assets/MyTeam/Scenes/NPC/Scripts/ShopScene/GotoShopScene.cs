using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GotoShopScene : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject TalkCanvas;

    private bool startTalking = false;
    Text talk;

    List<NPCReader.NPCTalk> talkData;

    int count;
    int firstTxt;

    

    private void Awake()
    {
        BetweenPlayerAndShop.Instance.onOff += ShopOn;
        BetweenPlayerAndShop.Instance.talk += TalkOn;
        talkData = new List<NPCReader.NPCTalk>();
    }

    private void Start()
    {
        GameObject text = TalkCanvas.transform.Find("sorse").gameObject;
        talk = text.GetComponent<Text>() as Text;
        Test();

    }

    void Update()
    {
        NextDialouge();
    }

    void Test()
    {
        NPCReader.NPCTalk t = new NPCReader.NPCTalk();
        t.id = 1000;
        t.name = "Npc";
        t.talk = new List<string>();
        t.talk.Add("ㅎㅇ");
        t.talk.Add("1");
        t.talk.Add("2");
        t.talk.Add("3");

        talkData.Add(t);
    }


    public void ShopOn(bool isOn)
    {
        shopCanvas.SetActive(isOn);
    }

    public void TalkOn(bool isOn, int id, string NpcName)
    {
        FindNpc(id, NpcName);
        TalkCanvas.SetActive(isOn);
    }

    public void FindNpc(int id, string NpcName)
    {
        for (int i = 0; i < talkData.Count; i++)
        {
            if(talkData[i].id == id && talkData[i].Equals(NpcName))
            {
                for (int k = 0; k < talkData[i].talk.Count; k++)
                {
                    firstTxt = k;
                    startTalking = true;
                    break;
                }
            }
        }
        talk.text = talkData[firstTxt].talk[count];
    }

    void NextDialouge()
    {
        if(startTalking)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (count < talkData[firstTxt].talk.Count)
                    talk.text = talkData[firstTxt].talk[count++];
            }
        }
    }
}
