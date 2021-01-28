using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainEnemy : MonoBehaviour
{
    public TMP_Text value;

    private void Update()
    {
        if (GameData.Instance.player.curSceneName.Equals("MAP000"))
            value.text = "0";
        else 
            value.text = GameData.Instance.player.enemyData.Count.ToString();
    }
}
