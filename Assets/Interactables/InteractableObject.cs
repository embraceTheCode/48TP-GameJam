using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;

    void Start()
    {
        //animator.GetComponent<Animator>();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void OnMouseDown()
    {
        Interact();
    }

    public void Interact()
    {
        animator.SetTrigger("Trigger");
    }

    public void ResetInteractable()
    {
        animator.SetTrigger("Trigger");
    }
}
