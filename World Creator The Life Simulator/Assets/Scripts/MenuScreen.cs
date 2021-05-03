using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//main menu functions
public class MenuScreen : MonoBehaviour
{
    public void NewWorld()
    {
       SceneManager.LoadScene("WorldScene");
    }

    //sets the scene to the vanilla behaviour tree
    public void VanillaWorld()
    {
        SceneManager.LoadScene("VanillaScene");
    }

    //sets the scene to the FSM behaviour tree
    public void FSMWorld()
    {
        SceneManager.LoadScene("FSMScene");
    }

    //sets the scene to the CNM behaviour tree
    public void CNMWorld()
    {
        SceneManager.LoadScene("CNMScene");
    }

    //quits the application
    public void Quit()
    {
        Application.Quit();
    }


}
