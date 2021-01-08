using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipSlot : MonoBehaviour
{
    public Image itemImage;
    private void Start()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }
}
