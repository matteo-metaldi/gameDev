
using UnityEngine;
using UnityEngine.SceneManagement;
public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isScientis = false;
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
        if (isScientis)
        {
            Destroy(gameObject);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(sceneName: "VictoryScreen"); ;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
