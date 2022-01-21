using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float valoreZWS = Input.GetAxis("Horizontal") * Time.deltaTime * 20;
        float valoreXAD = Input.GetAxis("Vertical") * Time.deltaTime * 20;
        transform.Translate(valoreZWS, 0, valoreXAD);
    }
}
