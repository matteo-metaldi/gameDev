using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendShooter : MonoBehaviour
{
    public float damage = 10f;
    public float range = 10f;

    public float attackRate = 0.5f;
    public Transform firePoint;


    float fireRate = 0f;
    float reloadTime = 7f;

    float caricatore = 60;

    public Transform fpsCam;
    public ParticleSystem muzzleflash;

    public AudioClip reloadGun;
    public AudioClip shootGun;
    public AudioSource soundSource;

    private void Update()
    {
        if (Time.time > fireRate)
        {
            fireRate = Time.time + attackRate;

            //Attack
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit))
            {

                Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * 1000, Color.cyan);

                Target nemico = hit.transform.GetComponent<Target>();

                if (caricatore > 0 && nemico!=null)
                {

                    Shoot();
                    caricatore--;
                }
                else if(caricatore==0)
                {
                    Reload();
                }

            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        muzzleflash.Play();
        soundSource.PlayOneShot(shootGun);
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            //Debug.Log(hit.transform.name);

            Target nemico = hit.transform.GetComponent<Target>();

            if (nemico != null)
            {
                nemico.TakeDamage(damage);
            }
        }

    }

    void Reload()
    {
        //Debug.Log("Ricarico");
        soundSource.PlayOneShot(reloadGun);
        if (Time.time > reloadTime)
        {
            reloadTime = Time.time + 7f;
            caricatore = 60;
        }
    }
}
