using System.Collections.Generic;
using UnityEngine;

// 아이템 정보를 관리하는 ScriptableObject
[CreateAssetMenu(fileName = "ItemRegistry", menuName = "Scriptable Objects/Item/Item Registry")]
public class ItemRegistrySO : ScriptableObject
{
    [SerializeField] private List<GenericItemDataSO> items;

    private Dictionary<string, GenericItemDataSO> _lookup;

    public void Init()
    {
        _lookup = new Dictionary<string, GenericItemDataSO>();
        foreach (var item in items)
        {
            if (!_lookup.ContainsKey(item.ItemID))
                _lookup[item.ItemID] = item;
        }
    }

    public GenericItemDataSO GetItem(string itemID)
    {
        if (_lookup == null)
        {
            _lookup = new Dictionary<string, GenericItemDataSO>();
        }
        return _lookup.TryGetValue(itemID, out var item) ? item : null;
    }
    
    public string GetStringID(int hash)
    {
        foreach (var kv in _lookup)
            if (kv.Value.ItemID.GetHashCode() == hash)
                return kv.Key;
        return null;
    }
    
#if UNITY_EDITOR
    public void ClearItems()
    {
        items.Clear();
    }

    public void AddItem(GenericItemDataSO item)
    {
        if (!items.Contains(item))
            items.Add(item);
    }

    public int GetItemCount()
    {
        return items.Count;
    }
#endif
}