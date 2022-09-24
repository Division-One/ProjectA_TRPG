using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;


public class TitleLoadingUIControler : LoadingUIControler
{
    private static TitleLoadingUIControler instance = null;
    public static TitleLoadingUIControler Instance
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
            instance = this;
        else
            Destroy(this.gameObject);

    }
    public void SetLoadingContent(string text)
    {
        loadingContent.text = text;
    }
    [SerializeField]
    TextMeshProUGUI loadingContent;
}
