using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //https://www.youtube.com/watch?v=_QajrabyTJc
    public CharacterController controller;

    public Transform groundCheck;
    public float groundDIstance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    //Velocita movimento
    [SerializeField] public float speed = 200;
    [SerializeField] public float gravity = -200;

    Vector3 velocity;

    [SerializeField] public float jmpheigth = 40;
    // Update is called once per frame
    void Update()
    {

        //Check se grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDIstance, groundMask);

        //Check se grounded
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Movimento
        Vector3 move = transform.right * x + transform.forward * z;
        
        //Gestione salto
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jmpheigth * -2f * gravity);
        }

        //Gestione sprint
        if(Input.GetKey("left shift") && isGrounded)
        {
            speed = 400;
        }
        else
        {
            speed = 200;
        }

        controller.Move(move * speed * Time.deltaTime);

        

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
