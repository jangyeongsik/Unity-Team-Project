using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemType;

public class Inventory : MonoBehaviour
{
    List<Weapon> WeaponList = new List<Weapon>();
    List<Armor> ArmorList = new List<Armor>();
    List<Potion> PotionList = new List<Potion>();

    [SerializeField]
    GameObject ItemSlotList;
    [SerializeField]
    List<GameObject> ItemSlots = new List<GameObject>();

    private int a = 0;

    // Start is called before the first frame update
    void Start()
    {
        ItemSlots.Clear();
        for (int i = 0; i < ItemSlotList.transform.childCount; i++)
        {
            ItemSlots.Add(ItemSlotList.transform.GetChild(i).gameObject);
        }
        
        Weapon temp = new Weapon();
        temp.CreateItem(001, "Sword 0", 100, 20, 0, "It's Sword 0");
        WeaponList.Add(temp);

        Weapon temp1 = new Weapon();
        temp1.CreateItem(002, "Sword 1", 110, 21, 1, "It's Sword 1");
        WeaponList.Add(temp1);

        Weapon temp2 = new Weapon();
        temp2.CreateItem(003, "Sword 2", 120, 22, 2, "It's Sword 2");
        WeaponList.Add(temp2);

        Weapon temp3 = new Weapon();
        temp3.CreateItem(004, "Sword 3", 130, 23, 3, "It's Sword 3");
        WeaponList.Add(temp3);

        Weapon temp4 = new Weapon();
        temp4.CreateItem(005, "Sword 4", 140, 24, 4, "It's Sword 4");
        WeaponList.Add(temp4);

        Weapon temp5 = new Weapon();
        temp5.CreateItem(006, "Sword 5", 150, 25, 5, "It's Sword 5");
        WeaponList.Add(temp5);

        for (int i = 0; i < WeaponList.Count; i++)
        {
            ItemSlots[i].GetComponentInChildren<Text>().text = WeaponList[i].Name;
        }
    }
}
