using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TempleKey : MonoBehaviour
{
    public int keyCount;
    public Text keyCountText;
    bool isRunning;
    private void Start()
    {
        keyCount = GameData.Instance.player.keyCounter;
        keyCountText.text = keyCount.ToString();
        isRunning = false;
    }
    private void Update()
    {
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(KeyCountCoroutine());
        }
    }
    IEnumerator KeyCountCoroutine()
    {
        if (keyCount != GameData.Instance.player.keyCounter)
        {
            keyCount = GameData.Instance.player.keyCounter;
            keyCountText.text = keyCount.ToString();
        }
        yield return new WaitForSecondsRealtime(0.5f);
        isRunning = false;
    }
}
