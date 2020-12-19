using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TicktokZone
{
    float startPos;
    float endPos;

    public TicktokZone(RectTransform tr)
    {
        startPos = tr.localPosition.x;
        endPos = startPos + tr.sizeDelta.x;
    }
    
    public bool CheckInZone(float pos)
    {
        if (pos < endPos && pos > startPos)
            return true;
        return false;
    }
}

public enum TICKTOKZONE
{
    NONE,GREEN,YELLOW,RED
}

public class TicktokGauge : MonoBehaviour
{
    public RectTransform Green;
    public RectTransform Yellow;
    public RectTransform Red;

    private TicktokZone greenZone;
    private TicktokZone yellowZone;
    private TicktokZone redZone;

    public RectTransform Ticktok;
    [SerializeField]
    float ticktokSpeed = 10f;
    float endPos;

    public TICKTOKZONE zone;

    private void Start()
    {
        greenZone = new TicktokZone(Green);
        yellowZone = new TicktokZone(Yellow);
        redZone = new TicktokZone(Red);

        endPos = GetComponent<RectTransform>().sizeDelta.x;
    }

    private void OnEnable()
    {
        Ticktok.localPosition = Vector3.zero;
        zone = TICKTOKZONE.NONE;
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
            gameObject.SetActive(false);
    }

    void CheckZone()
    {
        float x = Ticktok.localPosition.x;
        if (redZone.CheckInZone(x))
            zone = TICKTOKZONE.RED;
        else if (yellowZone.CheckInZone(x))
            zone = TICKTOKZONE.YELLOW;
        else if (greenZone.CheckInZone(x))
            zone = TICKTOKZONE.GREEN;
        else
            zone = TICKTOKZONE.NONE;
    }

}
