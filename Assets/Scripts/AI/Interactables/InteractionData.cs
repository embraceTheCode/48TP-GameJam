using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InteractionData
{
    [field: SerializeField] public testInteraction Interactable { get; private set; }
    [field: SerializeField] public Transform InteractionPosition { get; private set; }
    [field: SerializeField] public string InteractionAnimation { get; private set; }
    
    public InteractionData(testInteraction interactable, Transform interactionPosition, string interactionAnimation)
    {
        Interactable = interactable;
        InteractionPosition = interactionPosition;
        InteractionAnimation = interactionAnimation;
    }
}
