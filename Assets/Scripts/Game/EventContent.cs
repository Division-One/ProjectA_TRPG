using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public enum ContentType
{
    TEXT = 0,
    IMAGE = 1,
    OPTION = 2,
    CONTENT_TYPE_COUNT
}
public abstract class EventContent
{
    int contentIndex;
    public ContentType contentType;
    public  abstract bool SetContentToUIObject(GameObject obj);
    public int GetContentIndex() { return contentIndex; }
    public void SetContentIndex(int index) { contentIndex = index; }
}

public class EventContentText : EventContent
{
    string text;
    public EventContentText(string text)
    {
        this.contentType = ContentType.TEXT;
        this.text = text;
    }

    public override bool SetContentToUIObject(GameObject obj)
    {
        obj.GetComponent<TextMeshProUGUI>().text = text;
        return true;
    }
}
public class EventContentImage : EventContent
{
    string imageFileName;
    public EventContentImage(string imageFileName)
    {
        this.contentType = ContentType.IMAGE;
        this.imageFileName = imageFileName;
    }

    public override bool SetContentToUIObject(GameObject obj)
    {
        obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(imageFileName) as Sprite;
        return true;
    }
}
public class EventContentOption : EventContent
{
    string optionName;
    int connected;
    bool isEndOption;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionName">�ɼ��� �̸�</param>
    /// <param name="connected">�� �ɼ��� ������ �� �̵��� �̺�Ʈ Ȥ�� �̺�Ʈ ��� ��ȣ</param>
    /// <param name="isEndOption">�� �ɼ��� �����ϸ� �̺�Ʈ�� �������� ����</param>
    public EventContentOption(string optionName, int connected, bool isEndOption = false)
    {
        this.contentType = ContentType.OPTION;
        this.optionName = optionName;
        this.connected = connected;
        this.isEndOption = isEndOption;
    }

    public override bool SetContentToUIObject(GameObject obj)
    {
        obj.GetComponentInChildren<TextMeshProUGUI>().text = optionName;
        obj.GetComponent<Button>().onClick.AddListener(()=>GameManager.Instance.OnOptionSelected(connected,isEndOption));
        return true;
    }

}