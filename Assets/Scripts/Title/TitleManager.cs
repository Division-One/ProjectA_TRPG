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
    private PreWorker preWorker;
    public GameObject startText;
    public bool isPreworkDone = false;

    private void Awake()
    {
        preWorker = GetComponent<PreWorker>();
        startText.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        isPreworkDone = preWorker.PreWork();
        if(isPreworkDone)
        {
            startText.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPreworkDone)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene("GameScene");
            }
        }

    }

}
