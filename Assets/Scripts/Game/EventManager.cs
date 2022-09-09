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


    bool AddEvent(int id, GameEvent e)
    {
        if (events.ContainsKey(id)) return false;
        e.SetEventId(id);
        events[id] = e;
        return true;
    }
    void GenerateData()
    {
        Debug.Log("Generate Data");
        List<Dictionary<string, object>> mainEventData = CSVReader.Read("Data/MainEventData");
        List<Dictionary<string, object>> subEventData = CSVReader.Read("Data/SubEventData");

        GameEvent gameEvent = new GameEvent(EventType.MAIN_EVENT);
        for (int i = 0; i < mainEventData.Count; i++)
        { 
            var eventBlock = new EventBlock();
            eventBlock.AddContent(new EventContentText(mainEventData[i]["EventText"].ToString()));
            eventBlock.AddContent(new EventContentImage("Sprites/" + mainEventData[i]["Image"]));
            eventBlock.AddContent(new EventContentOption(mainEventData[i]["OptionName"].ToString(),
               Int32.Parse(mainEventData[i]["connectedNum"].ToString()), Convert.ToBoolean(Int32.Parse(mainEventData[i]["EndOption"].ToString()))));
            gameEvent.AddEventBlock(i,eventBlock);
        }
        AddEvent(0, gameEvent);

        
        gameEvent = new GameEvent(EventType.SUB_EVENT);

        for (int i = 0; i < subEventData.Count; i++)
        {
            var eventBlock = new EventBlock();
            eventBlock.AddContent(new EventContentText(subEventData[i]["EventText"].ToString()));
            eventBlock.AddContent(new EventContentImage("Sprites/" + subEventData[i]["Image"]));
            eventBlock.AddContent(new EventContentOption(subEventData[i]["OptionName"].ToString(),
                Int32.Parse(mainEventData[i]["connectedNum"].ToString()), Convert.ToBoolean(Int32.Parse(subEventData[i]["EndOption"].ToString()))));
            gameEvent.AddEventBlock(i, eventBlock);
        }
        AddEvent(1, gameEvent);
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
