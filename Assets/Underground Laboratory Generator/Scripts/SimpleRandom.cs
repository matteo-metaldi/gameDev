using System.Collections;
using UnityEngine;

public class SimpleRandom : MonoBehaviour
{
    public bool EnableChance = true;
    [Range(0, 100)]
    public int CreateChance = 60;

    [Space]
    public bool EnableChildrensDestroy = false;
    [Range(1, 10)]
    public int ChildrensLeft = 1;

    [Space]
    public bool EnableAlbedoRandom = false;
    public Color[] Colors;

    private void Start()
    {
        if (EnableChance && Random.Range(0, 100) > CreateChance) Destroy(gameObject);

        if (EnableChildrensDestroy) StartCoroutine(ChildrenDestroy());

        if (EnableAlbedoRandom && Colors.Length > 0) GetComponent<Renderer>().material.SetColor("_Color", Colors[Random.Range(0, Colors.Length)]);
    }

    IEnumerator ChildrenDestroy()
    {
        int lim = 100;
        while (gameObject.transform.childCount > ChildrensLeft && lim > 0)
        {
            lim--;

            Destroy( gameObject.transform.GetChild(Random.Range(0, gameObject.transform.childCount)).gameObject );

            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);

            yield return new WaitForEndOfFrame();
        }
    }
}
