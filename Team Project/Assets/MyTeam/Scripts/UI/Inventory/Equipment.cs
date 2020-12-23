using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
    {
        Weapon,
        Armor,
        Helm,
        Glove,
        Shoes
    }

[System.Serializable]
public class Equipment
{
    public string ID;
    public string Name;
    public string Category;
    public string equipmentType;
    public string itemGrade;
    public string itemScriptID;
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