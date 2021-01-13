using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressLoading : MonoBehaviour
{
    public Slider progressbar;
    public Text loadText;

    private void Start()
    {
        //StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("UI Scene");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }
            else if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1.0f, Time.deltaTime);
            }
            if(progressbar.value >= 1f)
            {
                loadText.text = "터치하면 게임이 시작됩니다.";
            }

            if (Input.GetMouseButton(0) && progressbar.value >= 1.0f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
