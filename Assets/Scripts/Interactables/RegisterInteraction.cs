using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterInteraction : MonoBehaviour
{
    [SerializeField] private InteractableObject interactableObject;
    [SerializeField] private InteractionData interactionData;

    private void Awake()
    {
        interactableObject.OnInteract += RegisterInteractable;
    }

    private void RegisterInteractable(bool isInteracted)
    {
        if (isInteracted)
        {
            InteractableTracker.Instance.RegisterInteraction(interactionData);
        }
    }
}
