using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryInteraction : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private InventoryController inventoryController;
    
    [Header("Event Channels")]
    [SerializeField] private VoidEventChannelSO OnInteract;
    [SerializeField] private VoidEventChannelSO OnStatusChanged;

    
    private List<RaycastResult> _results = new List<RaycastResult>();


    private void OnEnable()
    {
        OnInteract.RegisterListener(HandleClick);
    }

    private void OnDisable()
    {
        OnInteract.UnregisterListener(HandleClick);
    }
    
    private void HandleClick()
    {
        if (!TryRaycastSlotUI(out UISlot slotUI))
        {
            Debug.LogWarning("아이템 슬롯 검출 실패");
            return;
        }
            

        var slotData = inventoryController.GetSlot(slotUI.Index);
        if (slotData == InventorySlot.Empty)
        {
            Debug.LogWarning("아이템 데이터 Empty");
            return;
        }
            
        Debug.Log($"아이템 검출 {slotData.ItemId}");
        UseItem(slotData.ItemId);
    }

    // 인벤토리 상호작용을 Button이 아닌 Raycast 방식으로 구현
    private bool TryRaycastSlotUI(out UISlot slotUI)
    {
        _results.Clear();
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };
        graphicRaycaster.Raycast(pointerEventData, _results);

        foreach (var result in _results)
        {
            Debug.Log($"Raycast hit: {result.gameObject.name}");
            if (result.gameObject.TryGetComponent(out slotUI))
                return true;
        }

        slotUI = null;
        return false;
    }

    private void UseItem(string itemID)
    {
        inventoryController.UseItem(itemID);
        OnStatusChanged.Raise();
    }
}
