using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetSpeed : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private void Update()
    {
        animator.SetBool(IsMoving, agent.hasPath);
    }
}
