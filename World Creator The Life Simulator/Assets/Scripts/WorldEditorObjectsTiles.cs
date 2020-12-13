using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEditorObjectsTiles : MonoBehaviour
{
    public GameObject m_map;

    public void Grass()
    {
         m_map.GetComponent<Map>().setTileMapSprite(TileMap.TileMapObject.TileMapSprite.Grass);
    }

    public void Ground()
    {
        m_map.GetComponent<Map>().setTileMapSprite(TileMap.TileMapObject.TileMapSprite.Ground);
    }

    public void Sand()
    {
        m_map.GetComponent<Map>().setTileMapSprite(TileMap.TileMapObject.TileMapSprite.Sand);
    }

    public void Path()
    {
        m_map.GetComponent<Map>().setTileMapSprite(TileMap.TileMapObject.TileMapSprite.Path);
    }
}
