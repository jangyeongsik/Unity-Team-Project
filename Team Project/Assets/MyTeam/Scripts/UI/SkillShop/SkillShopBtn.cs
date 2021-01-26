using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillShopBtn : MonoBehaviour
{
    public GameObject SkillInfo;
    //public GameObject SkillInfo;
    public Text skillName;
    bool InfoWindowOnoff;
    bool SucceedWinodwOnoff;

    public void OpenSkillBuyInfo()
    {
        InfoWindowOnoff = !InfoWindowOnoff;

        SkillInfo.SetActive(InfoWindowOnoff);
    }

    //public void CloseSuccedWindow()
    //{
    //    SucceedWinodwOnoff = !SucceedWinodwOnoff;
    //
    //}
}
