using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UISlot slotUIPrefab;
    [SerializeField] private Transform inventoryUIContent;
    [SerializeField] private TextMeshProUGUI itemCountText;

    private List<UISlot> _slots;

    private void Start()
    {
        _slots = new List<UISlot>(InventoryManager.Instance.InitialSlotCount);
        Render();
    }

    // 인벤토리 변경 시 새로 Render
    public void Render()
    {
        foreach (var slot in _slots)
        {
            Destroy(slot.gameObject);
        }
        _slots.Clear();

        var inventory = InventoryManager.Instance.Inventory;

        for (int i = 0; i < inventory.Slots.Count; i++)
        {
            var slotData = inventory.Slots[i];
            var slotUI = Instantiate(slotUIPrefab, inventoryUIContent);
            
            slotUI.Render(slotData.ItemId, slotData.Quantity);
            slotUI.Index = i;
            
            if (IsEquipped(slotData.ItemId))
            {
                ColorUtility.TryParseHtmlString("#88DB90", out var unequippedColor);
                slotUI.SetOutlineColor(unequippedColor);
            }
            else
            {
                ColorUtility.TryParseHtmlString("#F1765C", out var unequippedColor);
                slotUI.SetOutlineColor(unequippedColor);
            }
            
            _slots.Add(slotUI);
        }

        itemCountText.text = $"{InventoryManager.Instance.ItemCount:D2}/{InventoryManager.Instance.InventorySize:D2}";
    }
    
    private bool IsEquipped(string itemId)
    {
        if (itemId == null || itemId == string.Empty) return false;
        
        return itemId == InventoryManager.Instance.EquippedWeaponId ||
               itemId == InventoryManager.Instance.EquippedArmorId;
    }

    public void CloseUI() => UIManager.Instance.CloseUI(UIType.Inventory);
}
