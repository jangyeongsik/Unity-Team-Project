using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ItemID;
    public string itemName;
    public int itemCategory;
    public string itemGrade;
    public string itemScriptID;

   
}
public enum ItemCategory
{
    Equipment,
    Consumable,
    Ingredient,
    Misc
}