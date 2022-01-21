using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerNav : MonoBehaviour
{

    [SerializeField] private Transform movePosition;
    private NavMeshAgent navMeshAgent;

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = movePosition.position;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
