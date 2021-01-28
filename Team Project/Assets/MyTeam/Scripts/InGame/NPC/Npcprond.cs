using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npcprond : MonoBehaviour
{
    Vector3 originPos;
    [SerializeField] int[] talk_id;
    int count;

    private bool trigger =false;
    private bool isChack = false;
    private bool giveitem = false;
    public GameObject box;
    public GameObject box2;

    void Start()
    {
        count = 0;
        // NpcSetting();
        GameEventToUI.Instance.player_Trigger += isTrigger;

        GameEventToUI.Instance.talkBtnEvent += TalkChange;
        
    }
    

    private void Update()
    {
;        if((DataManager.Instance.AllInvenData.EquipmentList.Count > 0 || DataManager.Instance.EquipInvenData.CurrentEquipmentList.Count > 0) && !isChack)
        {
            isChack = true;
            count++;
            GameEventToUI.Instance.talkBtnEvent += TalkChange;
        }
        switch (count)
        {
            case 1:
                if (!giveitem) {
                    Give_Item();
                    giveitem = true;
                }

                UIEventToGame.Instance.OnUiEventQuest_Name("무기 만들기");
                UIEventToGame.Instance.OnUiEventQuest_Count("0 / 1");
                break;
            case 2:
                UIEventToGame.Instance.OnUiEventQuest_Name("무기 만들기");
                UIEventToGame.Instance.OnUiEventQuest_Count("1 / 1");
                break;
            case 3:
                UIEventToGame.Instance.OnUiEventQuest_Name("다음 튜토리얼 이동");
                UIEventToGame.Instance.OnUiEventQuest_Count(" ");
                count++;
                break;
        }
        if(count >=3 && DataManager.Instance.EquipInvenData.CurrentEquipmentList.Count > 0)
        {
            box.SetActive(false);
            box2.SetActive(false);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
           
            GameEventToUI.Instance.OnEventTalkBtn(true);
            GameEventToUI.Instance.talk_box += return_Talk_id;
            trigger = true;
        }
    }
    private void OnDestroy()
    {
        GameEventToUI.Instance.player_Trigger -= isTrigger;
    }
    private void OnTriggerStay(Collider other)
    {
        Vector3 dir = other.gameObject.transform.position - this.transform.position;

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 7);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            GameEventToUI.Instance.OnEventTalkBtn(false);
            GameEventToUI.Instance.talk_box -= return_Talk_id;
            trigger = false;
        }
    }

    public bool isTrigger()
    {
        return trigger;
    }

    public int return_Talk_id()
    {
        if(count >= 4)
        {
            return talk_id[3];
        }
        return talk_id[count];
    }
    public void TalkChange()
    {
        count++;
        GameEventToUI.Instance.talkBtnEvent -= TalkChange;
    }

    public void Give_Item()
    {
        Inventory.Instance.AddIngredient(101,3);
        Inventory.Instance.AddIngredient(102,3);
    }
}
