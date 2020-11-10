using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TileMap 
{
    private GridMap<TileMapObject> m_gridMap;

    public TileMap(int t_width, int t_height, float t_cellsize)
    {
        m_gridMap = new GridMap<TileMapObject>(t_width, t_height, t_cellsize, (GridMap<TileMapObject> g, int x, int y) =>  new TileMapObject(g,x,y));
    }


    public void setTileMapSprite(Vector3 worldPos , TileMapObject.TileMapSprite tileMapSprite)
    {
        TileMapObject tilemapObject = m_gridMap.getGridObject(worldPos);

        if(tilemapObject != null)
        {
            tilemapObject.setTileMapSprite(tileMapSprite);
        }

    }

    public void setTileMapVisual(TileMapVisual tileMapVisual)
    {
        tileMapVisual.setGrid(this.m_gridMap);
    }


    public class TileMapObject
    {
        public enum TileMapSprite
        {
            None,
            Ground
        }


        private GridMap<TileMapObject> grid;
        private int x;
        private int y;

        private TileMapSprite tileMapSprite;

        public TileMapObject(GridMap<TileMapObject> g , int x , int y)
        {
            this.grid = g;
            this.x = x;
            this.y = y;
        }

        public void setTileMapSprite(TileMapSprite t_tileSprite)
        {
            this.tileMapSprite = t_tileSprite;
            grid.triggerObjectChange(x, y);
        }


        public TileMapSprite  getTileMapSprite()
        {
            return tileMapSprite;
        }

    }
}
