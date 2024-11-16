using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<SequenceObject> selectedSequenceObjects = new List<SequenceObject>();

    private GameSequenceManager gameSequenceManager;

    void Start()
    {
        gameSequenceManager = FindObjectOfType<GameSequenceManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                InteractableObject clickedObject = hit.collider.gameObject.GetComponent<InteractableObject>();

                if (clickedObject)
                {
                    Debug.Log("Interacted: " + clickedObject.name);
                    ObjectClicked(clickedObject);
                }
            }
        }
    }

    private void ObjectClicked(InteractableObject selectedObject)
    {
        selectedObject.Interact();
        selectedSequenceObjects.Add(selectedObject.sequenceObject);
    }
}
