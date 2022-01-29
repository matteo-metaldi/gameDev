using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowPlayerRotation : MonoBehaviour
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

            navMeshAgent.transform.LookAt(movePosition);
           
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
