using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    #region singletone
    private static EventManager instance = null;
    Dictionary<int,GameEvent> events = new Dictionary<int,GameEvent>();
    int currentEventID = 0;
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


    GameEvent AddEvent(int eventID, EventType eventType)
    {
        if (events.ContainsKey(eventID)) return null;
        events[eventID] = new GameEvent(eventID, eventType);
        return events[eventID];
    }
    EventBlock AddEventBlock(int eventID, int blockID)
    {
        if (!events.ContainsKey(eventID)) return null;
        return events[eventID].AddEventBlock(blockID);
    }
    bool AddEventContent(int eventID, int blockID, EventContent c)
    {
        if (!events.ContainsKey(eventID)) return false;
        if (!events[eventID].ContainsBlock(blockID)) return false;
        events[eventID].GetEventBlock(blockID).AddEventContent(c);

        return true;
    }
    bool AddEventOption(int eventID, int blockID, EventOption o)
    {
        if (!events.ContainsKey(eventID)) return false;
        if (!events[eventID].ContainsBlock(blockID)) return false;
        events[eventID].GetEventBlock(blockID).AddEventOption(o);

        return true;
    }
    /// <summary>
    /// CSV���� �о� �̺�Ʈ ������ ����
    /// </summary>
    public void GenerateEventData()
    {
        List<List<object>> mainEventData = CSVReader.Parsing("Data/MainEventData");

        for (int i = 0; i < mainEventData.Count; i++)
        {
            int eventNumber = Int32.Parse(mainEventData[i][Constants.CSV_EVENT_ID_IDX].ToString());
            int blockNumber = Int32.Parse(mainEventData[i][Constants.CSV_EVENT_BLOCK_IDX].ToString());
            EventElementType elemType = (EventElementType)Int32.Parse(mainEventData[i][Constants.CSV_EVENT_ELEMTYPE_IDX].ToString());
            string content = mainEventData[i][Constants.CSV_EVENT_CONTENT_IDX].ToString();

            AddEvent(eventNumber, EventType.MAIN_EVENT);
            AddEventBlock(eventNumber, blockNumber);
            if (elemType == EventElementType.TEXT)
            {
                AddEventContent(eventNumber, blockNumber, new EventContentText(content));
            }
            else if (elemType == EventElementType.IMAGE)
            {
                AddEventContent(eventNumber, blockNumber, new EventContentImage("Sprites/" + content));
            }
            else if (elemType == EventElementType.OPTION)
            {
                int connectedNumber = Int32.Parse(mainEventData[i][Constants.CSV_EVENT_CONNNUMBER_IDX].ToString());
                string tmpEndOp = mainEventData[i][Constants.CSV_EVENT_ISENDOP_IDX].ToString();
                bool endOption = tmpEndOp == "" ? false : Convert.ToBoolean(Int32.Parse(tmpEndOp));
                AddEventOption(eventNumber, blockNumber, new EventOption(content, connectedNumber, endOption));
            }
            else
            {

            }

        }
    }
    /// <summary>
    /// ���� event�� eventID�� �ش��ϴ� event�� ��ȯ
    /// </summary>
    /// <param name="eventID">��ȯ�� event�� ID</param>
    public void ToNextEvent(int eventID)
    {
        currentEventID = eventID; 
    }
    /// <summary>
    /// ���� event Block�� blockID�� �ش��ϴ� event Block���� ��ȯ
    /// </summary>
    /// <param name="blockID"></param>
    public void ToNextBlock(int blockID)
    {
        events[currentEventID].SetCurrentBlock(blockID);
    }
    /// <summary>
    /// ���� event�� ��ȯ
    /// </summary>
    /// <returns>���� event</returns>
    public GameEvent GetCurrentEvent() { return events[currentEventID]; }
}