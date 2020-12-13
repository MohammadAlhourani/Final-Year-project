using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEditor : MonoBehaviour
{
    public static bool WorldEditorActive = false;

    public GameObject WorldEditorUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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


    public void StarEditor()
    {
       WorldEditorUI.SetActive(true);
        WorldEditorActive = true;
    }

    public void ExitEditor()
    {
        WorldEditorUI.SetActive(false);
        WorldEditorActive = false;
    }
}
