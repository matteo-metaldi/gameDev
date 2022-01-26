using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToCollect : MonoBehaviour
{
    public static int objects = 0;
    public AudioSource collectSound;

    private void Awake()
    {
        objects++;
    }

    public void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        if (other.gameObject.tag == "Player")
            objects--;
        gameObject.SetActive(false);
    }
}
