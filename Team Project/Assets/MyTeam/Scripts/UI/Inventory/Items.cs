using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum EQUIPMENTTYPE
{
    WEAPON = 1,
    ARMOR,
    HELM,
    GLOVE,
    BOOTS
}
public class Items {}
[Serializable]
public class Equipment
{
    public int ID;
    public string name;
    public ITEMCATEGORY itemCategory;
    public EQUIPMENTTYPE  equipmentType;
    public int itemGrade;
    public int itemScriptID;
    public float damage;
    public float moveSpeed;
    public float critPercent;
    public float critDamage;
    public float attackSpeed;
    public int HPAdd;
    public int staminaAdd;
    public float def;
    public int counterJudgement;
    public int count;
}
[Serializable]
public class Ingredient
{
    public int ID;
    public string name;
    public ITEMCATEGORY itemCategory;
    public int scriptName;
    public int itemGrade;
    public int itemScriptID;
    [NonSerialized]
    public Sprite image;
    public int count;
}
[Serializable]
public class Misc
{
    public int ID;
    public string name;
    public ITEMCATEGORY itemCategory;
    public int scriptName;
    public int itemGrade;
    public int itemScriptID;
    [NonSerialized]
    public Sprite image;
    public int count;
}