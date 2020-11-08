using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Map : MonoBehaviour
{

    private GridMap m_gridMap;
    // Start is called before the first frame update
    void Start()
    {
         m_gridMap = new GridMap(10, 10, 10f);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
            vec.z = 0f;

            Debug.Log(vec);

            m_gridMap.setValue(vec, 10);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            vec.z = 0f;

            Debug.Log(vec);

            Debug.Log(m_gridMap.getValue(vec));
        }
    }

}
