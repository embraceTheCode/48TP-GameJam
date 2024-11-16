using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class BarkManager : SerializedMonoBehaviour
{
    [field: SerializeField] public Dictionary<BarkType, GameObject> EmoteDictionary { private set; get; } = new();
    [SerializeField] private float barkDuration;
    private BarkType? _currentBark = null;

    private void Start()
    {
        InteractableTracker.Instance.OnNoticeableInteract += TriggerBark;
    }

    public void TriggerBark(InteractionData interactionData)
    {
        BarkType barkType = interactionData.Emotion;
        if (_currentBark == null || _currentBark <= barkType)
        {
            StartCoroutine(ActivateBark(barkType));
        }
    }
    
    IEnumerator ActivateBark(BarkType barkType)
    {
        _currentBark = barkType;
        EmoteDictionary[barkType].SetActive(true);
        yield return new WaitForSeconds(barkDuration);
        EmoteDictionary[barkType].SetActive(false);
        _currentBark = null;
    }
}
