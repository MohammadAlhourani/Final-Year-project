using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cover script for wall 
public class Cover : MonoBehaviour
{
    //list of the objects cover spots
    [SerializeField] private Transform[] coverSpots;

    //the map object
    Map m_map;

    //return the walls coverspots
    public Transform[] GetCoverSpots()
    {
        return coverSpots;
    }

}
