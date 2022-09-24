using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingUIControler : MonoBehaviour
{
    [SerializeField]
    Image progressBar;

    public void SetProgressBar(float progress)
    {
        progressBar.fillAmount = progress/100;
    }
}



