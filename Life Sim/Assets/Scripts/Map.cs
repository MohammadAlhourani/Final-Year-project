using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private TileMapVisual tileMapVisual;

    TileMap m_tileMap;

    TileMap.TileMapObject.TileMapSprite currentTileSprite = TileMap.TileMapObject.TileMapSprite.Grass;

    // Start is called before the first frame update
    void Start()
    {
        m_tileMap = new TileMap(50, 50, 1f);


        m_tileMap.setTileMapVisual(tileMapVisual);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            vec.z = 0f;

            Debug.Log(vec);

            m_tileMap.setTileMapSprite(vec, currentTileSprite);
        }


        if(Input.GetButton("Button1"))
        {
            currentTileSprite = TileMap.TileMapObject.TileMapSprite.Grass;
        }
        else if(Input.GetButton("Button2"))
        {
            currentTileSprite = TileMap.TileMapObject.TileMapSprite.Ground;
        }
        else if (Input.GetButton("Button3"))
        {
            currentTileSprite = TileMap.TileMapObject.TileMapSprite.Sand;
        }
        else if (Input.GetButton("Button4"))
        {
            currentTileSprite = TileMap.TileMapObject.TileMapSprite.Path;
        }

    }

}
