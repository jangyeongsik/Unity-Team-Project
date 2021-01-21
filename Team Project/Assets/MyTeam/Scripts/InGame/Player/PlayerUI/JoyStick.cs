using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform lever;
    RectTransform back;

    Vector3 direction;
    Vector3 center;
    bool isDrag;

    float maxDistance = 100f;
    float amount = 0f;

    void Start()
    {
        lever = transform.Find("Lever").GetComponent<RectTransform>();
        back = transform.Find("Background").GetComponent<RectTransform>();
        center = back.position;
        UIEventToGame.Instance.JoystickSetting += Joystick_Center_Setting;
    }

    private void OnDestroy()
    {
        UIEventToGame.Instance.JoystickSetting -= Joystick_Center_Setting;
    }
    void Update()
    {
        UIEventToGame.Instance.OnPlayerMove(direction, amount);
    }

    //드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        UpdateJoystick(eventData);
    }

    //드래그중
    public void OnDrag(PointerEventData eventData)
    {
        UpdateJoystick(eventData);
    }

    //드래그끝
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.position = center;
        direction = Vector3.zero;
        amount = 0f;
    }

    public void Joystick_Center_Setting()
    {
        lever.position = center;
        direction = Vector3.zero;
        amount = 0f;
    }

    void UpdateJoystick(PointerEventData eventData = null)
    {
        if (eventData != null)
            lever.position = eventData.position;

        direction = (Vector3)eventData.position - center;
        float distance = direction.magnitude;
        direction.Normalize();

        if (distance > maxDistance)
        {
            lever.position = center + direction * maxDistance;
            distance = maxDistance;
        }

        amount = distance / maxDistance;
    }
}
