using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private static LobbyManager instance = null;
    public static LobbyManager Instance
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
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField]
    GameObject inGameUI;
    [SerializeField]
    GameObject lobbyCanvas;

    public void GameStartButton()
    {
        lobbyCanvas.SetActive(false);
        StartCoroutine(InGameLoader.Instance.LoadGameSceneAsync("GameScene"));
    }
   
}
