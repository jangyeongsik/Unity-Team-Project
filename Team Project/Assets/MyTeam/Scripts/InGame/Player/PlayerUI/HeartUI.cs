using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    Image[] hearts;
    Image[] disabled;

    int currentHeart;

    public GameObject healEffect;
    Image LessHp;
    Image Hit;
    GameObject gameOver;

    private void Awake()
    {
        hearts = transform.Find("Active").GetComponentsInChildren<Image>();
        disabled = transform.Find("Disabled").GetComponentsInChildren<Image>();
        LessHp = GameObject.Find("Less Hp").GetComponent<Image>();
        LessHp.gameObject.SetActive(false);
        Hit = GameObject.Find("Hit").GetComponent<Image>();
        Hit.gameObject.SetActive(false);
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);

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
        for (int i = 0; i < GameData.Instance.player.hp; ++i)
        {
            //hearts[currentHeart].gameObject.SetActive(true);
            //hearts[currentHeart].fillAmount = 1;
            disabled[i].gameObject.SetActive(true);
        }

        AddHeart((int)(GameData.Instance.player.currentHp * 2), 100);

        GameEventToUI.Instance.playerHP_Decrease += CutHeart;
        GameEventToUI.Instance.playerHP_Increase += AddHeart;
        GameEventToUI.Instance.AddMaxHp += AddMaxHp;
        GameEventToUI.Instance.gameover += OnGameOver;
    }

    private void Update()
    {
        GameData.Instance.player.currentHp = hearts[currentHeart].fillAmount + currentHeart;
        if (LessHp.gameObject.activeSelf && GameData.Instance.player.currentHp > 1)
            LessHp.gameObject.SetActive(false);
        else if (!LessHp.gameObject.activeSelf && GameData.Instance.player.currentHp <= 1)
            LessHp.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.P))
            AddHeart(1, 30);
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.playerHP_Decrease -= CutHeart;
        GameEventToUI.Instance.playerHP_Increase -= AddHeart;
        GameEventToUI.Instance.AddMaxHp -= AddMaxHp;
        GameEventToUI.Instance.gameover -= OnGameOver;
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

        if (!LessHp.gameObject.activeSelf)
            StartCoroutine(HitActive());
    }

    public void AddHeart(int value, int per)
    {
        if (Random.Range(0, 100) > per) return;
        if(per != 100)
        {
            GameObject obj = Instantiate(healEffect) as GameObject;
            obj.transform.position = GameData.Instance.player.position.position + Vector3.up;
            Destroy(obj, 1);
        }
        for(int j = 0; j < value; j++)
        {
            if (hearts[currentHeart].fillAmount == 1 && currentHeart < GameData.Instance.player.hp -1)
            {
                SetNewHeart(++currentHeart, 0.5f);
            }
            else
            {
                if (!hearts[currentHeart].gameObject.activeSelf)
                    hearts[currentHeart].gameObject.SetActive(true);
                hearts[currentHeart].fillAmount += 0.5f;
            }
        }

    }

    void SetNewHeart(int idx, float amount = 1)
    {
        disabled[idx].gameObject.SetActive(true);
        hearts[idx].gameObject.SetActive(true);
        hearts[idx].fillAmount = amount;
    }

    void AddMaxHp(int hp)
    {
        for(int i = 0; i < hp; ++i)
        {
            GameData.Instance.player.hp++;
            SetNewHeart(GameData.Instance.player.hp - 1,0);
            AddHeart(2, 100);
        }
    }

    IEnumerator HitActive()
    {
        Hit.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Hit.gameObject.SetActive(false);
    }

    void OnGameOver()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        gameOver.SetActive(true);
        GameData.Instance.player.PlayerGameOver();
        yield return new WaitForSeconds(2);
        gameOver.SetActive(false);
        
    }
}
