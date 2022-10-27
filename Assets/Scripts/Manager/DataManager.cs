using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region singletone
    private static DataManager instance = null;
    public static DataManager Instance
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
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    #endregion singletone

    public EventData eventData = new EventData();
    public TipData tipData = new TipData();
    public ItemData itemData = new ItemData();
    public PlayerProperties playerProperty = new PlayerProperties();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateData()
    {
        eventData.GenerateData();
        itemData.GenerateData();
        tipData.GenerateData();
    }
    
}
