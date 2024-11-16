using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;

    public Action<bool> OnInteract;

    // Object Information
    public SequenceObject sequenceObject;
    [HideInInspector] public bool interacted;

    void Start()
    {
        sequenceObject.gameObject = gameObject;
    }

    private void OnMouseOver()
    {
        
    }

    public void Interact()
    {
        interacted = !interacted;
        OnInteract.Invoke(interacted);
        animator.SetTrigger("Trigger");
    }
}
