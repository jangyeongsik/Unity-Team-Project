using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum ItemCategory
{
    Equipment,
    Consumable,
    Ingredient,
    Misc
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
    public Sprite image;
    public ItemCategory itemCategory;
    public int count;

    public PlayerInventory() { }
    public PlayerInventory(int _ID, string _name, int _scriptName, Sprite _image, ItemCategory _itemCategory, int _count)
    {
        ID = _ID;
        name = _name;
        scriptName = _scriptName;
        image = _image;
        itemCategory = _itemCategory;
        count = _count;
    }
}

