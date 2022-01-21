using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float RotationSpeed = 10f;

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, RotationSpeed * Time.deltaTime);
    }
}
