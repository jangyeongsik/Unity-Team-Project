using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TpPointButton : MonoBehaviour, IPointerClickHandler
{
    public TpPointButtonController TPBC;
    public int num;
    public void OnPointerClick(PointerEventData pED)
    {
        TPBC.CheckIsSelected(num);
    }
}
