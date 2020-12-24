using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemCategory
{
    Equipment,
    Consumable,
    Ingredient,
    Misc
}
public class PlayerInventory
{
    public int ID;
    public string name;
    public int scriptName;
    public Sprite image;
    public ItemCategory itemCategory;
    public int count;
}
