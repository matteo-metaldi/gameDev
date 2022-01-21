
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float healt = 50;

    public void TakeDamage(float amount)
    {
        healt -= amount;

        if (healt <= 0f)
        {
            Die();    
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
