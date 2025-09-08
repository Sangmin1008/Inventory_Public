using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] private VoidEventChannelSO OnInventoryChanged;
    [SerializeField] private StringEventChannelSO OnInventoryAddItem;
    
    private Inventory _inventory;

    private void Start()
    {
        _inventory = InventoryManager.Instance.Inventory;
    }

    private void OnEnable()
    {
        OnInventoryAddItem.RegisterListener(AddItem);
    }

    private void OnDisable()
    {
        OnInventoryAddItem.UnregisterListener(AddItem);
    }

    public bool AddItem(string itemID, int quantity = 1)
    {
        for (int i = 0; i < _inventory.Slots.Count; i++)
        {
            var slot = _inventory.Slots[i];
            if (slot.ItemId == itemID)
            {
                _inventory.Slots[i] = slot + (slot.ItemId, quantity);
                OnInventoryChanged?.Raise();
                return true;
            }
        }
        
        for (int i = 0; i < _inventory.Slots.Count; i++)
        {
            if (_inventory.Slots[i] == InventorySlot.Empty)
            {
                _inventory.Slots[i] = InventorySlot.Empty + (itemID, quantity);
                OnInventoryChanged?.Raise();
                return true;
            }
        }

        Debug.LogWarning("인벤토리 공간 부족");
        return false;
    }

    public void AddItem(string itemID) => AddItem(itemID, 1);

    public bool RemoveItem(string itemID, int quantity = 1)
    {
        for (int i = 0; i < _inventory.Slots.Count; i++)
        {
            var slot = _inventory.Slots[i];
            if (slot.ItemId == itemID)
            {
                try
                {
                    _inventory.Slots[i] = slot - (slot.ItemId, quantity);
                    OnInventoryChanged?.Raise();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    return false;
                }
            }
        }
        
        Debug.LogWarning("제거할 아이템이 없음");
        return false;
    }
    
    public InventorySlot GetSlot(int index)
    {
        if (index >= 0 && index < _inventory.Slots.Count)
            return _inventory.Slots[index];

        Debug.LogWarning("잘못된 인덱스 접근 " + index);
        return InventorySlot.Empty;
    }

    public void UseItem(string itemID)
    {
        var itemData = GameManager.Instance.ItemRegistry.GetItem(itemID);
        

        if (itemData.ItemType == ItemType.Consumable)
        {
            itemData.UseItem();
            Debug.Log($"{itemData.ItemName} 소비!");
            RemoveItem(itemID, 1);
        }
        else
        {
            SelectItem(itemID);
        }
    }

    private void SelectItem(string itemID)
    {
        var itemData = GameManager.Instance.ItemRegistry.GetItem(itemID);

        switch (itemData.ItemType)
        {
            case ItemType.Weapon:
                if (!String.IsNullOrEmpty(InventoryManager.Instance.EquippedWeaponId))
                {
                    var prevWeapon = GameManager.Instance.ItemRegistry.GetItem(InventoryManager.Instance.EquippedWeaponId);
                    foreach (var trait in prevWeapon.Traits)
                    {
                        if (trait is IItemTrait itemTrait)
                            itemTrait.Unapply();
                    }
                }
                
                if (InventoryManager.Instance.EquippedWeaponId == itemID)
                {
                    InventoryManager.Instance.EquippedWeaponId = null;
                    Debug.Log("무기 해제됨: " + itemID);
                }
                else
                {
                    InventoryManager.Instance.EquippedWeaponId = itemID;
                    Debug.Log("무기 장착됨: " + itemID);
                    itemData.UseItem();
                }
                break;

            case ItemType.Armor:
                if (!String.IsNullOrEmpty(InventoryManager.Instance.EquippedArmorId))
                {
                    var prevArmor = GameManager.Instance.ItemRegistry.GetItem(InventoryManager.Instance.EquippedArmorId);
                    foreach (var trait in prevArmor.Traits)
                    {
                        if (trait is IItemTrait itemTrait)
                            itemTrait.Unapply();
                    }
                }
                
                if (InventoryManager.Instance.EquippedArmorId == itemID)
                {
                    InventoryManager.Instance.EquippedArmorId = null;
                    Debug.Log("방어구 해제됨: " + itemID);
                }
                else
                {
                    InventoryManager.Instance.EquippedArmorId = itemID;
                    Debug.Log("방어구 장착됨: " + itemID);
                    itemData.UseItem();
                }
                break;
        }
        OnInventoryChanged?.Raise();
    }
}
