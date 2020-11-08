using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class GridMap
{
    //the width of the array (x value)
    private int m_width;

    //the height of the array (y value)
    private int m_height;

    private float m_cellSize;

    //the map array
    private int[,] m_gridarray;

    Color green = new Color(0, 255, 0);

    //constructor for a new map
    public GridMap(int t_x, int t_y , float t_cellsize)
    {
        this.m_width = t_x;
        this.m_height = t_y;
        this.m_cellSize = t_cellsize;

        m_gridarray = new int[m_width, m_height];

        for(int x = 0; x < m_gridarray.GetLength(0); x++ )
        {
            for(int y = 0; y < m_gridarray.GetLength(1); y++)
            {

                Debug.DrawLine(worldPos(x, y), worldPos(x , y + 1) , green , 100f);

                Debug.DrawLine(worldPos(x, y), worldPos(x + 1, y), green, 100f);

            }
        }

        Debug.DrawLine(worldPos(m_width, 0), worldPos(m_width , m_height), green, 100f);

        Debug.DrawLine(worldPos(0 ,m_height), worldPos(m_width, m_height), green, 100f);
    }



    //returns the position of a cell in the world for the array
    private Vector3 worldPos(int t_x, int t_y)
    {
        return (new Vector3(t_x, t_y , 0) * m_cellSize);
    }


    //returns x,y depending on the world pos
    private void getXY(Vector3 t_position , out int t_x , out int t_y)
    {
        t_x = Mathf.FloorToInt(t_position.x / m_cellSize);
        t_y = Mathf.FloorToInt(t_position.y / m_cellSize);
    }

    private void setValue(int t_x, int t_y , int t_value)
    {
        if (t_x >= 0 && t_y >= 0 && t_x < m_width && t_y < m_height)
        {
            m_gridarray[t_x, t_y] = t_value;
        }
    }

    public void setValue(Vector3 t_pos, int t_value)
    {
        int x;
        int y;

        getXY(t_pos, out x, out y);

        setValue(x, y, t_value);
    }

    public int getValue(int t_x, int t_y)
    {
        if (t_x >= 0 && t_y >= 0 && t_x < m_width && t_y < m_height)
        {
            return m_gridarray[t_x, t_y];
        }
        else
        {
            return 0;
        }
    }

    public int getValue(Vector3 t_pos)
    {
        int x;
        int y;

        getXY(t_pos, out x, out y);

        Debug.Log(x + " " + y);

        return m_gridarray[x, y];
    }
}
