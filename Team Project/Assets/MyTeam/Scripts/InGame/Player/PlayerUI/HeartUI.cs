using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    int maxHeartSize = 10;
    Image[] hearts;
    Image[] disabled;

    int currentHeart;

    private void Awake()
    {
        hearts = transform.Find("Active").GetComponentsInChildren<Image>();
        disabled = transform.Find("Disabled").GetComponentsInChildren<Image>();

        for(int i = 0; i < hearts.Length; ++i)
        {
            Vector3 pos = Vector3.zero;
            pos.x = (i % 5) * 50;
            pos.y = (i / 5) * -50;
            hearts[i].transform.localPosition = pos;
            disabled[i].transform.localPosition = pos;
            hearts[i].fillAmount = 0;
            hearts[i].gameObject.SetActive(false);
            disabled[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        for (currentHeart = 0; currentHeart < GameData.Instance.player.hp; ++currentHeart)
        {
            hearts[currentHeart].gameObject.SetActive(true);
            hearts[currentHeart].fillAmount = 1;
            disabled[currentHeart].gameObject.SetActive(true);
        }
        currentHeart--;

        GameEventToUI.Instance.playerHP_Decrease += CutHeart;
        GameEventToUI.Instance.playerHP_Increase += AddHeart;
    }

    private void Update()
    {
        GameData.Instance.player.currentHp = hearts[currentHeart].fillAmount + currentHeart;
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.playerHP_Decrease -= CutHeart;
        GameEventToUI.Instance.playerHP_Increase -= AddHeart;
    }

    public void CutHeart(int damage)
    {
        for(int i = 0; i < damage; ++i)
        {
            if (hearts[currentHeart].fillAmount <= 0 && currentHeart > 0)
            {
                currentHeart--;
            }
            if (currentHeart == 0 && hearts[currentHeart].fillAmount == 0)
            {
                currentHeart = 0;
                return;
            }
            hearts[currentHeart].fillAmount -= 0.5f;
        }
    }

    public void AddHeart(int value, int per)
    {
        if (Random.Range(0, 100) > per) return;
        for(int j = 0; j < value; j++)
        {
            if (hearts[currentHeart].fillAmount == 1 && currentHeart < GameData.Instance.player.hp -1)
            {
                SetNewHeart(++currentHeart, 0.5f);
            }
            else
            {
                hearts[currentHeart].fillAmount += 0.5f;
            }
        }
    }

    void SetNewHeart(int idx, float amount = 1)
    {
        hearts[idx].gameObject.SetActive(true);
        hearts[idx].fillAmount = amount;
    }
}
