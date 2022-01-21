using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject runner;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - runner.transform.position;   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = runner.transform.position + offset;
    }
}
