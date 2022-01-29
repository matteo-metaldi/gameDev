using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Object;
    public float damage = 10f;
    public float range = 10f;
    public AudioClip reloadRevolver;
    public AudioClip shootRevolver;
    public AudioSource soundSource;

    public bool isMelee = false;
    public bool isShotgun = false;

    float fireRate = 0f;
    float reloadTime = 3f;

    public int caricatore = 6;

    bool canfire = false;

    public Camera fpsCam;
    public ParticleSystem muzzleflash;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isMelee)
            {
                if (canfire)
                {
                    if (caricatore > 0)
                    {
                        Shoot();
                        m_Object.text = caricatore.ToString();

                    }
                    else
                    {
                        canfire = false;
                        m_Object.text = caricatore.ToString();
                        Reload();

                    }
                }
            }
            else
            {
                m_Object.text = "Coltello";
                Attack();
            }
            
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Time.time > fireRate)
        {
            fireRate = Time.time + 1f;
            caricatore--;
            muzzleflash.Play();
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                soundSource.PlayOneShot(shootRevolver);
                Target nemico = hit.transform.GetComponent<Target>();

                if (nemico != null)
                {
                    nemico.TakeDamage(damage);
                }

               
            }

            

        }

       
    }

    void Attack()
    {
        RaycastHit hit;
        if (Time.time > fireRate)
        {
            fireRate = Time.time + 1f;
            
          
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                soundSource.PlayOneShot(shootRevolver);

                if (hit.distance < 500)
                {
                    Target nemico = hit.transform.GetComponent<Target>();

                    if (nemico != null)
                    {
                        nemico.TakeDamage(damage);
                    }
                }
            }

        }
    }

    void Reload()
    {
        if (!isShotgun)
        {
            soundSource.PlayOneShot(reloadRevolver);
            if (Time.time > reloadTime)
            {

                reloadTime = Time.time + 3f;
                canfire = true;
                caricatore = 6;
            }
        }
        else
        {
            canfire = true;
            caricatore = 6;
        }
    }


    public void ActivateWeapon(bool activate)
    {
        StopAllCoroutines();
        canfire = true;
        gameObject.SetActive(activate);
    }
}
