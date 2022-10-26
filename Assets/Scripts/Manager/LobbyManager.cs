using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    public enum LobbyCanvasType
    {
        Loading = 0,
        Lobby,
        Setup,
        CollectionBook,
        Store,
        Count
    }
    #region singletone
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
    #endregion singletone
    [SerializeField]
    List<GameObject> canvasList;
    [SerializeField]
    InGameLoader gameLoader;

    
    // Start is called before the first frame update
    void Start()
    {
        gameLoader.Initialize();
        SwitchCanvas(LobbyCanvasType.Lobby);

    }


    public void SwitchCanvas(LobbyCanvasType type)
    {
        for (int i = 0; i < (int)LobbyCanvasType.Count; i++)
            canvasList[i].SetActive(false);
        canvasList[(int)type].SetActive(true);
    }
    public void GameStartButton()
    {
        SwitchCanvas(LobbyCanvasType.Loading);
        gameLoader.loader.StartLoad();


    }
    public void SetupButton()
    {
        SwitchCanvas(LobbyCanvasType.Setup);
    }
    public void CollectionBookButton()
    {
        SwitchCanvas(LobbyCanvasType.CollectionBook);
    }
    public void StoreButton()
    {
        SwitchCanvas(LobbyCanvasType.Store);
    }
    public void ToLobby()
    {
        SwitchCanvas(LobbyCanvasType.Lobby);
    }
}
