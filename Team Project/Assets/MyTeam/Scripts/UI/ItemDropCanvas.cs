using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropCanvas : MonoBehaviour
{
    public Image image1;
    public Image image2;

    private void Start()
    {
        GameEventToUI.Instance.ItemDropInfo += OnItemDropInfo;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.ItemDropInfo -= OnItemDropInfo;
    }
    public void Close()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        gameObject.SetActive(false);
    }
    void OnItemDropInfo(bool value, int MapIdx)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "보상획득");
        gameObject.SetActive(value);
        switch (MapIdx)
        {
            case 008:
                //이미지 설정
                image1.sprite = GameData.Instance.itemImages[46];
                image2.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(105).itemScriptID];

                GameEventToUI.Instance.OnAddStamina(10);
                Inventory.Instance.AddIngredient(105);
                break;
            case 009:
                image1.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(108).itemScriptID];
                image2.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(111).itemScriptID];

                Inventory.Instance.AddIngredient(108);
                Inventory.Instance.AddIngredient(111);
                break;
            case 010:
                image1.sprite = GameData.Instance.itemImages[45];
                image2.sprite = GameData.Instance.itemImages[46];

                GameEventToUI.Instance.OnAddMaxHp(1);
                GameEventToUI.Instance.OnAddStamina(10);
                break;
            case 018:
                image1.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(114).itemScriptID];
                image2.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(117).itemScriptID];

                Inventory.Instance.AddIngredient(114);
                Inventory.Instance.AddIngredient(117);
                break;
            case 019:
                image1.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(106).itemScriptID];
                image2.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(109).itemScriptID];

                Inventory.Instance.AddIngredient(106);
                Inventory.Instance.AddIngredient(109);
                break;
            case 020:
                image1.sprite = GameData.Instance.itemImages[45];
                image2.sprite = GameData.Instance.itemImages[46];

                GameEventToUI.Instance.OnAddMaxHp(1);
                GameEventToUI.Instance.OnAddStamina(10);
                break;
            case 013:
                image1.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(112).itemScriptID];
                image2.sprite = GameData.Instance.itemImages[45];

                GameEventToUI.Instance.OnAddMaxHp(1);
                Inventory.Instance.AddIngredient(112);
                break;
            case 012:
                image1.sprite = GameData.Instance.itemImages[46];
                image2.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(115).itemScriptID];

                GameEventToUI.Instance.OnAddStamina(10);
                Inventory.Instance.AddIngredient(115);
                break;
            case 023:
                image1.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(118).itemScriptID];
                image2.sprite = GameData.Instance.itemImages[46];

                GameEventToUI.Instance.OnAddStamina(10);
                Inventory.Instance.AddIngredient(118);
                break;
            case 024:
                image1.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(107).itemScriptID];
                image2.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(110).itemScriptID];

                Inventory.Instance.AddIngredient(107);
                Inventory.Instance.AddIngredient(110);
                break;
            case 026:
                image1.sprite = GameData.Instance.itemImages[45];
                image2.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(113).itemScriptID];

                GameEventToUI.Instance.OnAddMaxHp(1);
                Inventory.Instance.AddIngredient(113);
                break;
            case 027:
                image1.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(116).itemScriptID];
                image2.sprite = GameData.Instance.itemImages[GameData.Instance.FindIngredientByID(119).itemScriptID];

                Inventory.Instance.AddIngredient(116);
                Inventory.Instance.AddIngredient(119);
                break;
        }
    }
}
