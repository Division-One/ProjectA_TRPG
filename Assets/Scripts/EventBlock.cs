using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class EventBlock 
{
    Queue<EventContent> contentsQueue = new Queue<EventContent>();
    Queue<EventOption> optionQueue = new Queue<EventOption>();
    int blockID;
    public EventBlock(int id)
    {
        blockID = id;
    }
    public int GetBlockID() { return blockID; }
    public EventContent GetNextContent()
    {
        if (contentsQueue.Count <= 0)
            return null;
        return contentsQueue.Dequeue();
    }
    public EventOption GetNextOption()
    {
        if (optionQueue.Count <= 0)
            return null;
        return optionQueue.Dequeue();
    }
    /// <summary>
    /// Block에 contents 추가
    /// </summary>
    /// <param name="contents"></param>
    /// <returns>성공여부</returns>
    public bool AddEventContent(params EventContent[] contents)
    {
        if (contents.Length == 0)return false;

        foreach (var contentItem in contents)
        {

            contentsQueue.Enqueue(contentItem);
        }

        return true;
    }
    public bool AddEventOption(params EventOption[] options)
    {
        if(options.Length == 0)return false;

        foreach(var option in options)
        {
            optionQueue.Enqueue(option);
        }

        return true;
    }



}
