using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ColorZone
{
    float startPos;
    float endPos;

    public ColorZone(RectTransform tr)
    {
        float wid = tr.sizeDelta.x * 0.5f;
        startPos = tr.localPosition.x  - wid;
        endPos = tr.localPosition.x + wid;
    }
    
    public bool CheckInZone(float pos)
    {
        if (pos < endPos && pos > startPos)
            return true;
        return false;
    }
}

public enum COLORZONE
{
    NONE,GREEN,YELLOW,RED
}

public class SkillGauge : MonoBehaviour
{
    public RectTransform Green;
    public RectTransform Yellow;
    public RectTransform Red;

    private ColorZone greenZone;
    private ColorZone yellowZone;
    private ColorZone redZone;

    public RectTransform Ticktok;
    [SerializeField]
    float ticktokSpeed = 10f;
    float endPos;

    public COLORZONE zone;

    float followDistX = 42f;
    float followDistY = 27f;


    float MingWid = 10;
    float MinbWid = 20;
    float MinrWid = 30;
    float MaxgWid = 15;
    float MaxbWid = 25;
    float MaxrWid = 35;

    private void Start()
    {
        greenZone = new ColorZone(Green);
        yellowZone = new ColorZone(Yellow);
        redZone = new ColorZone(Red);

        endPos = GetComponent<RectTransform>().sizeDelta.x;
    }

    private void FollowPlayer(Vector2 pos)
    {
        pos.x -= followDistX;
        pos.y += followDistY;
        transform.position = pos;
    }

    Vector2 gSizedelta = new Vector3();
    Vector2 bSizedelta = new Vector3();
    Vector2 rSizedelta = new Vector3();
    private void OnEnable()
    {
        Ticktok.localPosition = Vector3.zero;
        zone = COLORZONE.NONE;

        //카운터 크기 설정
        float size = (GameData.Instance.player.counter_judgement > 10) ? 10 : GameData.Instance.player.counter_judgement;
        gSizedelta.Set(MingWid, 20);
        gSizedelta.x = Mathf.Clamp(gSizedelta.x + (((MaxgWid - MingWid) / 10) * size), MingWid, MaxgWid);
        bSizedelta.Set(MinbWid, 20);
        bSizedelta.x = Mathf.Clamp(bSizedelta.x + (((MaxbWid - MinbWid) / 10) * size), MinbWid, MaxbWid);
        rSizedelta.Set(MinrWid, 20);
        rSizedelta.x = Mathf.Clamp(rSizedelta.x + (((MaxrWid - MinrWid) / 10) * size), MinrWid, MaxrWid);

        //판정박스 크기
        Green.sizeDelta = gSizedelta;
        Yellow.sizeDelta = bSizedelta;
        Red.sizeDelta = rSizedelta;

        //위치들 설정
        Yellow.localPosition = new Vector3(Green.localPosition.x - Green.sizeDelta.x, Green.localPosition.y, Green.localPosition.z);
        Red.localPosition = new Vector3(Yellow.localPosition.x - Yellow.sizeDelta.x, Yellow.localPosition.y, Yellow.localPosition.z);
    }

    private void Update()
    {
        TicktokMove();
        CheckZone();
    }

    void TicktokMove()
    {
        Vector2 pos = Ticktok.localPosition;
        pos.x += ticktokSpeed * Time.deltaTime;
        Ticktok.localPosition = pos;
        if (pos.x > endPos)
        {
            gameObject.SetActive(false);
            UIEventToGame.Instance.OnPlayerDelay();
        }
    }

    void CheckZone()
    {
        float x = Ticktok.localPosition.x;
        if (redZone.CheckInZone(x))
            zone = COLORZONE.RED;
        else if (yellowZone.CheckInZone(x))
            zone = COLORZONE.YELLOW;
        else if (greenZone.CheckInZone(x))
            zone = COLORZONE.GREEN;
        else
            zone = COLORZONE.NONE;
    }
    
}
