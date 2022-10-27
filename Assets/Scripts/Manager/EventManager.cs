using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    #region singletone
    private static EventManager instance = null;

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }
    // Start is called before the first frame update
    #endregion singletone


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
