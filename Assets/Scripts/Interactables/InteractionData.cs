using System;
using System.Collections;
using System.Collections.Generic;
using ParadoxNotion.Design;
using UnityEngine;

[Serializable]
public struct InteractionData
{
    [field: SerializeField] public InteractableObject Interactable { get; private set; }
    [field: SerializeField] public Transform InteractionPosition { get; private set; }
    [field: SerializeField] public List<string> InteractionAnimation { get; private set; }
    [field: SerializeField] public bool IsNoticeable { get; private set; }
    [ShowIf("IsNoticeable", 1)] [field: SerializeField] public BarkType Emotion;
    
    public InteractionData(InteractableObject interactable, Transform interactionPosition, List<string> interactionAnimation, bool isNoticeable, BarkType emotion)
    {
        Interactable = interactable;
        InteractionPosition = interactionPosition;
        InteractionAnimation = interactionAnimation;
        IsNoticeable = isNoticeable;
        Emotion = emotion;
    }
}
