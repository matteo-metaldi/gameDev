using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeMenu : MonoBehaviour
{

    public Button yourButton;
    
    // Use this for initialization
    void Start()
    {

        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(changeScene);
   
    }

    public void changeScene()
    {
        Debug.Log(yourButton.name);

        switch (yourButton.name)
        {

            //MAIN MENU
            case "Play":
                SceneManager.LoadScene(sceneName: "MenuPlay");
                break;
            case "Tutorial":
                //SceneManager.LoadScene(sceneName: "MenuPlay");
                break;
            case "Settings":
                //SceneManager.LoadScene(sceneName: "MenuPlay");
                break;
            case "Exit":
                Application.Quit();
                break;

            //MENU PLAY
            case "NewGame":
                Debug.Log("ENTRO E CARICO");
                SceneManager.LoadScene(sceneName: "1_LaboOff");
                Debug.Log("BORGO BIO");
                break;
            case "Load":
                //SceneManager.LoadScene(sceneName: "MenuPlay");
                break;
            case "Achievements":
                //SceneManager.LoadScene(sceneName: "MenuPlay");
                break;
            case "Back":
                SceneManager.LoadScene(sceneName: "MainMenu");
                break;

            default:
                break;
        }

    }
}
