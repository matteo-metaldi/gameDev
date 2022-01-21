using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    public float speedMove = 200.0f;
    public float speedRotate = 200.0f;

    private bool inAir = false;
    private float startY = 0.0f;

    void Awake()
    {
        startY = transform.position.y;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        float rotationY = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            rotationY -= Time.deltaTime * speedRotate;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationY += Time.deltaTime * speedRotate;
        }

        if (Input.GetKey(KeyCode.Space) && !inAir)
        {
            inAir = true;
        }

        transform.Rotate(Vector3.up, rotationY, Space.Self);
        transform.Translate(new Vector3(moveX, 0, moveZ) * Time.deltaTime * speedMove, Space.Self);
    }
}
