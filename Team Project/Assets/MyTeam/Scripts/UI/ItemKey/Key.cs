using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{
    TextMeshProUGUI resourceText;
    private int resource;

    void Start()
    {
        resourceText = GetComponent<TextMeshProUGUI>();
        resource = 0;
        resourceText.text = resource.ToString();
        GameEventToUI.Instance.keyCount += OnGetTempleKeys;
    }

    public void OnGetTempleKeys(int addition)
    {
        resource += addition;
        resourceText.text = resource.ToString();
    }

    public void OnLoseTempleKeys(int addition)
    {
        resource -= addition;
        resourceText.text = resource.ToString();
    }

    private void OnDestroy()
    {
        GameEventToUI.Instance.keyCount -= OnGetTempleKeys;
    }
}
