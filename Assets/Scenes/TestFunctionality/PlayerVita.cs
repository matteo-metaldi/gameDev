using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerVita : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Object;
    // Start is called before the first frame update
    public float healt = 50;

    public void TakeDamage(float amount)
    {
        healt -= amount;
        Debug.Log("SONO COLPITO");
        m_Object.text = healt.ToString();
        if (healt <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(sceneName: "GameOver");
        
    }
}
