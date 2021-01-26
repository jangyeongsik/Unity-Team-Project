using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyMentControll : MonoBehaviour
{
    public float mentSpeed;         //멘트박스 위로 올라가는 속도 
    public float alphaSpeed;        //투명해지는 속도 
    TextMeshPro mentBox;
    Color alpha;
    public GameObject obj;         //아이템 습득 로그창.  

    bool isOpen = false;

    void Start()
    {
        mentBox = GetComponent<TextMeshPro>();
        obj.SetActive(false);
        GameEventToUI.Instance.isGet += OnOpenMentBox;
    }

    public void OnOpenMentBox()
    {
        obj.SetActive(true);
        StartCoroutine(ShowItemLogBox());
    }

    IEnumerator ShowItemLogBox()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        obj.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.isGet -= OnOpenMentBox;
    }

}
