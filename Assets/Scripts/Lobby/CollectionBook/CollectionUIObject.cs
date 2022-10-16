using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CollectionUIObject : MonoBehaviour
{
    [SerializeField]
    Image imageUI;
    [SerializeField]
    TextMeshProUGUI nameUI;
    [SerializeField]
    TextMeshProUGUI infoUI;

    public CollectionData data;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Display(CollectionData data)
    {
        this.data = data;
        if (imageUI != null)
        {
            imageUI.sprite = data.collectionImage;
        }
        if (nameUI != null)
        {
            nameUI.text = data.collectionName;
        }
        if (infoUI != null)
        {
            infoUI.text = data.collectionInfo;
        }

    }
    public void ToNextCollection()
    {
        int count = CollectionBookManager.Instance.dataList[(int)data.collectionType].Count;
        if(count <= data.id + 1)
            Display(CollectionBookManager.Instance.dataList[(int)data.collectionType][0]);
        else
            Display(CollectionBookManager.Instance.dataList[(int)data.collectionType][data.id + 1]);
    }
    public void ToPrevCollection()
    {
        int count = CollectionBookManager.Instance.dataList[(int)data.collectionType].Count;
        if (0 > data.id - 1)
            Display(CollectionBookManager.Instance.dataList[(int)data.collectionType][count-1]);
        else
            Display(CollectionBookManager.Instance.dataList[(int)data.collectionType][data.id - 1]);
    }
}

