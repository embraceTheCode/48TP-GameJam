using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequenceManager : MonoBehaviour
{
    [SerializeField] public List<SequenceObjectWrapper> gameSequences;
    [SerializeField] private int currentSequence;
    [SerializeField] private int sequenceCounter;

    public static GameSequenceManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        currentSequence = 0;
    }

    public void PerformSequenceCheck(SequenceObject selectedObject, int index)
    {
        SequenceObject currentSequenceObject = gameSequences[currentSequence].sequenceObjects[index];

        // If the selected object corresponds to the object set on the manager, add one to the sequence counter
        if (selectedObject == currentSequenceObject)
        {
            sequenceCounter++;
        }
        // Else if the selected object does not pass the isCheck flag, reset the sequence counter and the sequence check
        else if (selectedObject.isCheckObject != currentSequenceObject.isCheckObject)
        {
            sequenceCounter = 0;
            ResetSequenceCheck();
            return;
        }

        // If the sequenceCounter is equal to the number of objects in the current sequence list, go to the next sequence
        if (sequenceCounter == gameSequences[currentSequence].sequenceObjects.Count)
        {
            currentSequence++;
        }
        // Else, restart the sequence counter and sequence check
        else
        {
            sequenceCounter = 0;
            ResetSequenceCheck();
        }
    }

    public void ResetSequenceCheck()
    {
        // Method to call the AI reset behaviour
    }
}
