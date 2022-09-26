using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Å¸ÀÌÆ² È­¸é µ¿ÀÛ ÃÑ°ý
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
        while(progress < 1f)
        {
            yield return null;

            TitleLoader.Instance.SetProgressBar(progress);
        }
        gameStartButton.SetActive(true);
    }
    IEnumerator StartPrework()
    {
        //TitleLoadingUIControler.Instance.SetProgressBar(0);
        progress = 0;

        yield return StartCoroutine(TitleLoader.Instance.GameUpdate());
        progress = 0.3f;


        yield return StartCoroutine(TitleLoader.Instance.SystemLoading());
        progress = 0.6f;

        TitleLoader.Instance.SetLoadingContent("SceneLoading...");
        yield return StartCoroutine(TitleLoader.Instance.LoadGameSceneAsync("Lobby",progress));
       

    }


    
}
