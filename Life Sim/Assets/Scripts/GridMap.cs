using System.Collections;
using System.Collections.Generic;
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

    Color red = new Color(255, 0, 0);

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

                Debug.DrawLine(worldPos(x, y), worldPos(x , y + 1) , red , 100f);

                Debug.DrawLine(worldPos(x, y), worldPos(x + 1, y), red, 100f);

            }
        }

        Debug.DrawLine(worldPos(m_width, 0), worldPos(m_width , m_height), red, 100f);

        Debug.DrawLine(worldPos(0 ,m_height), worldPos(m_width, m_height), red, 100f);
    }



    //returns the position of a cell in the array
    private Vector3 worldPos(int t_x, int t_y)
    {
        return (new Vector3(t_x, t_y , 0) * m_cellSize);
    }

}
