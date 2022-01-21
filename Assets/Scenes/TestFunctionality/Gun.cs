using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 10f;


    float caricatore = 6;

    public Camera fpsCam;
    public ParticleSystem muzzleflash;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (caricatore > 0)
            {
                Shoot();
                caricatore--;
            }
            else
            {
                Reload();
            }
            
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        muzzleflash.Play();
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            Target nemico = hit.transform.GetComponent<Target>();

            if(nemico != null)
            {
                nemico.TakeDamage(damage);
            }
        }

       
    }

    void Reload()
    {
        Debug.Log("Ricarico");
        caricatore = 6;
    }
}
