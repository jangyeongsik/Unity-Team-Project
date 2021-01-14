using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingProgress : MonoBehaviour
{
    static string nextScene;
    [SerializeField] Image progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        //씬 로딩이 끝나지 않은 상태라면~ 계속해서 반복하게 만들어준다.
        while (!op.isDone)      
         {
            //반복문이 한 번 반복할 때마다, 유니티 엔진에 제어권을 넘긴다. 
            yield return null;

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                //90%보다 커지면, 페이크 로딩을 진행. 
                timer += Time.unscaledDeltaTime;

                //progressBar.fillAmount 를 0.9에서 1로 1초에 걸쳐서 채우게 만든다.
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressBar.fillAmount >= 1.0f)
                {
                    ScreenFade.Instance.OnFadeIn(1.5f);
                    op.allowSceneActivation = true;
                    yield break;
                }  
            }
        }
    }
}
