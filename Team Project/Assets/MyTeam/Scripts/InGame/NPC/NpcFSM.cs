using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcFSM : MonoBehaviour
{
    [SerializeField] int[] talk_id;

    private bool trigger =false;

    void Start()
    {
        GameEventToUI.Instance.player_Trigger += isTrigger;
        GameEventToUI.Instance.talkBtnEvent += TalkChange;

    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.player_Trigger -= isTrigger;
        GameEventToUI.Instance.talkBtnEvent -= TalkChange;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (GameData.Instance.player.Talk_Box[0])
            {
                GameEventToUI.Instance.OnEventSkillShopPush();
            }
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
            if (GameData.Instance.player.Talk_Box[0])
            {
                GameEventToUI.Instance.onEventSkillShopback();
            }
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
        if (GameData.Instance.player.Talk_Box[0])
        {
            return talk_id[1];
        }
        return talk_id[0];
    }
    public void TalkChange()
    {
        GameData.Instance.player.Talk_Box[0] = true;
        GameEventToUI.Instance.talkBtnEvent -= TalkChange;
    }
}
