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
    public List<Equipment> EquipmentList = new List<Equipment>();
    public List<Ingredient> IngredientList = new List<Ingredient>();
    public List<Misc> MiscList = new List<Misc>();
}

