using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SeparatedVolume: MonoBehaviour
{
    [SerializeField]
    Image fill;
    [SerializeField]
    Button decButton;
    [SerializeField]
    Button incButton;

    [SerializeField]
    float defaultFill = 0.4f;
    [SerializeField]
    float threshold = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        fill.fillAmount = defaultFill <= 1f ? defaultFill : 1f;
    }
    public void Decrease()
    {
        if(fill.fillAmount >= threshold)
            fill.fillAmount -= threshold;
    }
    public void Increase()
    {
        if (fill.fillAmount <= 1f)
            fill.fillAmount += threshold;
    }

    /// <summary>
    /// DecraseButton이 눌렸을 때의 동작을 추가한다.
    /// </summary>
    /// <param name="call">추가될 동작</param>
    public void AddDecreaseListener(UnityAction call)
    {
        decButton.onClick.AddListener(call);
    }

    /// <summary>
    /// IncreaseButton이 눌렸을 때의 동작을 추가한다.
    /// </summary>
    /// <param name="call">추가될 동작</param>
    public void AddIncreaseListener(UnityAction call)
    {
        incButton.onClick.AddListener(call);
    }
}
