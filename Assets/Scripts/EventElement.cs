using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public enum EventElementType
{
    TEXT = 0,
    IMAGE = 1,
    OPTION = 2,
    EVENT_ELEM_TYPE_COUNT
}

public abstract class EventElement
{
    public EventElementType eventElementType;
    public abstract bool SetElementToUIObject(GameObject obj);
}
public abstract class EventContent:EventElement
{

}
public class EventContentText : EventContent
{
    string text;
    public EventContentText(string text)
    {
        this.eventElementType = EventElementType.TEXT;
        this.text = text;
    }

    public override bool SetElementToUIObject(GameObject obj)
    {
        obj.GetComponent<TextMeshProUGUI>().text = text;
        Debug.Log("SetText: " + text);
        return true;
    }
}
public class EventContentImage : EventContent
{
    string imageFileName;
    public EventContentImage(string imageFileName)
    {
        this.eventElementType = EventElementType.IMAGE;
        this.imageFileName = imageFileName;
    }

    public override bool SetElementToUIObject(GameObject obj)
    {
        obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(imageFileName) as Sprite;
        Debug.Log("SetImage: " + imageFileName);
        return true;
    }
}
public class EventOption : EventElement
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
    public EventOption(string optionName, int connected, bool isEndOption = false)
    {
        this.eventElementType = EventElementType.OPTION;
        this.optionName = optionName;
        this.connected = connected;
        this.isEndOption = isEndOption;
    }

    public override bool SetElementToUIObject(GameObject obj)
    {
        obj.GetComponentInChildren<TextMeshProUGUI>().text = optionName;
        obj.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.OnOptionSelected(connected, isEndOption));
        Debug.Log("SetOption: " + optionName);
        return true;
    }

}