using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneEquipment : MonoBehaviour
{
    public EquipSlot[] EquipList;
    private void Start()
    {
        EquipList = GetComponentsInChildren<EquipSlot>();
        StartCoroutine(SetImageCoroutine());
    }
    public IEnumerator SetImageCoroutine()
    {
        yield return new WaitUntil(() => DataManager.Instance.EquipInvenData != null);
        for (int i = 0; i <= EquipList.Length; i++)
        {
            if (DataManager.Instance.FindEquipmentScriptID((EQUIPMENTTYPE)(i + 1)) == 0)
            {

                EquipList[i].itemImage.sprite = GameData.Instance.itemImages[44];
                continue;
            }
            EquipList[i].itemImage.sprite = GameData.Instance.itemImages[DataManager.Instance.FindEquipmentScriptID((EQUIPMENTTYPE)(i + 1))];
        }
    }
}
