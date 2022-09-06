using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class EventBlock 
{
    Queue<EventContent> contentsQueue = new Queue<EventContent>();
    int blockID;
    int containedContentCount = 0;

    public int GetBlockID() { return blockID; }
    public void SetBlockID(int a) { blockID = a; }
    public int GetContainedContentCount() { return containedContentCount; }

    /// <summary>
    /// Block�� contents �߰�
    /// </summary>
    /// <param name="content"></param>
    /// <returns>��������</returns>
    public bool AddContent(params EventContent[] content)
    {
        if (content.Length == 0)return false;

        foreach (var contentItem in content)
        {
            contentItem.SetContentIndex(containedContentCount++);
            contentsQueue.Enqueue(contentItem);
        }

        return true;
    }
    public EventContent GetNextContent()
    {
        if(contentsQueue.Count <= 0)
            return null;
        return contentsQueue.Dequeue();
    }


}
