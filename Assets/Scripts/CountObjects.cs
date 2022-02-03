using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountObjects : MonoBehaviour
{
    public string nextLevel;
    public GameObject objToDestry;
    GameObject objUI;
    // Start is called before the first frame update
    void Start()
    {
        objUI = GameObject.Find("ObjectNum");
    }

    // Update is called once per frame
    void Update()
    {
        objUI.GetComponent<Text>().text = ObjectsToCollect.objects.ToString();
        if(ObjectsToCollect.objects == 0)
        {
            Destroy(objToDestry);
            objUI.GetComponent<Text>().text = "The door is open!";
        }
    }
}
