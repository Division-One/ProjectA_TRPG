using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;


public class InGameLoader : Loader
{
    private static InGameLoader instance = null;
    public static InGameLoader Instance
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

    [SerializeField]
    TextMeshProUGUI tipContent;
    [SerializeField]
    Image portrait;

}
