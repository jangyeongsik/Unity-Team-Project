using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainEnemy : MonoBehaviour
{
    public TMP_Text value;

    private void Update()
    {
        value.text = GameData.Instance.player.enemyData.Count.ToString();
    }
}
