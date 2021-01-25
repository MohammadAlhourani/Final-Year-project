using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private float m_startingHealth;
    [SerializeField] public float m_lowHealthThreshold;   
    [SerializeField] private float m_healthRegenRate;

    [SerializeField] private GameObject m_map;

    private List<TileMap.TileMapObject> m_path;

    protected float m_currentHealth;

    protected Vector3 m_velocity;

    public float currentHealth
    {
        get { return m_currentHealth; }
        set { m_currentHealth = Mathf.Clamp(value, 0, m_startingHealth); }
    }

    public Vector3 velocity
    {
        get { return m_velocity; }    
        set { m_velocity = value; }
    }


    private void Start()
    {
        currentHealth = m_startingHealth;
        m_velocity = Vector3.zero;
        OnStarting();
    }

    private void Update()
    {
        OnUpdate();
        Orientation(velocity);
    }

    public abstract void OnStarting();
    public abstract void OnUpdate();

    public float Orientation(Vector3 t_direction)
    {
        float rotation = ((Mathf.Atan2(-t_direction.x, t_direction.y)) * (180.0f / 3.14f));

        transform.rotation = Quaternion.Euler(Vector3.forward * (rotation + 180));

        return rotation;
    }

    public void pathFind(int t_x , int t_y)
    {
        m_path.Clear();

        m_map.GetComponent<Map>().tileMap.GetGridMap().getXY(gameObject.transform.position,out int x,out int y);

        m_path = m_map.GetComponent<Map>().getPathAStar(x, y, t_x, t_y);
    }


    public void pathFind(Vector3 t_endPos)
    {
        m_path.Clear();

        m_map.GetComponent<Map>().tileMap.GetGridMap().getXY(gameObject.transform.position, out int x, out int y);

        m_map.GetComponent<Map>().tileMap.GetGridMap().getXY(t_endPos, out int t_x, out int t_y);

        m_path = m_map.GetComponent<Map>().getPathAStar(x, y, t_x, t_y);
    }

    public boundary getBoundary()
    {
       return m_map.GetComponent<Map>().tileMap.getBoundary();
    }
}
