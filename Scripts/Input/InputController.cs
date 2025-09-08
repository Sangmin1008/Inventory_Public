using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputController : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO OnInteract;
    
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Canceled)
        {
            OnInteract?.Raise();
        }
    }
}