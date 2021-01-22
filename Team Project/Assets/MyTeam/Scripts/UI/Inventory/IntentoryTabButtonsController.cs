using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IntentoryTabButtonsController : MonoBehaviour, IPointerClickHandler
{
    Toggle instance;
    int tabNum;
    private void Awake()
    {
        instance = gameObject.GetComponent<Toggle>();
        for (int i = 0; i < Inventory.Instance.InvenTabs.Length; i++)
        {
            if (Inventory.Instance.InvenTabs[i] == instance)
            {
                tabNum = i;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "미니맵");
        Inventory.Instance.ChangeTab(tabNum);
    }
}
