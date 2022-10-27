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
        loader = new Loader(progressBar, true, loadingText);
        loader.AddLoadingTask(delegate {
            loader.LoadGameSceneAsync("GameScene");
        });
    }

}
