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
        GameEvent gameEvent = new GameEvent(EventType.MAIN_EVENT);
        EventBlock eventBlock = new EventBlock();

        eventBlock.AddContent(new EventContentText("event 0 start"));
        eventBlock.AddContent(new EventContentImage("Sprites/img"));
        eventBlock.AddContent(new EventContentOption("to block 1",1));
        gameEvent.AddEventBlock(0,eventBlock);

        eventBlock = new EventBlock();
        eventBlock.AddContent(new EventContentText("event0, second block" ));
        eventBlock.AddContent(new EventContentImage("Sprites/Logo"));
        eventBlock.AddContent(new EventContentOption("end Event0, to Event1", 1,true));
        gameEvent.AddEventBlock(1, eventBlock);
        AddEvent(0, gameEvent);

        gameEvent = new GameEvent(EventType.SUB_EVENT);

        eventBlock = new EventBlock();
        eventBlock.AddContent(new EventContentText("event1 start"));
        eventBlock.AddContent(new EventContentImage("Sprites/img"));
        eventBlock.AddContent(new EventContentOption("to event1 second block", 1));
        gameEvent.AddEventBlock(0, eventBlock);

        eventBlock = new EventBlock();
        eventBlock.AddContent(new EventContentText("event1 second block"));
        eventBlock.AddContent(new EventContentImage("Sprites/Logo"));
        eventBlock.AddContent(new EventContentOption("end Event1, game End", -1, true));
        gameEvent.AddEventBlock(1, eventBlock);
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
