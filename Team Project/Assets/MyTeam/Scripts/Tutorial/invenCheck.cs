using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenCheck : MonoBehaviour
{
    public GameObject box;
    public int talk_id;
    private bool isTalk;
    bool check;

    private void Update()
    {
        //조건 완료 후 박스 콜리이더 트리거 체크
        //제작했냐안했냐
        if(DataManager.Instance.EquipInvenData.CurrentEquipmentList.Count > 0)
        {
            box.SetActive(false);
            check = true;
            gameObject.SetActive(false);

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(!check && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameEventToUI.Instance.OnEvent_TalkBox(talk_id);
            isTalk = true;
        }
    }



}
