using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private LayerMask detectionLayer;
    private void OnTriggerEnter(Collider other)
    {
        if ((detectionLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log(other.name);
        }
    }
}
