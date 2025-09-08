using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericItemTraitSO<T> : ScriptableObject, IItemTrait
{
    [SerializeField] private T data;
    [SerializeField] private GenericEventChannelSO<T> eventChannelSO;
    public void Apply()
    {
        eventChannelSO.Raise(data);
    }

    public void Unapply()
    {
        if (typeof(T) == typeof(float))
        {
            float value = (float)(object)data;
            float negative = -value;
            eventChannelSO.Raise((T)(object)negative);
        }
        else
        {
            Debug.LogWarning("Unapply not supported for type " + typeof(T));
        }
    }

    protected void SetData(T value)
    {
        data = value;
    }
}
