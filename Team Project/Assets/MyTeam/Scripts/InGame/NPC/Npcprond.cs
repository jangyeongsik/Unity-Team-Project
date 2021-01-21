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

    void Start()
    {
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
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
           /* Vector3 pos = other.gameObject.transform.position - gameObject.transform.position;
            pos.y = 0;
            gameObject.transform.LookAt(gameObject.transform.position + pos);*/

           
            GameEventToUI.Instance.OnEventTalkBtn(true);
            GameEventToUI.Instance.talk_box += return_Talk_id;
            trigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 dir = other.gameObject.transform.position - this.transform.position;

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 15);
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
        return talk_id[count];
    }
    public void TalkChange()
    {
        count++;
        GameEventToUI.Instance.talkBtnEvent -= TalkChange;
    }
}
