using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CountObjects : MonoBehaviour
{
    public string nextLevel;
    public GameObject objToDestry;
    [SerializeField] TextMeshProUGUI m_Object;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
        m_Object.text = ObjectsToCollect.objects.ToString();
        if (ObjectsToCollect.objects == 0)
        {
            Destroy(objToDestry);
            m_Object.text = "The door is open!";
        }
    }
}
