using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//world editor functions
public class WorldEditor : MonoBehaviour
{
    //bool for if the world editor 
    public static bool WorldEditorActive = false;    

    //the world editor UI
    public GameObject WorldEditorUI;

    // Update is called once per frame
    void Update()
    {
        if (WorldEditorActive == true)
        {
            StarEditor();
        }
        else
        {
            ExitEditor();
        }
    }

    //activates the editor
    public void StarEditor()
    {
       WorldEditorUI.SetActive(true);
        WorldEditorActive = true;
    }

    //deactivates the editor
    public void ExitEditor()
    {
        WorldEditorUI.SetActive(false);
        WorldEditorActive = false;
    }
}
