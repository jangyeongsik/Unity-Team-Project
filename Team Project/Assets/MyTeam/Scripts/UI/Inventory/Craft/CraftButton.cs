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
        cCon.OnIngredientInfoScreen(btnName, itemID);
    }
    public void OnEquipmentInfoScreen()
    {
        cCon.OnEquipmentInfoScreen(btnName, itemID);
    }
    public void OnSpecialIngScreen()
    {
        cCon.SetSpecialIngredientImages();
    }
}
