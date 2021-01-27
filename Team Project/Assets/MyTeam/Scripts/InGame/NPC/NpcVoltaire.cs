using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class NpcVoltaire : MonoBehaviour
{

    Vector3 originPos;
    [SerializeField] int[] talk_id;
    public GameObject box;
    public GameObject box2;
    public GameObject p_1;
    public GameObject p_2;
    private bool trigger =false;
    private bool isCollider =false;
    private int count;
    private bool isTrue;
    int t_count;

    StringBuilder quest_str;
    void Start()
    {
        count = 0;
        t_count = 0;
        isTrue = false;
        trigger = false;
        isCollider = false;
    GameEventToUI.Instance.player_Trigger += isTrigger;

        quest_str = new StringBuilder();
    }

    private void Update()
    {
        if (p_1.activeSelf && !p_2.activeSelf)
        {
            t_count = 1;
        }
        else if (p_2.activeSelf && !p_1.activeSelf)
        {
            t_count = 1;
        }
        else if (p_1.activeSelf && p_2.activeSelf && !isTrue)
        {
            t_count = 2;
            isTrue = true;
            count++;
            GameEventToUI.Instance.talkBtnEvent += TalkChange;
            
        }
        if(count >= 3)
        {
            box2.SetActive(false);
        }
        quest_str.Clear();
        quest_str.Append(t_count.ToString());
        quest_str.Append(" / 2");
        switch (count)
        {
            case 1:
            case 2:
                UIEventToGame.Instance.OnUiEventQuest_Name("신전 및 석상 상호작용");
                UIEventToGame.Instance.OnUiEventQuest_Count(quest_str.ToString());
                break;
            case 3:
                UIEventToGame.Instance.OnUiEventQuest_Name("출발지로 이동");
                UIEventToGame.Instance.OnUiEventQuest_Count(" ");
                count++;
                break;

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollider&& other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isCollider = true;
            GameEventToUI.Instance.talkBtnEvent += TalkChange;
        }
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
        GameEventToUI.Instance.talkBtnEvent -= TalkChange;
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
        if (count >= 4)
        {
            return talk_id[3];
        }
        return talk_id[count];
    }
    public void TalkChange()
    {
        count++;
        box.SetActive(false);
        GameEventToUI.Instance.talkBtnEvent -= TalkChange;
    }

}
