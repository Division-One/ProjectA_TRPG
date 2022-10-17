using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public enum CollectionType
{
    Ending,
    Item,
    Monster,
    Achievement,
    Count
}
public class CollectionBookManager : MonoBehaviour
{
    #region singletone
    private static CollectionBookManager instance = null;
    public static CollectionBookManager Instance
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
        {
            instance = this;

            Initiate();
        }
        else
            Destroy(this.gameObject);
    }
    #endregion singletone
    [SerializeField]
    CollectionUIObject detailInfoView;
    [SerializeField]
    TabGroup tabView;
    [SerializeField]
    Button toPrevButton;
    [SerializeField]
    Button toNextButton;
    [SerializeField]
    TextButton goBackButton;

    List<CollectionBookTab > pages =  new List<CollectionBookTab>();

    CollectionBookTab currentSelectedTab;
    public List<List<CollectionData>> dataList = new List<List<CollectionData>>((int)CollectionType.Count);

    private void Start()
    {
        ToTabView();
    }
    public void Initiate()
    {
        GenerateCollectionData();
        tabView.Initiate();
        foreach (var tabButton in tabView.tabButtons)
        {
            tabButton.onTabSelected.AddListener(delegate {
                SetPrevNextButtonTabView(tabView.objectsToSwap[tabButton.myIndex].GetComponent<CollectionBookTab>());
            });
        }

    }
    public void ToDetailInfoView()
    {
        goBackButton.onClick.RemoveAllListeners();
        goBackButton.onClick.AddListener(ToTabView);

        SetPrevNextButtonDetailView();

        detailInfoView.gameObject.SetActive(true);
        tabView.gameObject.SetActive(false);
    }
    public void ToTabView()
    {
        goBackButton.onClick.RemoveAllListeners();
        goBackButton.onClick.AddListener(LobbyManager.Instance.ToLobby);

        currentSelectedTab = tabView.objectsToSwap[tabView.selectedTabIndex].GetComponent<CollectionBookTab>();
        SetPrevNextButtonTabView(currentSelectedTab);

        detailInfoView.gameObject.SetActive(false);
        tabView.gameObject.SetActive(true);
    }
    public void SetPrevNextButtonTabView(CollectionBookTab tab)
    {
        toPrevButton.onClick.RemoveAllListeners();
        toNextButton.onClick.RemoveAllListeners();
        toPrevButton.onClick.AddListener(tab.ToPrevPage);
        toNextButton.onClick.AddListener(tab.ToNextPage);
    }
    public void SetPrevNextButtonDetailView()
    {
        toPrevButton.onClick.RemoveAllListeners();
        toNextButton.onClick.RemoveAllListeners();
        toPrevButton.onClick.AddListener(detailInfoView.ToPrevCollection);
        toNextButton.onClick.AddListener(detailInfoView.ToNextCollection);
    }
    public void OnCollectionButtonSelected(CollectionData data)
    {
        detailInfoView.Display(data);

        ToDetailInfoView();
    }
    void GenerateCollectionData()
    {
        //List<List<object>> mainEventData = CSVReader.Parsing("Data/MainEventData");

        //for (int i = 0; i < mainEventData.Count; i++)
        //{
        //}
        for (int i = 0; i < (int)CollectionType.Count; i++)
        {
            dataList.Add(new List<CollectionData>());
            for (int j = 0; j < 23; j++)
            {
                CollectionData data = new CollectionData();
                data.collectionName = i + "," + j;
                data.collectionType = (CollectionType)i;
                data.collectionInfo = i + "," + j + "ÀÔ´Ï´Ù";
                data.id = j;
                dataList[i].Add(data);
            }
        }
       
    }
}
public class CollectionData 
{

    public Sprite collectionImage;
    public string collectionName;
    public CollectionType collectionType;
    public string collectionInfo;
    public int id;

}
