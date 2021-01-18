using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public GameObject[] tip;
    public GameObject leftBtn;
    public GameObject rightBtn;
    public GameObject Exit;

    private int number = 0;


    public void TipOn()
    {
        Exit.SetActive(true);
        rightBtn.SetActive(true);
        leftBtn.SetActive(true);
        tip[number].SetActive(true);


    }

    public void TipOff()
    {
         tip[number].SetActive(false);
        Exit.SetActive(false);
        rightBtn.SetActive(false);
        leftBtn.SetActive(false);
    }

    public void LeftBtn()
    {
        tip[number].SetActive(false);
        number--;
        if (number < 0)
        {
            number = tip.Length-1;
        }
        tip[number].SetActive(true);
    }

    public void RightBtn()
    {
        tip[number].SetActive(false);
        number++;
        if (number > (tip.Length-1))
        {
            number = 0;
        }
        tip[number].SetActive(true);
    }
}
