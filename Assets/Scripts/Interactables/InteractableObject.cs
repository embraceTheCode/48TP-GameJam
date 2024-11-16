using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;

    // Event Information
    public delegate void InteractAction(InteractableObject interactableObject);
    public static event InteractAction OnInteract;

    // Object Information
    [HideInInspector]
    public SequenceObject sequenceObject;
    public bool interacted;

    void Start()
    {
        sequenceObject.gameObject = gameObject;
    }

    private void Update()
    {
        
    }

    private void OnMouseOver()
    {
        
    }

    public void Interact()
    {
        //OnInteract.Invoke(this);
        animator.SetTrigger("Trigger");
        interacted = !interacted;
    }
}
