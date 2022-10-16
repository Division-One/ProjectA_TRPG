using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;


public class TitleLoader : Loader
{
    private static TitleLoader instance = null;
    public static TitleLoader Instance
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

    /// <summary>
    /// 게임 업데이트.
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameUpdate()
    {
        SetLoadingContent("GameUpdate...");
        yield return new WaitForSeconds(2);
        SetProgressBar(0.3f);
    }
    public IEnumerator SystemLoading()
    {
        SetLoadingContent("SystemLoading...");
        yield return new WaitForSeconds(2);
        SetProgressBar(0.6f);
    }
}
