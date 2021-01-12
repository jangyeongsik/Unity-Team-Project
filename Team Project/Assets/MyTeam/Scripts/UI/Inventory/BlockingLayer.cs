using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BlockingLayer : MonoBehaviour, IPointerClickHandler
{
    CraftController cCon;
    ItemInfoScreen iis;
    private void Start()
    {
        if (transform.parent.name.Contains("UI"))
        {
            iis = transform.parent.parent.GetComponent<ItemInfoScreen>();
        }
        else
        {
            cCon = transform.parent.parent.GetComponent<CraftController>();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (iis != null)
        {
            iis.CloseTab();
        }
        else
        {
            if (transform.parent.name.Contains("Special"))
            {
                cCon.InactiveSpecialIngScreen();
            }
            else if (transform.parent.name.Contains("Ingredient"))
            {
                cCon.InactiveIngredientInfoScreen();
            }
            else if (transform.parent.name.Contains("Equipment"))
            {
                cCon.InactiveEquipmentInfoScreen();
            }
        }
    }
}
