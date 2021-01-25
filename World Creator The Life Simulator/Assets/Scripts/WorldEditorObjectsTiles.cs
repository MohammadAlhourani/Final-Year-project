using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEditorObjectsTiles : MonoBehaviour
{
    public Map m_map;

    public GameObject m_Default;

    public GameObject m_Wall;

    public void None()
    {
        m_map.setTileMapSprite(TileMap.TileMapObject.TileMapSprite.None);
        m_map.destroyObject = false;
        DefaultObject();
    }

    public void Grass()
    {
         m_map.setTileMapSprite(TileMap.TileMapObject.TileMapSprite.Grass);
        m_map.destroyObject = false;
        DefaultObject();
    }

    public void Ground()
    {
        m_map.setTileMapSprite(TileMap.TileMapObject.TileMapSprite.Ground);
        m_map.destroyObject = false;
        DefaultObject();
    }

    public void Sand()
    {
        m_map.setTileMapSprite(TileMap.TileMapObject.TileMapSprite.Sand);
        m_map.destroyObject = false;
        DefaultObject();
    }

    public void Path()
    {
        m_map.setTileMapSprite(TileMap.TileMapObject.TileMapSprite.Path);
        m_map.destroyObject = false;
        DefaultObject();
    }

    //Objects
    public void DefaultObject()
    {
        m_map.currentGameObject = m_Default;
        m_map.destroyObject = false;
    }

    public void DestroyObject()
    {
        m_map.destroyObject = true;
    }

    public void Wall()
    {
        None();
        m_map.destroyObject = false;
        m_map.currentGameObject = m_Wall;       
    }
}
