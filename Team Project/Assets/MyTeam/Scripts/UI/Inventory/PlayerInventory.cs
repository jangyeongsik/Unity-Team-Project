using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum ITEMCATEGORY
{
    EQUIPMENT,
    INGREDIENT,
    CONSUMABLE,
    MISC
}
[Serializable]
public class PlayerInven
{
    public List<PlayerInventory> ListData = new List<PlayerInventory>();
}
[Serializable]
public class PlayerInventory
{
    public int ID;
    public string name;
    public int scriptName;
    public string itemGrade;
    [NonSerialized]
    public Sprite image;
    public ITEMCATEGORY itemCategory;
    public int count;

    public PlayerInventory() { }
    public PlayerInventory(int _ID, string _name, int _scriptName, string _itemGrade, Sprite _image, ITEMCATEGORY _itemCategory, int _count)
    {
        ID = _ID;
        name = _name;
        scriptName = _scriptName;
        itemGrade = _itemGrade;
        image = _image;
        itemCategory = _itemCategory;
        count = _count;
    }
}

