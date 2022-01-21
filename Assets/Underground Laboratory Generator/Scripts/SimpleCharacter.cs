using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleCharacter : MonoBehaviour
{
    public bool hideCursor = false, freezeCursor = true;
    public float speed = 3, run = 6, sens = 10;
    public Transform CharacterCamera;

    CharacterController ch;
    Quaternion camquat;

    private void Start()
    {
        ch = GetComponent<CharacterController>();

        if (hideCursor) Cursor.visible = false;
        if (freezeCursor) Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector3 move = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        if (Input.GetKey(KeyCode.LeftShift)) move *= run;
        else move *= speed;
        move -= Vector3.up * 2;
        ch.Move(move * Time.deltaTime);

        float bodyrot = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        transform.Rotate(0, bodyrot, 0);
        float camrot = -Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        CharacterCamera.Rotate(camrot, 0, 0);
    }
}
