using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringSKillName : MonoBehaviour
{
    public Text skillNameText;
    public Text SkillCylinderCountText;

    private void Start()
    {
        UIEventToGame.Instance.SkillName += PopSkillNameOnWindow;
        gameObject.SetActive(false);
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


}
