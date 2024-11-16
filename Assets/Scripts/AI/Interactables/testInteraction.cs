using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInteraction : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Renderer renderer;
    private Material defaultMaterial;
    
    [SerializeField] private InteractionData interactionData;
    
    private void Awake()
    {
        defaultMaterial = renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Interact();
            InteractableTracker.Instance.RegisterInteraction(interactionData);
        }
    }
    
    private void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        renderer.material = highlightMaterial;
    }
    
    public void Reset()
    {
        Debug.Log("Reset " + gameObject.name);
        renderer.material = defaultMaterial;
    }
}
