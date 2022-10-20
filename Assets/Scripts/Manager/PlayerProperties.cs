using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    #region singletone
    private static PlayerProperties instance = null;
    public static PlayerProperties Instance
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
