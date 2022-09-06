using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    GameObject prefap;
    Transform parent;
    int useCount=0;
    int reserveCount=0;
    Queue<GameObject> items;

    public ObjectPool()
    {
        items = new Queue<GameObject>();
    }
    private GameObject AllocNewObject()
    {
        var obj = Instantiate(prefap);
        if (parent != null)
        {
            obj.transform.SetParent(parent);
        }

        useCount++;
        return obj;
    }

    public void Initialize(GameObject prefap, Transform parent)
    {
        this.prefap = prefap;
        this.parent = parent;
    }

    public GameObject Pop()
    {
        if(reserveCount <= 0)
        {
            return AllocNewObject();
        }
        else
        {
            var obj = items.Dequeue();
            obj.SetActive(true);
            reserveCount--;
            useCount++;
            return obj;
        }

    }
    public void Push(GameObject obj)
    {
        Debug.Log(items.Count );
        reserveCount++;
        useCount++;
        obj.SetActive(false);
        items.Enqueue(obj);
    }
}
