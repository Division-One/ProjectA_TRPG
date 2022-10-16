using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SwitchCanvas(LobbyCanvasType.Lobby);
    }

    [SerializeField]
    List<GameObject> canvasList;
    public void SwitchCanvas(LobbyCanvasType type)
    {
        for (int i = 0; i < (int)LobbyCanvasType.Count; i++)
            canvasList[i].SetActive(false);
        canvasList[(int)type].SetActive(true);
    }
    public void GameStartButton()
    {
        SwitchCanvas(LobbyCanvasType.Loading);
        StartCoroutine(InGameLoader.Instance.UpdateTipContent(4));
        StartCoroutine(InGameLoader.Instance.LoadGameSceneAsync("GameScene"));
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
