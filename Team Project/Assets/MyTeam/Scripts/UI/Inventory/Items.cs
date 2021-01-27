using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum EQUIPMENTTYPE
{
    WEAPON = 1,
    HELM,
    ARMOR,
    GLOVE,
    BOOTS
}
public enum ITEMCATEGORY
{
    EQUIPMENT = 1,
    INGREDIENT,
    MISC
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
    public float speed;
    public float critPercent;
    public float critDamage;
    public int HPAdd;
    public int staminaAdd;
    public float def;
    public int counterJudgement;
    public int count;
    public int productionID;
}
[Serializable]
public class Ingredient //아이템 재료 db
{
    public int ID;
    public string name;
    public ITEMCATEGORY itemCategory;
    public int itemGrade;
    public int itemScriptID;
    public int count;
}
[Serializable]
public class Misc // 기타 
{
    public int ID;
    public string name;
    public ITEMCATEGORY itemCategory;
    public int itemGrade;
    public int itemScriptID;
    public int count;
}
[Serializable]
public class Production //제작도 묶음
{
    public int productionID;
    public int normal_ItemID;
    public int normal_Ingredient_1_ID;
    public int normal_Ingredient_1_Count;
    public int normal_Ingredient_2_ID;
    public int normal_Ingredient_2_Count;
    public int rare_Item_ID;
    public int rare_Ingredient_1_ID;
    public int rare_Ingredient_1_Count;
    public int rare_Ingredient_2_ID;
    public int rare_Ingredient_2_Count;
    public int unique_Item_1_ID;
    public int unique_Item_2_ID;
    public int unique_Item_3_ID;
    public int unique_Ingredient_1_ID;
    public int unique_Ingredient_1_Count;
    public int unique_Ingredient_2_ID;
    public int unique_Ingredient_2_Count;
    public int unique_Ingredient_3_ID;
    public int unique_Ingredient_3_Count;
}