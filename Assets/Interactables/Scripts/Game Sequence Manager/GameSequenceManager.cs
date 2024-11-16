using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequenceManager : MonoBehaviour
{
    [SerializeField] public List<SequenceObjectWrapper> gameSequences;
    private int currentSequence;

    void Start()
    {
        currentSequence = 0;
    }

    public void PerformCheck()
    {
        //gameSequences[currentSequence][]
    }
}
