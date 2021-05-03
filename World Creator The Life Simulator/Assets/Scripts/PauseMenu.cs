using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//pause screen functions
public class PauseMenu : MonoBehaviour
{
    //bool for if the game is paused
    public static bool GamePaused = false;

    //the pause screen UI
    public GameObject PauseMenuUI;

    //checks fo if the escape key is pressed to activate/deactivate the pause screen UI
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    //resume from the pause screen
    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePaused = false;
    }

    //activate the pasue screen
    public void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GamePaused = true;
    }

    //return to the main menu from the pause screen
    public void Menu()
    {
        GamePaused = false;
        Time.timeScale = 1.0f;
        WorldEditor.WorldEditorActive = false;
        SceneManager.LoadScene("Scenes/Menu");
    }

    //quit the application
    public void Quit()
    {
        Application.Quit();
    }
}
