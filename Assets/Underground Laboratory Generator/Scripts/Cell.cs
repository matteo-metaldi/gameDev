using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class Cell : MonoBehaviour
{
    [HideInInspector]
    public BoxCollider TriggerBox;
    public GameObject[] Exits;

    private void Awake()
    {
        TriggerBox = GetComponent<BoxCollider>();
        TriggerBox.isTrigger = true;
    }
}
