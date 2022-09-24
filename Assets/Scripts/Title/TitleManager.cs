using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 타이틀 화면 동작 총괄
/// </summary>
public class TitleManager : MonoBehaviour
{
    private static TitleManager instance = null;
    public static TitleManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    [SerializeField]
    GameObject gameStartButton;
    float progress = 0;
    AsyncOperation op;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        gameStartButton.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateProgressBar());
        StartCoroutine( StartPrework());
    }
    public void GameStartButton()
    {
        op.allowSceneActivation = true;
    }
    IEnumerator UpdateProgressBar()
    {
        while(progress < 100)
        {
            yield return null;

            TitleLoadingUIControler.Instance.SetProgressBar(progress);
        }
        gameStartButton.SetActive(true);
    }
    IEnumerator StartPrework()
    {
        TitleLoadingUIControler.Instance.SetProgressBar(0);
        progress = 0;
        TitleLoadingUIControler.Instance.SetLoadingContent("GameUpdate...");
        yield return StartCoroutine(GameUpdate());
        progress = 30;
        TitleLoadingUIControler.Instance.SetLoadingContent("SystemLoading...");
        yield return StartCoroutine(SystemLoading());
        progress = 60;
        TitleLoadingUIControler.Instance.SetLoadingContent("SceneLoading...");
        float timer = 0f;
        float remainProgress = 100 - progress;
        op = SceneManager.LoadSceneAsync("GameScene");
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 0.9f)
            {
                progress += op.progress * remainProgress / 100;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progress = Mathf.Lerp(progress, 100, timer);
                if (progress >= 100)
                {
                    TitleLoadingUIControler.Instance.SetLoadingContent("Load Complete");
                    yield break;
                }
            }
        }

    }


    /// <summary>
    /// 게임 업데이트.
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameUpdate()
    {
        Debug.Log("GameUpdate");
        yield return new WaitForSeconds(2);
    }
    public IEnumerator SystemLoading()
    {
        Debug.Log("SystemLoading");
        yield return new WaitForSeconds(2);
    }
}
