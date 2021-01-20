using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipItemStatInfo : MonoBehaviour
{
    public TMP_Text Damage;
    public TMP_Text Judement;
    public TMP_Text Speed;
    public TMP_Text Hp;
    public TMP_Text Stamina;

    private void OnEnable()
    {
        SetText();
    }

    public void SetText()
    {
        Damage.text = GameData.Instance.player.damage.ToString();
        Judement.text = GameData.Instance.player.counter_judgement.ToString();
        Speed.text = GameData.Instance.player.movespeed.ToString();
        Hp.text = GameData.Instance.player.hp.ToString();
        Stamina.text = GameData.Instance.player.stamina.ToString();
    }
}
