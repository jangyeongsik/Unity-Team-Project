using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject BaseLever;       //뒷배경
    private Vector3 basePos;        
   // private RectTransform lever;
    //private RectTransform rectTransform;

    [SerializeReference, Range(10,150)]
    private float leverRange;

    private Vector3 inputDirection;
    private bool isInput;

    private void Awake()
    {
        //rectTransform = GetComponent<RectTransform>();
        basePos = BaseLever.transform.position;
       // lever.anchoredPosition = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //ControllJoystickLever(eventData);
        //isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControllJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       // lever.anchoredPosition = Vector2.zero;
        isInput = false;
        inputDirection = Vector2.zero;
    }

    private void ControllJoystickLever(PointerEventData eventData)
    {
        //var inputPos = eventData.position - rectTransform.anchoredPosition;
        //var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        // lever.anchoredPosition = inputVector;
        //inputDirection = inputVector / leverRange;

        inputDirection = (Vector3)eventData.position - basePos;
        inputDirection.Normalize();
        var dist = Vector3.Distance(basePos, eventData.position);
        if(dist < leverRange)
        {
            transform.position = basePos + inputDirection * dist;
        }
        else
        {
            transform.position = basePos + inputDirection * leverRange;
        }
    }

    public float Horizontal()
    {
        return inputDirection.x;
    }
    public float Vertical()
    {
        return inputDirection.y;
    }

}
