using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    [SerializeField] private Outline outline;
    [SerializeField] private IntegerEventChannelSO OnPointerEnterEvent;
    
    public int Index;

    // 슬롯 정보가 변경되면 새로 Render
    public void Render(string itemID, int quantity)
    {
        var itemData = GameManager.Instance.ItemRegistry.GetItem(itemID);

        if (itemData != null)
        {
            itemIcon.sprite = itemData.Icon;
            itemIcon.enabled = true;
            itemQuantity.text = quantity > 0 ? quantity.ToString() : string.Empty;
        }
        else
        {
            itemIcon.enabled = false;
            itemQuantity.text = string.Empty;
        }
    }
    
    public void SetOutlineColor(Color color)
    {
        if (outline != null)
            outline.effectColor = color;
    }

    
    // 마우스가 슬롯 위에 들어오면 이벤트 발생
    // 아이템 툴팁 패널 액티브 활성화
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent.Raise(Index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerEnterEvent.Raise(-1);
    }
}