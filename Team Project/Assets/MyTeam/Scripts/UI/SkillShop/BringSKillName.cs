using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringSKillName : MonoBehaviour
{
    public Text skillNameText;
    public Text SkillCylinderCountText;
    public Text CurrentCylinderCountText;

    private void Start()
    {
        UIEventToGame.Instance.SkillName += PopSkillNameOnWindow;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        CurrentCylinderCountText.text = GameData.Instance.player.cylinderCounter.ToString();
    }

    private void OnDestroy()
    {
        UIEventToGame.Instance.SkillName -= PopSkillNameOnWindow;
    }

    public void PopSkillNameOnWindow(string skillName, string skillCylinderCount)
    {
        skillNameText.text = skillName;
        SkillCylinderCountText.text = skillCylinderCount;
    }

    public void PopCurrentCylinderCount(string currentCylinderCount)
    {
        CurrentCylinderCountText.text = currentCylinderCount;
    }
}
