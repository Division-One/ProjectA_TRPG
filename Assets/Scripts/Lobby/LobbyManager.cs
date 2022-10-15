using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public enum CanvasType
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
        SwitchCanvas(CanvasType.Lobby);
    }

    [SerializeField]
    List<GameObject> canvasList;
    public void SwitchCanvas(CanvasType type)
    {
        for (int i = 0; i < (int)CanvasType.Count; i++)
            canvasList[i].SetActive(false);
        canvasList[(int)type].SetActive(true);
    }
    public void GameStartButton()
    {
        SwitchCanvas(CanvasType.Loading);
        StartCoroutine(InGameLoader.Instance.UpdateTipContent(4));
        StartCoroutine(InGameLoader.Instance.LoadGameSceneAsync("GameScene"));
    }
    public void SetupButton()
    {
        SwitchCanvas(CanvasType.Setup);
    }
    public void CollectionBookButton()
    {
        SwitchCanvas(CanvasType.CollectionBook);
    }
    public void StoreButton()
    {
        SwitchCanvas(CanvasType.Store);
    }
}
