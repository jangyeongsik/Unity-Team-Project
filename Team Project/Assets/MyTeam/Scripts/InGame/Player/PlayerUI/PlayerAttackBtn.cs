using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttackBtn : MonoBehaviour
{
    [SerializeField]
    SkillGauge skillGauge;
    // Start is called before the first frame update
    void Start()
    {
        GameEventToUI.Instance.skillGaugeActive += SkillGaugeActive;
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.skillGaugeActive -= SkillGaugeActive;
    }

    public void OnPointerDown()
    {
        UIEventToGame.Instance.OnPlayerAttack(GameData.Instance.player.counterTime, skillGauge.zone);
    }

    void SkillGaugeActive(bool isOn)
    {
        skillGauge.gameObject.SetActive(isOn);
    }
}
