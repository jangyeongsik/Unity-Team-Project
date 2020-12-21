using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemType
{
    public enum Type
    {
        Weapon,
        Armor,
        Potion,
        Ingred,
        Misc
    }

    public abstract class ItemData
    {
        protected int itemCode;
        protected string name;
        protected Type type;
        protected Texture texture;
        protected int buyPrice;
        protected int sellPrice;
        protected int stat;
        protected string description;

        public abstract void CreateItem(int itemCode, string name, int buyPrice, int sellPrice, int stat, string description);
    }

    public class Weapon : ItemData
    {
        int atk;

        public string Name { get { return name; } }

        public override void CreateItem(int itemCode, string name, int buyPrice, int sellPrice, int stat, string description)
        {
            this.itemCode = itemCode;
            this.name = name;
            this.type = Type.Weapon;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
            this.description = description;
            this.atk = stat;
        }
    }

    public class Armor : ItemData
    {
        int def;

        public override void CreateItem(int itemCode, string name, int buyPrice, int sellPrice, int stat, string description)
        {
            this.itemCode = itemCode;
            this.name = name;
            this.type = Type.Weapon;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
            this.description = description;
            this.def = stat;
        }
    }

    public class Potion : ItemData
    {
        int recoveryStat;
        public override void CreateItem(int itemCode, string name, int buyPrice, int sellPrice, int stat, string description)
        {
            this.itemCode = itemCode;
            this.name = name;
            this.type = Type.Weapon;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
            this.description = description;
            this.recoveryStat = stat;
        }
    }
}