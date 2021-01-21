using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    void Start()
    {
        GameEventToUI.Instance.player_Trigger += isTrigger;
       

    }

    private void Update()
    {
        if(p_1.activeSelf && p_2.activeSelf && !isTrue)
        {
            isTrue = true;
            count++;
            GameEventToUI.Instance.talkBtnEvent += TalkChange;
            box2.SetActive(false);
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
        return talk_id[count];
    }
    public void TalkChange()
    {
        count++;
        box.SetActive(false);
        GameEventToUI.Instance.talkBtnEvent -= TalkChange;
    }

}
