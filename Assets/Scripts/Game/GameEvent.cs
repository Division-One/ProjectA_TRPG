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
    public GameEvent(int id, EventType eventType)
    {
        this.eventID = id;
        this.eventType = eventType;
    }
    public int GetEventID() { return eventID; }
    public void SetEventId(int id) { eventID = id; }
    public bool ContainsBlock(int id)
    {
        return eventBlocks.ContainsKey(id);
    }
    public EventBlock GetEventBlock(int id)
    {
        if (eventBlocks.ContainsKey(id))
            return eventBlocks[id];
        return null;
    }
    public EventBlock GetCurretBlock(){return eventBlocks[currentBlockID];}
    public void SetCurrentBlock(int blockId) { currentBlockID = blockId; }

    public EventBlock AddEventBlock(int blockId)
    {
        if (eventBlocks.ContainsKey(blockId)) return null;
        eventBlocks[blockId] = new EventBlock(blockId);

        return eventBlocks[blockId];
    }

}
