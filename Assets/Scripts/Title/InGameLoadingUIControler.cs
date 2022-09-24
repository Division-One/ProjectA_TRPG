using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;


public class InGameLoadingUIControler : LoadingUIControler
{
    private static InGameLoadingUIControler instance = null;
    public static InGameLoadingUIControler Instance
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
        this.gameObject.SetActive(false);

    }

    [SerializeField]
    TextMeshProUGUI tipContent;
    [SerializeField]
    Image portrait;
}
