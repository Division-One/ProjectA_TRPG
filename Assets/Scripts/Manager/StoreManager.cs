using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class StoreManager : MonoBehaviour
{
    #region singletone
    private static StoreManager instance = null;
    public static StoreManager Instance
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
        }
        else
            Destroy(this.gameObject);
    }
    #endregion singletone
    [SerializeField]
    Store store;
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    int storeItemCount;
    // Start is called before the first frame update
    void Start()
    {
        if(storeItemCount > ItemData.Instance.itemCount) storeItemCount = ItemData.Instance.itemCount;

        List<int> pool = new List<int>();
        Constants.CreateUnDuplicateRandom(pool, 0, ItemData.Instance.itemCount, storeItemCount);
        for (int i = 0; i < storeItemCount; i++)
        {
            store.Stock(ItemData.Instance.items[pool[i]]);
        }
    }

}
