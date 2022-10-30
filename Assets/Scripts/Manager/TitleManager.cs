using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
/// <summary>
/// Å¸ÀÌÆ² È­¸é µ¿ÀÛ ÃÑ°ý
/// </summary>
public class TitleManager : MonoBehaviour
{
    #region singletone
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
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initiate();
        }

        else
            Destroy(this.gameObject);
    }
    #endregion singletone

    [SerializeField]
    GameObject gameStartButton;
    [SerializeField]
    Image progressBar;
    [SerializeField]
    TextMeshProUGUI loadingContent;
    [SerializeField]
    Loader loader;

    public void Initiate()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        loader.Initialize(progressBar, false, loadingContent);
        loader.AddLoadingTask(GenerateData,"DataLoading");
        loader.AddLoadingTask(GameUpdate,"GameUpdate");
        loader.AddLoadingTask(SystemLoading, "SystemLoading");
        loader.AddLoadingTask(delegate {
            loader.LoadGameSceneAsync("Lobby");
        },"SceneLoading");
        loader.OnLoadCompleted += delegate { gameStartButton.gameObject.SetActive(true); };
        StartCoroutine( loader.StartLoad());
    }
    public void GameStartButton()
    {
        loader.sceneLoadingOp.allowSceneActivation = true;
    }
    public void GenerateData()
    {
        loadingContent.text = "DataLoading...";
        DataManager.Instance.GenerateData();
    }
    public void GameUpdate()
    {
        loadingContent.text = "GameUpdate...";
        Debug.Log("GameUpdate called");
    }
    public void SystemLoading()
    {
        loadingContent.text = "SystemLoading...";
        Debug.Log("SystemLoading called");

    }
    //IEnumerator StartPrework()
    //{
    //    //TitleLoadingUIControler.Instance.SetProgressBar(0);
    //    progress = 0;

    //    yield return StartCoroutine(TitleLoader.Instance.GameUpdate());
    //    progress = 0.3f;


    //    yield return StartCoroutine(TitleLoader.Instance.SystemLoading());
    //    progress = 0.6f;

    //    TitleLoader.Instance.SetLoadingContent("SceneLoading...");
    //    yield return StartCoroutine(TitleLoader.Instance.LoadGameSceneAsync("Lobby", progress));


    //}



}
