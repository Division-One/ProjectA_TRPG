using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class Loader 
{
    public delegate void LoadTask();
    event LoadTask taskEvent;
    Image progressBar;
    TextMeshProUGUI loadingText;
    bool autoSceneChange;

    int taskCount;
    int completedTaskCount = 0;
    bool separateLoad = false;
    List<string> loadingTextStrings;
    public AsyncOperation sceneLoadingOp;

    public Loader(Image progressBar, bool autoSceneChange, TextMeshProUGUI loadingText = null)
    {
        this.progressBar = progressBar;
        this.autoSceneChange = autoSceneChange;
        this.loadingText = loadingText;
        if (loadingText != null)
        {
            loadingTextStrings = new List<string>();
        }
    }
    public void AddLoadingTask(LoadTask task, string loadingText = null)
    {
        taskCount++;
        if(loadingText != null)
            loadingTextStrings.Add(loadingText);
        taskEvent += BeforeTask;
        taskEvent += task;
        taskEvent+= AfterTask;
    }
    public void StartLoad()
    {
        if(taskEvent != null)
            taskEvent.Invoke();
    }
    /// <summary>
    /// 임시함수
    /// </summary>
    /// <param name="loadingTexts"></param>
    public void SeparatedLoading(List<string> loadingTexts)
    {
        separateLoad = true;
        loadingTextStrings = loadingTexts;
    }
    public float GetProgress()
    {
        return progressBar.fillAmount;
    }
    void BeforeTask()
    {
        if (loadingText != null)
            if(!separateLoad && completedTaskCount < loadingTextStrings.Count)
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
    /// 씬을 Load
    /// </summary>
    /// <param name="sceneName">Load할 씬의 이름</param>
    /// <returns></returns>
    public void LoadGameSceneAsync(string sceneName)
    {
        Debug.Log("LoadGameSceneAsync called, " + sceneName);
        sceneLoadingOp= SceneManager.LoadSceneAsync(sceneName);
        if (!autoSceneChange)
        {
            sceneLoadingOp.allowSceneActivation = false;
        }

    }
}



