using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class NpcRolin : MonoBehaviour
{
    Vector3 originPos;
    [SerializeField] int[] talk_id;
    int count;
    
    public GameObject Box;
    public GameObject Collider;
    private bool trigger =false;
    private bool isChack;
    private bool inTrue;
    private bool isTalk;
    StringBuilder quest_str;


    private int attackCount = 0;
    void Start()
    {
        // NpcSetting();
        count = 0;
        isChack = false; 
        inTrue = false;
        isTalk = false;
    GameEventToUI.Instance.player_Trigger += isTrigger;

        GameEventToUI.Instance.TutoAttack += AttackCount;
        quest_str = new StringBuilder();
    }
    private void OnDestroy()
    {
        
        GameEventToUI.Instance.player_Trigger -= isTrigger;
    }
    private void Update()
    {
        if(attackCount >= 5 && !isChack)
        {
            isChack = true;
            count++;
            GameEventToUI.Instance.TutoAttack -= AttackCount;
            GameEventToUI.Instance.talkBtnEvent += TalkChange;
        }
        if(count >=3)
        {
            Collider.SetActive(false);
        }
        quest_str.Append(attackCount.ToString());
        quest_str.Append(" / 5");
        switch (count)
        {
            case 1:
            case 2:
                UIEventToGame.Instance.OnUiEventQuest_Name("몬스터 공격 카운트");
                UIEventToGame.Instance.OnUiEventQuest_Count(quest_str.ToString());
                break;
            case 3:
                UIEventToGame.Instance.OnUiEventQuest_Name("다음 튜토리얼 이동");
                UIEventToGame.Instance.OnUiEventQuest_Count(" ");
                count++;
                break;

        }
        quest_str.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!inTrue && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameEventToUI.Instance.talkBtnEvent += TalkChange;
            inTrue = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameEventToUI.Instance.OnEventTalkBtn(true);
            GameEventToUI.Instance.talk_box += return_Talk_id;
            trigger = true;
        }
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
        Box.SetActive(false);
        GameEventToUI.Instance.talkBtnEvent -= TalkChange;
    }

    public void AttackCount()
    {
        attackCount++;
    }
}
