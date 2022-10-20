using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameLoader : MonoBehaviour
{

    [SerializeField]
    Image progressBar;
    [SerializeField]
    TextMeshProUGUI loadingText;
    [SerializeField]
    Image portrait;

    public Loader loader;
    List<List<object>> tipData;
    private void Awake()
    {
        LoadTipData();
    }
    void Start()
    {
        loader = new Loader(progressBar, true, loadingText);
        loader.AddLoadingTask(delegate {
            loader.LoadGameSceneAsync("GameScene");
        });
    }

    /// <summary>
    /// (임시 함수)
    /// </summary>
    public void LoadTipData()
    {
        tipData = CSVReader.Parsing("Data/TipText");
    }

}
