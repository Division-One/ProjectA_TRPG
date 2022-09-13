using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
/// <summary>
/// 컨텐츠 패널 컨트롤
/// </summary>
public class ContentsControler : MonoBehaviour,IDragHandler
{
    private static ContentsControler instance;
    public static ContentsControler Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    [SerializeField]
    RectTransform contentsTransfrom;
    [SerializeField]
    List<GameObject> contentsUIPrefaps;

    List<ObjectPool> contentsUIObjectPool = new List<ObjectPool>();
    List<List<GameObject>> contentsUIObjects = new List<List<GameObject>>();

    Vector3 contentsTopPos;
    Vector3 contentsBottomPos;
    Vector3 currentContentPos;
    float panelHeight;
    float contentsHeight;
    float moveLimitY;
    bool isOnTop;
    bool isOnBottom;

    public TextMeshProUGUI t;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        Canvas.ForceUpdateCanvases();
        for (ContentType i = 0; i < ContentType.CONTENT_TYPE_COUNT; i++)
        {
            contentsUIObjects.Add(new List<GameObject>());
            contentsUIObjectPool.Add(this.gameObject.AddComponent(typeof(ObjectPool)) as ObjectPool);
            contentsUIObjectPool[(int)i].Initialize(contentsUIPrefaps[(int)i], contentsTransfrom);
        }

        panelHeight = transform.gameObject.GetComponent<RectTransform>().rect.height;
        contentsHeight = GetContentsHeight();
        moveLimitY = contentsHeight - panelHeight > 0 ? contentsHeight - panelHeight : 0;
        contentsTopPos = contentsTransfrom.localPosition;
        currentContentPos = contentsTopPos;
        contentsBottomPos = contentsTransfrom.localPosition + new Vector3(0, moveLimitY, 0);

    }
    // Start is called before the first frame update
    void Start()
    {
        MoveContentsToTop();
    }

    // Update is called once per frame
    void Update()
    {
        //t.text ="contensPos: "+ contentsTransfrom.localPosition + ", contentsHeight: " + contentsHeight+ ", contentsTopPos" + contentsTopPos;
    }

    float GetContentsHeight()
    {
        float height = 0;
        foreach(var obj in contentsUIObjects)
        {
            height += t.gameObject.GetComponent<RectTransform>().rect.height;
        }

        return height;
    }

    /// <summary>
    /// 컨텐츠 이동
    /// </summary>
    /// <param name="amount">움직일 거리</param>
    void MoveContentsY(float amount)
    {
        contentsTransfrom.position += new Vector3(0, amount, 0);
        isOnBottom = false;
        isOnTop = false;
    }
    /// <summary>
    /// 컨텐츠 맨 위로 이동
    /// </summary>
    public void MoveContentsToTop()
    {
        contentsTransfrom.localPosition = contentsTopPos;
        isOnTop = true;
    }
    /// <summary>
    /// 컨텐츠 맨 아래로 이동
    /// </summary>
    public void MoveContentsToBot()
    {
        contentsTransfrom.localPosition = contentsBottomPos;
        isOnBottom = true;
    }

    public void ClearContents()
    {
        for (ContentType i = 0; i < ContentType.CONTENT_TYPE_COUNT; i++)
        {
            foreach (var j in contentsUIObjects[(int)i])
            {
                contentsUIObjectPool[(int)i].Push(j);
            }
        }
        currentContentPos = contentsTopPos;
    }
    void SetUIObjectPosition(GameObject obj)
    {

        Debug.Log("Move "+obj.ToString() + " to "+ currentContentPos);
        Debug.Log("obj height: " + obj.GetComponent<RectTransform>().rect);
        obj.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);



        Debug.Log("Set currentContentPos to" + currentContentPos);

    }
    public void AppendBlock(EventBlock eventBlock)
    {
        EventContent content;
        content = eventBlock.GetNextContent();
        while(content != null)
        {
            var obj = contentsUIObjectPool[(int)content.contentType].Pop();
            content.SetContentToUIObject( obj);
            contentsUIObjects[(int)content.contentType].Add(obj);
            Canvas.ForceUpdateCanvases();

            RectTransform t = obj.GetComponent<RectTransform>();
            t.localPosition = currentContentPos;
            t.offsetMax = new Vector2(0, t.offsetMax.y);
            t.offsetMin = new Vector2(0, t.offsetMin.y);
            currentContentPos -= new Vector3(0, t.rect.height, 0);
            //SetUIObjectPosition(obj);
   

            content = eventBlock.GetNextContent();
        }


    }
    public void SetContentsPanel(EventBlock eventBlock)
    {
        ClearContents();
        AppendBlock(eventBlock);
    }

    public void OnDrag(PointerEventData eventData)
    {
        t.text = "eventData.delta.y : " + eventData.delta.y + ", contensPos: " + contentsTransfrom.localPosition;
        if (moveLimitY == 0) return;

        float beforePos, afterPos;
        if (eventData.delta.y < 0)//아래로 드래그하면
        {
            beforePos = contentsTransfrom.localPosition.y;
            afterPos = beforePos + eventData.delta.y;

            if (isOnTop) return;

            if (afterPos < contentsTopPos.y)//맨 위에 보고 있을 때
            {
                MoveContentsToTop();
                return;
            }

        }
        else//아래로 드래그하면
        {
            beforePos = contentsTransfrom.localPosition.y;
            afterPos = beforePos + eventData.delta.y;

            if (isOnBottom) return;

            if (afterPos > contentsBottomPos.y)//맨 아래 보고 있을 때
            {
                MoveContentsToBot();
                return;
            }
        }
        MoveContentsY(eventData.delta.y);
    }
}
