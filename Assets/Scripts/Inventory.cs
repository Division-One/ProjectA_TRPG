using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class Inventory: MonoBehaviour
{
    public int horizontalSlotCount = 6;
    public int verticalSlotCount = 2;
    public GameObject slotPrefab;
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
        rectTransform.sizeDelta = new Vector2(horizontalSlotCount * grid.cellSize.x + (horizontalSlotCount - 1)*grid.spacing.x,
            verticalSlotCount * grid.cellSize.y + (verticalSlotCount - 1) * grid.spacing.y);
        for(int i = 0; i < verticalSlotCount*horizontalSlotCount; i++)
        {
            Instantiate(slotPrefab, transform);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
