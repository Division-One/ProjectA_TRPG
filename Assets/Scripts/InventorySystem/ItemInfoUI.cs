using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemInfoUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemDescription;
    [SerializeField]
    TextMeshProUGUI itemPrice;
    [SerializeField]
    Image itemIcon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Display(ItemInfo item)
    {
        if(itemName != null)
            itemName.text = item.itemName;
        if(itemDescription != null)
            itemDescription.text = item.itemDescription;
        if(itemPrice != null)
            itemPrice.text = item.itemPrice.ToString();
        if(itemIcon != null)
            itemIcon.sprite = item.itemIcon;
    }
}
