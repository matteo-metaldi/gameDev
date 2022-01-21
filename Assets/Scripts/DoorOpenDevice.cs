using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;
    private bool _open = false;

    void Operate()
    {
        Debug.Log("EEEEEEEE");
        if (_open)
        {
            Vector3 pos =
            transform.position - dPos;
            transform.position = pos;
        }
        else
        {
            Vector3 pos =
            transform.position + dPos;
            transform.position = pos;
        }
        _open = !_open;
    }
}
