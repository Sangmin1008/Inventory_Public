using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    // 인벤토리를 MonoBehaviour에 상속시키지 않고 생명 주기와 독립적으로 정보만 갖도록 설계
    public List<InventorySlot> Slots;
    
    public Inventory(int slotNum)
    {
        Slots = new List<InventorySlot>(slotNum);

        for (int i = 0; i < slotNum; i++)
        {
            Slots.Add(InventorySlot.Empty);
        }
    }
}
