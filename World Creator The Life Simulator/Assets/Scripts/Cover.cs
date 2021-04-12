using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] private Transform[] coverSpots;

    Map m_map;

    public Transform[] GetCoverSpots()
    {
        return coverSpots;
    }

}
