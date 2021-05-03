using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inspection functions
public class MapControls : MonoBehaviour
{
    public Map map;

    //sets the inspection to true/false
    public void inspectObject()
    {
        if (map.InspectObject == false)
        {
            map.InspectObject = true;
            Debug.Log("inspect true");
        }
        else
        {
            map.InspectObject = false;
        }
    }    
}
