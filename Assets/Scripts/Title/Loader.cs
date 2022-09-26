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
    /// ���� Load�ϸ鼭 progressBar�� ���൵�� �Բ� ����
    /// </summary>
    /// <param name="sceneName">Load�� ���� �̸�</param>
    /// <param name="startProgress">�� Load ���� ���� ���൵</param>
    /// <param name="fakeLoadStartProgress">fakeLoading�� ������ ���൵</param>
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



