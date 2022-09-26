using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField]
    Image progressBar;

    public void SetProgressBar(float progress)
    {
        progressBar.fillAmount = progress;
    }
    /// <summary>
    /// 씬을 Load하면서 progressBar의 진행도를 함께 조작
    /// </summary>
    /// <param name="sceneName">Load할 씬의 이름</param>
    /// <param name="startProgress">씬 Load 시작 시의 진행도</param>
    /// <param name="fakeLoadStartProgress">fakeLoading을 시작할 진행도</param>
    /// <returns></returns>
    public IEnumerator LoadGameSceneAsync(string sceneName, float startProgress = 0f, float fakeLoadStartProgress=0.9f)
    {

        progressBar.fillAmount = startProgress;
        float timer = 0f;
        float remainProgress = 1 - startProgress;
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;
            if (op.progress < fakeLoadStartProgress)
            {
                progressBar.fillAmount = startProgress + op.progress*remainProgress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
}



