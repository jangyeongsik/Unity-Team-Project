using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GotoShopScene : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject TalkCanvas;
    public GameObject miniMapCanvas;

    private bool startTalking = false;
    Text talk;

    int count;
    int firstTxt;

    private void Awake()
    {
        BetweenPlayerAndShop.Instance.onOff += ShopOn;
        BetweenPlayerAndShop.Instance.miniOnOff += MiniMapOn; ;
        BetweenPlayerAndShop.Instance.talk += TalkOn;
    }

    private void Start()
    {
        GameObject text = TalkCanvas.transform.Find("sorse").gameObject;
        talk = text.GetComponent<Text>() as Text;
    }

    void Update()
    {
        NextDialouge();
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

    public void MiniMapOn(bool isOn)
    {
        miniMapCanvas.SetActive(isOn);
    }

    public void FindNpc(int id, string NpcName)
    {
        for (int i = 0; i < talkdataTest.Instance.data.Count; i++)
        {
            if(talkdataTest.Instance.data[i].id == id && talkdataTest.Instance.data[i].name.Equals(NpcName))
            {
                firstTxt = i;
                startTalking = true;
                count = 0;
                break;
            }
        }
        talk.text = talkdataTest.Instance.data[firstTxt].talk[count++];
    }

    void NextDialouge()
    {
        if(startTalking)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log(count + "dd " + talkdataTest.Instance.data[firstTxt].talk.Count);
                if (count < talkdataTest.Instance.data[firstTxt].talk.Count)
                {
                    talk.text = talkdataTest.Instance.data[firstTxt].talk[count++];
                }
                else
                {
                    startTalking = false;
                    TalkCanvas.SetActive(false);
                }
            }
        }
    }


}
