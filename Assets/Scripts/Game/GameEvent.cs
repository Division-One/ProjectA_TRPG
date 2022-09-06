using System.Collections;
using System.Collections.Generic;

public enum EventType
{
    MAIN_EVENT = 0,
    SUB_EVENT = 1,
    EVENT_TYPE_COUNT
}

public class GameEvent
{

    EventType eventType;
    Dictionary<int,EventBlock> eventBlocks = new Dictionary<int, EventBlock>();
    int currentBlockID = 0;
    int eventID;
    public GameEvent(EventType eventType)
    {
        this.eventType = eventType;
    }
    public int GetEventID() { return eventID; }
    public void SetEventId(int id) { eventID = id; }
    public EventBlock GetCurretBlock(){return eventBlocks[currentBlockID];}
    public void SetCurrentBlock(int blockId) { currentBlockID = blockId; }

    public bool AddEventBlock(int index, EventBlock eventBlock)
    {
        if (eventBlocks.ContainsKey(index)) return false;
        eventBlocks[index] = eventBlock;

        return true;
    }

}
