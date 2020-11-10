using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Map : MonoBehaviour
{

    // private GridMap<int> m_gridMap;

    [SerializeField] private TileMapVisual tileMapVisual;

    TileMap m_tileMap;

    // Start is called before the first frame update
    void Start()
    {
        // m_gridMap = new GridMap<int>(50, 50, 1f , (GridMap<int> g , int x, int y) => { return 0; });

        m_tileMap = new TileMap(50, 50, 1f);


        m_tileMap.setTileMapVisual(tileMapVisual);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            vec.z = 0f;

            Debug.Log(vec);

            m_tileMap.setTileMapSprite(vec, TileMap.TileMapObject.TileMapSprite.Ground);
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    vec.z = 0f;

        //    Debug.Log(vec);

        //    Debug.Log(m_gridMap.getGridObject(vec));
        //}
    }

}
