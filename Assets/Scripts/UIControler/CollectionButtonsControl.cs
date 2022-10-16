using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollectionButtonsControl : MonoBehaviour
{
    [SerializeField]
    GameObject collectionButtonPrefab;

    List<CollectionUIObject> collectionButtons = new List<CollectionUIObject>();

    public void Instantiate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(collectionButtonPrefab, transform);
            CollectionUIObject collectionUI = obj.GetComponent<CollectionUIObject>();
            collectionButtons.Add(collectionUI);
            obj.GetComponent<Button>().onClick.AddListener(delegate
            {
                CollectionBookManager.Instance.OnCollectionButtonSelected(collectionUI.data);
            });
        }
    }
    public void DisplayPage(List<CollectionData> data)
    {
        int dataCount = data.Count;
        int collectionButtonCount = collectionButtons.Count;
        if (dataCount > collectionButtonCount)
        {
            Debug.LogError("CollectionButtonsPaging Error(DispalyPage too much)");
            return;
        }

        for (int i = 0; i < collectionButtonCount; i++)
        {
            if (i < dataCount)
            {
                collectionButtons[i].gameObject.SetActive(true);
                collectionButtons[i].Display(data[i]);
            }
            else
            {
                collectionButtons[i].gameObject.SetActive(false);
            }

        }

    }
}
