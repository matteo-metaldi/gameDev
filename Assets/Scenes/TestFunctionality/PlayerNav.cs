using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerNav : MonoBehaviour
{

    [SerializeField] private Transform movePosition;
    private NavMeshAgent navMeshAgent;

    public float loadPosition = 5.0f;



    // Update is called once per frame
    void Update()
    {
        loadPosition -= Time.deltaTime;

        if (loadPosition <= 0.0f)
        {
 
            navMeshAgent.destination = movePosition.position;
            loadPosition = 5;
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
