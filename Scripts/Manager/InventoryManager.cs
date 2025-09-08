using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class InventoryManager : Singleton<InventoryManager>
{
    [Header("Initial Slots")]
    [SerializeField] private int initialSlotCount = 1;
    
    [Header("Event Channel")]
    public VoidEventChannelSO OnInventoryChanged;
    
    [field:SerializeField] public Inventory Inventory { get; private set; }
    public int InitialSlotCount => initialSlotCount;
    public string EquippedWeaponId = null;
    public string EquippedArmorId = null;
    public int InventorySize => Inventory.Slots.Count;
    public int ItemCount => Inventory.Slots.Count(slot => slot.Quantity != 0);

    protected override void Awake()
    {
        base.Awake();
        //Inventory = new Inventory(initialSlotCount);
    }

    private void Start()
    {
        OnInventoryChanged.RegisterListener(UIManager.Instance.InventoryUI.Render);
    }
}
