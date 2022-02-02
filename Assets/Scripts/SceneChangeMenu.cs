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
            case "Play":
                Cursor.visible = true;
                SceneManager.LoadScene(sceneName: "MenuPlay");
                break;
            case "Tutorial":
                Cursor.visible = true;
                SceneManager.LoadScene(sceneName: "Tutorial");
                break;
            case "Exit":
                Application.Quit();
                break;

            //MENU PLAY
            case "NewGame":
                Cursor.visible = true;
                SceneManager.LoadScene(sceneName: "initialRoom");
                break;
            case "Back to menu":
            case "Back":
                Cursor.visible = true;
                SceneManager.LoadScene(sceneName: "MainMenu");
                break;
            case "End Credits":
                SceneManager.LoadScene(sceneName: "Credits");
                break;

            case "NextBtn":
                SceneManager.LoadScene(sceneName: "1_LaboOff");
                break;
            default:
                break;
        }

    }
}
