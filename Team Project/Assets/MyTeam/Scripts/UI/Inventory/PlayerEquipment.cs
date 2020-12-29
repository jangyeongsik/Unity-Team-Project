using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment
{
    public List<PlayerInventory> EquipingItems = new List<PlayerInventory>();

    public void EquipItem(PlayerInventory _item)
    {
        Equipment temp = new Equipment();
        temp = GameData.Instance.findEquipment(_item.name);

        for (int i = 0; i < EquipingItems.Count; i++)
        {
            
        }

        switch (temp.equipmentType)
        {
            //WEAPON
            case 1:
                break;
            //ARMOR
            case 2:
                break;
            //HELM
            case 3:
                break;
            //GLOVE
            case 4:
                break;
            //SHOES
            case 5:
                break;
        }
    }

}
