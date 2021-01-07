using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTeleporter : MonoBehaviour
{
    List<Misc> miscList;
    public void SetActive()
    {
        UIEventToGame.Instance.OnTPActivate(true);
        gameObject.SetActive(false);

        //아이템 생기면 다시 테스트해봐야함
        //miscList = DataManager.Instance.AllInvenData.MiscList;
        //for (int i = 0; i < miscList.Count; i++)
        //{
        //    if (miscList[i].name == "실린더" && miscList[i].count >= 10)
        //    {
        //        UIEventToGame.Instance.OnTPActivate(true);
        //    }
        //}
    }
    public void Cancel()
    {
        UIEventToGame.Instance.OnCancel(true);
        gameObject.SetActive(false);
    }
}
