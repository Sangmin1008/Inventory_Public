using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 아이템 정보를 띄우는 툴팁
// 마우스를 아이템이 있는 슬롯 위에 올려두면 나타나도록 설계
public class InventoryItemTooltip : MonoBehaviour
{
    [SerializeField] private GameObject itemToolTipPanel;
    [SerializeField] private IntegerEventChannelSO OnPointerEnterEvent;
    
    private bool _isItemTooltipActive = false;
    private Vector2 _offset = new Vector2(-170f, 78f);

    private void OnEnable()
    {
        OnPointerEnterEvent.RegisterListener(ShowItemTooltip);
    }

    private void OnDisable()
    {
        OnPointerEnterEvent.UnregisterListener(ShowItemTooltip);
    }

    private void Update()
    {
        if (!_isItemTooltipActive) return;
        
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            itemToolTipPanel.transform.parent as RectTransform,
            mousePosition,
            null,
            out Vector2 localPoint);

        itemToolTipPanel.GetComponent<RectTransform>().localPosition = localPoint + _offset;
    }

    private void ShowItemTooltip(int slotIndex)
    {
        if (slotIndex == -1 || String.IsNullOrEmpty(InventoryManager.Instance.Inventory.Slots[slotIndex].ItemId))
        {
            itemToolTipPanel.SetActive(false);
            _isItemTooltipActive = false;
            return;
        }

        itemToolTipPanel.SetActive(true);
        _isItemTooltipActive = true;
        
        if (itemToolTipPanel.TryGetComponent<UIItemData>(out var itemDataUI))
        {
            string itemID = InventoryManager.Instance.Inventory.Slots[slotIndex].ItemId;
            itemDataUI.Render(itemID);
        }
        else
        {
            Debug.LogWarning("UIItemData가 없음");
        }
    }
}
