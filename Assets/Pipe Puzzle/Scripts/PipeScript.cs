using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    [SerializeField] protected LayerMask detectionLayer;

    private float[] rotations = { 0, 90, 180, 270 };

    private void Start()
    {
        int random = Random.Range(0, rotations.Length);
        transform.localEulerAngles = new Vector3(0, 0, rotations[random]);
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }

    void DetectTriggerColliders()
    {
        // Center of the sphere
        Vector3 sphereCenter = transform.position;

        // Detect colliders, including triggers
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, detectionRadius, detectionLayer, QueryTriggerInteraction.Collide);

        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log("Detected: " + hitCollider.gameObject.name);

            // Optional: Check if the collider is a trigger
            if (hitCollider.isTrigger)
            {
                Debug.Log("This is a trigger collider!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the detection sphere in the Scene view
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
