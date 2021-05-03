using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the base character class
public abstract class Character : MonoBehaviour
{
    //the starting health of the charcter
    [SerializeField] private float m_startingHealth;
    //the threshhold where the charcter will be consiidered at low health
    [SerializeField] public float m_lowHealthThreshold;

    //how much the charcter will regain health
    [SerializeField] protected float m_healthRegenRate;

    //the spee of the character
    [SerializeField] protected float m_speed;

    //the detection range of the charcter affects the size of the vision cone
    [SerializeField] protected float m_detectionRange = 0;

    // the map the charcter is on
    [SerializeField] private GameObject m_map;

    // the charcters healthbar for displaying their health
    [SerializeField] private UnitHealthBar m_healthBar;

    //the stats of the charcters behaviour tree
    [SerializeField] public TreeStats m_stats;


    //a list of all the cover spots around the charcter
    private List<Cover> m_covers;

    //the cover spot that is the best for the charcter to hide at
    private Transform m_bestCoverSpot;

    //a path for the charcter to move along
    private List<TileMap.TileMapObject> m_path;

    //the charcter current health amount
    protected float m_currentHealth;

    //the direction the charcter is looking at
    protected Vector3 m_direction;

    //get and set for the charcter current health 
    //clamps the value between 0 and the starting amount of health 
    public float currentHealth
    {
        get { return m_currentHealth; }
        set { m_currentHealth = Mathf.Clamp(value, 0, m_startingHealth); }
    }

    // get for the behaviour tree stats
    public TreeStats stats
    {
        get { return m_stats; }
    }

    //get and set for the character direction
    public Vector3 velocity
    {
        get { return m_direction; }
        set { m_direction = value; }
    }

    //get for the charcter path
    public List<TileMap.TileMapObject> path
    {
         get { return m_path; }
    }

    //the start function
    //initilaises values here
    private void Start()
    {
        m_covers = new List<Cover>();
        currentHealth = m_startingHealth;
        m_direction = Vector3.zero;
        m_path = new List<TileMap.TileMapObject>();

        m_healthBar.setMaxHealth(m_startingHealth);

        OnStarting();
    }

    //the update 
    //ensures the charcter is facing the direction they move
    private void Update()
    {
        OnUpdate();
        Orientation(velocity);
    }

    //the start update function for theclasses that inherit from this base class
    public abstract void OnStarting();
    public abstract void OnUpdate();

    //sets the direction of the character
    public float Orientation(Vector3 t_direction)
    {
        float rotation = ((Mathf.Atan2(-t_direction.x, t_direction.y)) * (180.0f / 3.14f));

        transform.rotation = Quaternion.Euler(Vector3.forward * (rotation + 180));

        return rotation;
    }

    //pathfinds to a node based on the nods x and y values
    public void pathFind(int t_x , int t_y)
    {
        m_path.Clear();

        m_map.GetComponent<Map>().tileMap.GetGridMap().getXY(gameObject.transform.position,out int x,out int y);

        m_path = m_map.GetComponent<Map>().getPathAStar(x, y, t_x, t_y);
    }


    //pathfind based on a position in the worldspace
    public void pathFind(Vector3 t_endPos)
    {
        m_path.Clear();

        m_map.GetComponent<Map>().tileMap.GetGridMap().getXY(gameObject.transform.position, out int x, out int y);

        m_map.GetComponent<Map>().tileMap.GetGridMap().getXY(t_endPos, out int t_x, out int t_y);

        m_path = m_map.GetComponent<Map>().getPathAStar(x, y, t_x, t_y);
    }

    //the boundary of the map so the character does not move out of world space
    public boundary getBoundary()
    {
       return m_map.GetComponent<Map>().tileMap.getBoundary();
    }
       
    //sets the best cover spot for the charcter to hide in
    public void SetBestCover(Transform t_cover)
    {
        m_bestCoverSpot = t_cover;
    }

    //return the best cover spot
    public Transform GetBestCover()
    {
        return m_bestCoverSpot;
    }

    // gets all the cover spot around the character
    public Cover[] GetCoverAround()
    {
        CoverAround();

        if (m_covers.Count > 0)
        {
            Cover[] covers = new Cover[m_covers.Count];

            for (int i = 0; i < covers.Length; i++)
            {
                covers[i] = m_covers[i];
            }

            return covers;
        }
        else
        {
            return new Cover[0];
        }
    }

    //finds all the cover spot around the character
    private void CoverAround()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, m_detectionRange);

        m_covers.Clear();

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject != gameObject)
            {
                if (hitColliders[i].gameObject.CompareTag("Wall") == true)
                {
                    m_covers.Add(hitColliders[i].gameObject.GetComponent<Cover>());
                }
            }
        }
    }

    //damage to the charcters health
    //updates the health bar value here
    public void Damage(float t_damage)
    {
        if (currentHealth > 0)
        {
            m_currentHealth -= t_damage;

            m_healthBar.setCurrentHealth(m_currentHealth);
        }
    }
}
