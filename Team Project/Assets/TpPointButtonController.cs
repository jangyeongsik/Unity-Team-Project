using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPointButtonController : MonoBehaviour
{
    TpPointButton[] tpPointButtons;
    public SelectiveTeleporterTpScreen STTS;
    void Start()
    {
        tpPointButtons = GetComponentsInChildren<TpPointButton>();
        for (int i = 0; i < 3; i++)
        {
            tpPointButtons[i].num = i;
        }
    }

    public void CheckIsSelected(int num)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == num)
            {
                tpPointButtons[i].GetComponent<Animator>().ResetTrigger("Deselected");
                tpPointButtons[i].GetComponent<Animator>().SetTrigger("Selected");
                STTS.destTPPointIdx = i;
            }
            else
            {
                tpPointButtons[i].GetComponent<Animator>().ResetTrigger("Selected");
                tpPointButtons[i].GetComponent<Animator>().SetTrigger("Deselected");
            }
        }
    }
}
