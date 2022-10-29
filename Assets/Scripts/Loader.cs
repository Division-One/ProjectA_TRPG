using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class Loader :MonoBehaviour
{
    public AsyncOperation sceneLoadingOp;
    public delegate void task();
    List<task> loadTasks;
    public task OnLoadCompleted;
    Image progressBar;
    TextMeshProUGUI loadingText;
    bool autoSceneChange;
    int taskCount;
    int completedTaskCount = 0;
    List<string> loadingTextStrings;

    /// <summary>
    /// Loader.AddLoadingTask�� Task���� �߰�.
    /// </summary>
    /// <param name="progressBar"></param>
    /// <param name="autoSceneChange">false�� ��� SceneLoading �� �ٷ� ��ȯ���� ����</param>
    /// <param name="loadingText"></param>
    public void Initialize(Image progressBar, bool autoSceneChange, TextMeshProUGUI loadingText = null)
    {
        loadTasks = new List<task>();
        this.progressBar = progressBar;
        this.autoSceneChange = autoSceneChange;
        this.loadingText = loadingText;
        if (loadingText != null)
        {
            loadingTextStrings = new List<string>();
        }

        SetProgressBar(0);
    }
    public float GetProgress()
    {
        return progressBar.fillAmount;
    }
    public void AddLoadingTask(task task, string loadingText = null)
    {
        taskCount++;
        if(loadingText != null)
            loadingTextStrings.Add(loadingText);
        loadTasks.Add(task);
    }
    /// <summary>
    /// �ݵ�� StartCoroutine�� �̿��� ȣ���ؾ���.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartLoad()
    {
        if (loadTasks.Count == 0)
            yield break;
        taskCount++;
        foreach (var func in loadTasks)
        {
            BeforeTask();
            func.Invoke();
            AfterTask();
        }

        loadingText.text = loadingTextStrings[loadingTextStrings.Count - 1];
        yield return StartCoroutine(FakeLoad(2f));
        AfterTask();

        if(OnLoadCompleted != null)
            OnLoadCompleted.Invoke();
    
    yield break;
    }


    void BeforeTask()
    {
        if (loadingText != null)
            if(completedTaskCount < loadingTextStrings.Count)
                loadingText.text = loadingTextStrings[completedTaskCount];
    }
    void AfterTask()
    {
        completedTaskCount++;
        SetProgressBar((float)completedTaskCount / (float)taskCount);
    }
    void SetProgressBar(float progress)
    {
        progressBar.fillAmount = progress;
    }


    /// <summary>
    /// ���� Load. 
    /// </summary>
    /// <param name="sceneName">Load�� ���� �̸�</param>
    /// <returns></returns>
    public void LoadGameSceneAsync(string sceneName)
    {
        Debug.Log("LoadGameSceneAsync called, " + sceneName);
        sceneLoadingOp= SceneManager.LoadSceneAsync(sceneName);
        sceneLoadingOp.allowSceneActivation = false;
    }
    IEnumerator FakeLoad(float time)
    {
        yield return new WaitForSeconds(time);
        if(autoSceneChange)
            sceneLoadingOp.allowSceneActivation = true;
    }
}



