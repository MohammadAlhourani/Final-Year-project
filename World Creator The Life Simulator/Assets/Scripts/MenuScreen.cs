using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{


    public void NewWorld()
    {
       SceneManager.LoadScene("WorldScene");
    }

    public void VanillaWorld()
    {
        SceneManager.LoadScene("VanillaScene");
    }

    public void FSMWorld()
    {
        SceneManager.LoadScene("FSMScene");
    }

    public void CNMWorld()
    {
        SceneManager.LoadScene("CNMScene");
    }

    public void Quit()
    {
        Application.Quit();
    }


}
