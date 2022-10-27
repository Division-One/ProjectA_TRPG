using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
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
        ContentsControler.Instance.SetContentsPanel(DataManager.Instance.eventData.GetCurrentEvent().GetCurretBlock());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnOptionSelected(int connect, bool isEndEvent)
    {
        Debug.Log("OnOptionSElected, " + connect + " " + isEndEvent);
        if(isEndEvent == true)
        {
            if (connect == -1)
            {
                GameEnd();
                return;
            }
            DataManager.Instance.eventData.ToNextEvent(connect);
            ContentsControler.Instance.SetContentsPanel(DataManager.Instance.eventData.GetCurrentEvent().GetCurretBlock());
        }
        else
        {
            DataManager.Instance.eventData.ToNextBlock(connect);
            ContentsControler.Instance.AppendBlock(DataManager.Instance.eventData.GetCurrentEvent().GetCurretBlock());
        }
    }
    public void GameEnd()
    {
        Debug.Log("Game ENd");
    }
}
