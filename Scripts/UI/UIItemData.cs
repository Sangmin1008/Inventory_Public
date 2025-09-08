using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIItemData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    
    // 아이템 툴팁 패널
    // 다른 슬롯 위에 마우스가 이동하면 새롭게 Render
    public void Render(string itemID)
    {
        itemNameText.text = GameManager.Instance.ItemRegistry.GetItem(itemID).ItemName;
        itemDescriptionText.text = GameManager.Instance.ItemRegistry.GetItem(itemID).Description;
    }
}
