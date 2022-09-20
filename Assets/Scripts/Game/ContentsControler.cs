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
    RectTransform elementsParentTransfrom;
    [SerializeField]
    RectTransform panelTransfrom;

    [SerializeField]
    GameObject textUIPrefaps;
    [SerializeField]
    GameObject imageUIPrefaps;
    [SerializeField]
    GameObject optionUIPrefap;

    ObjectPool textUIObjectPool;
    ObjectPool imageUIObjectPool;
    ObjectPool optionUIObjectPool;

    List<GameObject> textUIObjects = new List<GameObject>();
    List<GameObject> imageUIObjects = new List<GameObject>();
    List<GameObject> optionUIObjects = new List<GameObject> ();

    float panelHeight;


    /// <summary>
    /// 사용자가 맨 위를 보고있을 때 elementsParent의 localPosition
    /// </summary>
    float elementsParentTopPos;
    /// <summary>
    /// 사용자가 맨 아래를 보고있을 때 elementsParent의 localPosition
    /// </summary>
    float elementsParentBottomPos;


    /// <summary>
    /// 사용자가 위 아래로 스크롤 할 수 있는 길이 범위
    /// </summary>
    float moveLimitY;
    /// <summary>
    /// 모든 요소들의 높이 합
    /// </summary>
    float elementsHeight;
    /// <summary>
    /// 컨텐츠(옵션을 제외한 요소)들의 높이 합
    /// </summary>
    float contentsHeight;
    /// <summary>
    /// 옵션버튼들의 높이 합
    /// </summary>
    float optionsHeight;
    bool isOnTop;
    bool isOnBottom;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        Canvas.ForceUpdateCanvases();

        textUIObjectPool= this.gameObject.AddComponent(typeof(ObjectPool)) as ObjectPool;
        textUIObjectPool.Initialize(textUIPrefaps, elementsParentTransfrom);
        imageUIObjectPool = this.gameObject.AddComponent(typeof(ObjectPool)) as ObjectPool;
        imageUIObjectPool.Initialize(imageUIPrefaps, elementsParentTransfrom);
        optionUIObjectPool = this.gameObject.AddComponent(typeof(ObjectPool)) as ObjectPool;
        optionUIObjectPool.Initialize(optionUIPrefap, elementsParentTransfrom);

        panelHeight = panelTransfrom.rect.height;
        elementsParentTopPos = elementsParentTransfrom.localPosition.y;
        
        SetHeights();
        SetMoveLimitY();
        SetPositions();

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

    void SetHeights()
    {
        float height = 0;

        foreach(var item in textUIObjects)
            height+= item.GetComponent<RectTransform>().rect.height;
        foreach (var item in imageUIObjects)
            height += item.GetComponent<RectTransform>().rect.height;
        contentsHeight = height;

        foreach (var item in optionUIObjects)
            height += item.GetComponent<RectTransform>().rect.height;

        elementsHeight = height;
        optionsHeight = elementsHeight - contentsHeight;
    }
    void SetMoveLimitY()
    {
        moveLimitY= elementsHeight - panelHeight > 0 ? elementsHeight - panelHeight : 0;
    }
    void SetPositions()
    {
        elementsParentBottomPos = elementsParentTopPos + moveLimitY;
    }
    /// <summary>
    /// 컨텐츠 이동
    /// </summary>
    /// <param name="amount">움직일 거리</param>
    void MoveContentsY(float amount)
    {
        elementsParentTransfrom.position += new Vector3(0, amount, 0);
        isOnBottom = false;
        isOnTop = false;
    }
    /// <summary>
    /// 컨텐츠 맨 위로 이동
    /// </summary>
    public void MoveContentsToTop()
    {
        elementsParentTransfrom.localPosition = new Vector3(0,elementsParentTopPos,0);        
        isOnTop = true;
    }
    /// <summary>
    /// 컨텐츠 맨 아래로 이동
    /// </summary>
    public void MoveContentsToBot()
    {
        elementsParentTransfrom.localPosition = new Vector3(0, elementsParentBottomPos, 0); ;
        isOnBottom = true;
    }
    void ClearElements()
    {
        ClearContents();
        ClearOptions();
    }
    public void ClearContents()
    {
        foreach (var item in textUIObjects)
        {
            textUIObjectPool.Push(item);
        }
        textUIObjects.Clear();
        foreach (var item in imageUIObjects)
        {
            imageUIObjectPool.Push(item);
        }
        imageUIObjects.Clear();
        elementsParentBottomPos = elementsParentTopPos;

        contentsHeight = 0;
        elementsHeight = optionsHeight;
        SetMoveLimitY();
        SetPositions();
        MoveContentsToTop();
    }
    void ClearOptions()
    {
        foreach (var item in optionUIObjects)
        {
            optionUIObjectPool.Push(item);
        }
        optionUIObjects.Clear();
        Debug.Log("ClearOptions count: " + optionUIObjects.Count);
        optionsHeight = 0;
        elementsHeight = contentsHeight;
        SetMoveLimitY();
        SetPositions();
        Debug.Log( "ClearOptions-> elementsHeight: " + elementsHeight + ", moveLimitY:" + moveLimitY + ", elementsBottomPos: " + elementsParentBottomPos);
    }
    void SetContentsPosition(GameObject obj)
    {
        RectTransform t = obj.GetComponent<RectTransform>();
        t.localPosition = new Vector3(0, elementsParentTopPos - contentsHeight, 0);
        t.offsetMax = new Vector2(0, t.offsetMax.y);
        t.offsetMin = new Vector2(0, t.offsetMin.y);
        Canvas.ForceUpdateCanvases();
        float height = t.rect.height;
        elementsHeight += height;
        contentsHeight += height;
        SetMoveLimitY();
        SetPositions();

        Debug.Log("height: " + height + "-> elementsHeight: " + elementsHeight + ", moveLimitY:" + moveLimitY+ ", elementsBottomPos: " + elementsParentBottomPos);

    }
    void SetOptionsPosition(GameObject obj)
    {
        RectTransform t = obj.GetComponent<RectTransform>();

        t.localPosition = new Vector3(0, elementsParentTopPos - elementsHeight, 0);
        t.offsetMax = new Vector2(0, t.offsetMax.y);
        t.offsetMin = new Vector2(0, t.offsetMin.y);
        Canvas.ForceUpdateCanvases();
        float height = t.rect.height;
        elementsHeight += height;
        optionsHeight += height;
        SetMoveLimitY();
        SetPositions();
        Debug.Log("height: " + height + "-> elementsHeight: " + elementsHeight + ", moveLimitY:" + moveLimitY + ", elementsBottomPos: " + elementsParentBottomPos);

    }
    public void AppendContent(EventContent content)
    {
        EventElementType type = content.eventElementType;
        GameObject obj;
        switch (type)
        {
            case EventElementType.TEXT:
                obj = textUIObjectPool.Pop();
                textUIObjects.Add(obj);
                break;
            case EventElementType.IMAGE:
                obj = imageUIObjectPool.Pop();
                imageUIObjects.Add(obj);
                break;
            default:
                return;
        }
       
        content.SetElementToUIObject(obj);

        SetContentsPosition(obj);
    }
    void AppendOption(EventOption option)
    {
        var obj = optionUIObjectPool.Pop();
        optionUIObjects.Add(obj);
        option.SetElementToUIObject(obj);

        SetOptionsPosition(obj);
    }
    public void AppendBlock(EventBlock eventBlock)
    {
        ClearOptions();
        EventContent content = eventBlock.GetNextContent();
        while(content != null)
        {
            AppendContent(content);
            content = eventBlock.GetNextContent();
        }
        EventOption option = eventBlock.GetNextOption();
        while (option != null)
        {
            AppendOption(option);
            option = eventBlock.GetNextOption();
        }


    }
    public void SetContentsPanel(EventBlock eventBlock)
    {
        ClearContents();
        AppendBlock(eventBlock);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (moveLimitY == 0) return;

        float beforePos, afterPos;
        if (eventData.delta.y < 0)//아래로 드래그하면
        {
            beforePos = elementsParentTransfrom.localPosition.y;
            afterPos = beforePos + eventData.delta.y;

            if (isOnTop) return;

            if (afterPos < elementsParentTopPos)//맨 위에 보고 있을 때
            {
                MoveContentsToTop();
                return;
            }

        }
        else//위로 드래그하면
        {
            beforePos = elementsParentTransfrom.localPosition.y;
            afterPos = beforePos + eventData.delta.y;

            if (isOnBottom) return;

            if (afterPos > elementsParentBottomPos)//맨 아래 보고 있을 때
            {
                MoveContentsToBot();
                return;
            }
        }
        MoveContentsY(eventData.delta.y);
    }
}
