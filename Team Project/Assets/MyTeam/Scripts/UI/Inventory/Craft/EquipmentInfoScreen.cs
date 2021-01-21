using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentInfoScreen : MonoBehaviour
{
    public bool isAble;
    public Image itemImage;
    public TMP_Text ableText;
    public TMP_Text description;
    public TMP_Text itemName;
    public Button forgeButton;
    public string btnName;
    public int itemID;
    
    public void SetForgeButtonInteractable(bool state)
    {
        isAble = state;
        if (isAble)
        {
            forgeButton.interactable = true;
        }
        else
        {
            forgeButton.interactable = false;
        }
    }

}
