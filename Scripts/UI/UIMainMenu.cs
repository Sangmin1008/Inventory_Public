using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private StringEventChannelSO OnInventoryAddItem;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Character player;

    private void Start()
    {
        playerName.text = player.PlayerName;
    }

    public void OpenInventory() => UIManager.Instance.ShowUI(UIType.Inventory);
    public void OpenStatus() => UIManager.Instance.ShowUI(UIType.Status);
    public void AddApple() => OnInventoryAddItem.Raise("apple_001");
    public void AddBread() => OnInventoryAddItem.Raise("bread_002");
    public void AddSlot()
    {
        InventoryManager.Instance.Inventory.Slots.Add(InventorySlot.Empty);
        UIManager.Instance.InventoryUI.Render();
    }
}
