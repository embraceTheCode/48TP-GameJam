using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTracker : MonoBehaviour
{
    public Action<InteractionData> OnNoticeableInteract;
    
    public static InteractableTracker Instance;
    private Stack<InteractionData> _interactables = new ();
    
    public bool HasInteractions => _interactables.Count > 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void RegisterInteraction(InteractionData interactionData)
    {
        _interactables.Push(interactionData);
        
        if(interactionData.IsNoticeable)
        {
            OnNoticeableInteract?.Invoke(interactionData);
        }
    }
    
    public void UnregisterInteraction()
    {
        if (_interactables.Count > 0)
        {
            _interactables.Pop();
        }
    }
    
    public InteractionData GetCurrentlyInteracting()
    {
        return _interactables.Peek();
    }
}
