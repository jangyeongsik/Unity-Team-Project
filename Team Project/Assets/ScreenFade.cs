using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : SingletonMonobehaviour<ScreenFade>
{
    Image image;
    float time;
    float F_Time;

    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        image.gameObject.SetActive(false);
    }

    public void OnFadeIn(float T)
    {
        StartCoroutine(Fade_In(T));
    }

    IEnumerator Fade_In(float T)
    {
        image.gameObject.SetActive(true);
        Color alpha = image.color;
        alpha.a = 1;
        image.color = alpha;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / T;
            alpha.a = Mathf.Lerp(0, 1, time);
            image.color = alpha;
            yield return null;
        }
        time = 0;
        yield return new WaitForSeconds(0.5f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / T;
            alpha.a = Mathf.Lerp(1, 0, time);
            image.color = alpha;
            yield return null;
        }
        time = 0;
        image.gameObject.SetActive(false);
        yield return null;
    }

   
}
