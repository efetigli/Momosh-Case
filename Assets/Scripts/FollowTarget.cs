using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public Transform TargetTransform = null;
    NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Destination();
    }

    private void Destination()
    {
        if (TargetTransform == null)
            return;

        navAgent.SetDestination(TargetTransform.position);
    }
}
