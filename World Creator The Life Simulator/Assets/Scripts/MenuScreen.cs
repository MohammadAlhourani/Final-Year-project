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

    public void Quit()
    {
        Application.Quit();
    }


}
