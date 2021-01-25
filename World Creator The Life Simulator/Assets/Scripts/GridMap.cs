using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public struct boundary
{
   public float k_north;
   public float k_south;
   public float k_east;
   public float k_west;
}

public class GridMap<TGridObject>
{



    public event EventHandler<onGridObjectChangeEventArgs> onGridObjectChange;
    public class onGridObjectChangeEventArgs : EventArgs
    {
        public int x;
        public int y;
    }


    //the width of the array (x value)
    private int m_width;

    //the height of the array (y value)
    private int m_height;

    private float m_cellSize;

    //the map array
    private TGridObject[,] m_gridarray;

    Color green = new Color(0, 255, 0);

    private boundary m_boundary;

    //constructor for a new map
    public GridMap(int t_x, int t_y , float t_cellsize, Func<GridMap<TGridObject>, int, int ,TGridObject> createGridObject)
    {
        this.m_width = t_x;
        this.m_height = t_y;
        this.m_cellSize = t_cellsize;

        m_gridarray = new TGridObject[m_width, m_height];

        CalcBoundary();

        //initilises the grid with default objects of its type
        for (int x = 0; x < m_gridarray.GetLength(0); x++)
        {
            for (int y = 0; y < m_gridarray.GetLength(1); y++)
            {
                m_gridarray[x, y] = createGridObject(this, x, y);
            }
        }

        //setGridDebugLine();
    }

    public int getWidth()
    {
        return m_width;
    }

    public int getHeight()
    {
        return m_height;
    }

    public float getCellSize()
    {
        return m_cellSize;
    }

    //returns the position of a cell in the world for the array
    public Vector3 worldPos(int t_x, int t_y)
    {
        return (new Vector3(t_x, t_y , 0) * m_cellSize);
    }

    public boundary getBoundary()
    {
        return m_boundary;
    }

    //returns x,y depending on the world pos
    public void getXY(Vector3 t_position , out int t_x , out int t_y)
    {
        t_x = Mathf.FloorToInt(t_position.x / m_cellSize);
        t_y = Mathf.FloorToInt(t_position.y / m_cellSize);
    }

    private void setGridObject(int t_x, int t_y , TGridObject t_value)
    {
        if (t_x >= 0 && t_y >= 0 && t_x < m_width && t_y < m_height)
        {
            m_gridarray[t_x, t_y] = t_value;

            if (onGridObjectChange != null) onGridObjectChange(this, new onGridObjectChangeEventArgs { x = t_x, y = t_y });
        }
    }

    public void setGridObject(Vector3 t_pos, TGridObject t_value)
    {
        int x;
        int y;

        getXY(t_pos, out x, out y);

        if (x >= 0 && y >= 0 && x < m_width && y < m_height)
        {
            setGridObject(x, y, t_value);
        }
    }


    public void triggerObjectChange(int t_x, int t_y)
    {
        if (onGridObjectChange != null) onGridObjectChange(this, new onGridObjectChangeEventArgs { x = t_x, y = t_y });
    }


    public TGridObject getGridObject(int t_x, int t_y)
    {
        if (t_x >= 0 && t_y >= 0 && t_x < m_width && t_y < m_height)
        {
            return m_gridarray[t_x, t_y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject getGridObject(Vector3 t_pos)
    {
        int x;
        int y;

        getXY(t_pos, out x, out y);

       // Debug.Log(x + " " + y);

        if (x >= 0 && y >= 0 && x < m_width && y < m_height)
        {
            return m_gridarray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    private void setGridDebugLine()
    {
        for (int x = 0; x < getWidth(); x++)
        {
            for (int y = 0; y < getHeight(); y++)
            {

                Debug.DrawLine(worldPos(x, y), worldPos(x, y + 1), green, 100f);

                Debug.DrawLine(worldPos(x, y), worldPos(x + 1, y), green, 100f);

            }
        }

        Debug.DrawLine(worldPos(m_width, 0), worldPos(m_width, m_height), green, 100f);

        Debug.DrawLine(worldPos(0, m_height), worldPos(m_width, m_height), green, 100f);
    }

    private void CalcBoundary()
    {
       m_boundary.k_north = worldPos(0, m_height).y;
        
       m_boundary.k_south = worldPos(0, 0).y;
       
       m_boundary.k_east = worldPos(m_width, 0).x;
        
       m_boundary.k_west = worldPos(0, 0).x;
       
    }
}
