using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControls : MonoBehaviour
{
    public Map map;


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
