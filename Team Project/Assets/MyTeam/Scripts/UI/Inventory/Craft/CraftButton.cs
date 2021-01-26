using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public int itemID;
    public string btnName;
    public CraftController cCon;
    
    public void OnIngredientInfoScreen()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        cCon.OnIngredientInfoScreen(btnName, itemID);
    }
    public void OnEquipmentInfoScreen()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        cCon.OnEquipmentInfoScreen(btnName, itemID);
    }
    public void OnSpecialIngScreen()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        cCon.SetSpecialIngredientImages();
    }
}
