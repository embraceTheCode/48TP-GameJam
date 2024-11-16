using System;
using System.Collections;
using System.Collections.Generic;
using ParadoxNotion.Design;
using UnityEngine;

[Serializable]
public struct InteractionData
{
    [field: SerializeField] public testInteraction Interactable { get; private set; }
    [field: SerializeField] public Transform InteractionPosition { get; private set; }
    [field: SerializeField] public string InteractionAnimation { get; private set; }
    [field: SerializeField] public bool IsNoticeable { get; private set; }
    [ShowIf("IsNoticeable", 1)] [field: SerializeField] public BarkType Emotion;
    
    public InteractionData(testInteraction interactable, Transform interactionPosition, string interactionAnimation, bool isNoticeable, BarkType emotion)
    {
        Interactable = interactable;
        InteractionPosition = interactionPosition;
        InteractionAnimation = interactionAnimation;
        IsNoticeable = isNoticeable;
        Emotion = emotion;
    }
}
