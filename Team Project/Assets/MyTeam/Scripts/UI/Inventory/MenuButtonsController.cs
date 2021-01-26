using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class MenuButtonsController : MonoBehaviour, IPointerClickHandler
{
    Toggle instance;
    public GotoShopScene gtSS;
    int ToggleNum;
    private void Awake()
    {
        instance = gameObject.GetComponent<Toggle>();
        for (int i = 0; i < gtSS.Toggles.Length; i++)
        {
            if (gtSS.Toggles[i] == instance)
            {
                ToggleNum = i;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭1");
        gtSS.ChangeScreen(ToggleNum + 1);
    }
    public void SetIsOn()
    {
        for (int i = 0; i < gtSS.Toggles.Length; i++)
        {
            if (gtSS.Toggles[i] != instance)
            {
                gtSS.Toggles[i].gameObject.GetComponent<Animator>().ResetTrigger("Selected");
                gtSS.Toggles[i].gameObject.GetComponent<Animator>().SetTrigger("Deselected");
            }
        }
        instance.isOn = true;
        gameObject.GetComponent<Animator>().ResetTrigger("Deselected");
        gameObject.GetComponent<Animator>().SetTrigger("Selected");
    }
    public void ResetTrigger()
    {
        gameObject.GetComponent<Animator>().ResetTrigger("Selected");
        gameObject.GetComponent<Animator>().SetTrigger("Deselected");
    }
}
