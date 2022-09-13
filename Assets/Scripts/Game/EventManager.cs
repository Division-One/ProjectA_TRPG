using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
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

        GenerateData();
    }
    // Start is called before the first frame update
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
    void GenerateData()
    {
        Debug.Log("Generate Data");
        List<List<object>> mainEventData = CSVReader.Parsing("Data/MainEventData");

        for (int i = 0; i < mainEventData.Count; i++)
        {
            string str ="";
            foreach (var s in mainEventData[i])
            {
                str += s.ToString()+",";
            }
            Debug.Log(str.TrimEnd(','));
            int eventNumber = Int32.Parse(mainEventData[i][0].ToString());
            int blockNumber = Int32.Parse(mainEventData[i][1].ToString());
            ContentType contentType = (ContentType)Int32.Parse(mainEventData[i][2].ToString());
            string content = mainEventData[i][3].ToString();

            Debug.Log("EventNumber: " + eventNumber + ", blockNumber: " + blockNumber + ", contentType: " + contentType + ", content: " + content);
            AddEvent(eventNumber, EventType.MAIN_EVENT);
            AddEventBlock(eventNumber, blockNumber);
            if (contentType == ContentType.TEXT)
            {
                AddEventContent(eventNumber, blockNumber, new EventContentText(content));
            }
            else if (contentType == ContentType.IMAGE)
            {
                AddEventContent(eventNumber, blockNumber, new EventContentImage("Sprites/" + content));
            }
            else if (contentType == ContentType.OPTION)
            {
                int connectedNumber = Int32.Parse(mainEventData[i][4].ToString());
                bool endOption = mainEventData[i][5].ToString() == "" ? false : Convert.ToBoolean(Int32.Parse(mainEventData[i][5].ToString()));
                Debug.Log("connectedNumber: " + connectedNumber );
                Debug.Log(", endOption: " + (mainEventData[i][5].ToString() == content).ToString());
                AddEventContent(eventNumber, blockNumber, new EventContentOption(content, connectedNumber, endOption));
            }
            else
            {

            }

        }
    }
    public void ToNextEvent(int eventID)
    {
        currentEventID = eventID; 
    }
    public void ToNextBlock(int blockID)
    {
        events[currentEventID].SetCurrentBlock(blockID);
    }
    public GameEvent GetCurrentEvent() { return events[currentEventID]; }
}
