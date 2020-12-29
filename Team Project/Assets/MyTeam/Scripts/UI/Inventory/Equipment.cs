using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EQUIPMENTTYPE
{
    WEAPON = 1,
    ARMOR,
    HELM,
    GLOVE,
    SHOES
}
public class Equipment
{
    public int ID;
    public string Name;
    public ITEMCATEGORY Category;
    public int  equipmentType;
    public string itemGrade;
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
}