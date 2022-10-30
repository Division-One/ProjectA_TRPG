using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InGameLoader : MonoBehaviour
{

    [SerializeField]
    Image progressBar;
    [SerializeField]
    TextMeshProUGUI loadingText;
    [SerializeField]
    Image portrait;

    public Loader loader;
 
    private void Awake()
    {


    }
    void Start()
    {

    }
    public void Initialize()
    {
        loader.Initialize(progressBar, true,loadingText);
        List<int> pool = new List<int>();
        Constants.CreateUnDuplicateRandom(pool,0, DataManager.Instance.tipData.tipData.Count-1,3);
        
        loader.AddLoadingTask(delegate {
            loader.LoadGameSceneAsync("GameScene");
        }, DataManager.Instance.tipData.tipData[pool[0]]);

    }

}
