using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
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


                Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
                Debug.DrawRay(transform.position, forward, Color.green);
                PlayerVita nemico = hit.transform.GetComponent<PlayerVita>();

                if (caricatore > 0 && nemico != null)
                {

                    Shoot();
                    caricatore--;
                }
                else if(caricatore == 0)
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

            PlayerVita nemico = hit.transform.GetComponent<PlayerVita>();
            
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
