using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    int maxHeartSize = 10;
    Image[] hearts;

    int currentHeart;

    private void Awake()
    {
        hearts = new Image[maxHeartSize];
        int idx = 0;

        foreach(Transform child in transform)
        {
            Vector3 pos = Vector3.zero;
            pos.x = (idx % 5) * 50;
            pos.y = (idx / 5) * -50;
            child.localPosition = pos;
            hearts[idx] = child.GetComponent<Image>();
            hearts[idx].fillAmount = 0;
            ++idx;
            child.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        for (currentHeart = 0; currentHeart < GameData.Instance.player.hp && 
            currentHeart < hearts.Length; ++currentHeart)
        {
            hearts[currentHeart].gameObject.SetActive(true);
            hearts[currentHeart].fillAmount = 1;
        }
        currentHeart--;

        GameEventToUI.Instance.playerHP_Decrease += CutHeart;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            AddHeart(true);
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.playerHP_Decrease -= CutHeart;
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

    public void AddHeart(bool isHalf)
    {
        int i = isHalf ? 1 : 0;
        for(int j = i; j < 2; j++)
        {
            if (hearts[currentHeart].fillAmount == 1 && currentHeart < maxHeartSize -1)
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
