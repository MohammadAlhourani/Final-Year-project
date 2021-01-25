using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Map : MonoBehaviour
{
    public TileMapVisual tileMapVisual;

    public CameraMovement MainCamera;

    public int m_mapWidth = 10;

    public int m_mapHeight = 10;

    public float m_cellSize = 10.0f;

    private const int MOVE_STRAIGHT = 10;
    private const int MOVE_DIAGONAL = 14;

    private float zoom = 300f;

    TileMap m_tileMap;

    public TileMap tileMap
    {
        get { return m_tileMap; }
    }

    TileMap.TileMapObject.TileMapSprite m_currentTileSprite = TileMap.TileMapObject.TileMapSprite.Grass;

    public TileMap.TileMapObject.TileMapSprite currentTileSprite
    { 
        get { return m_currentTileSprite; }
        set { m_currentTileSprite = value; }
    }

    public GameObject m_currentGameObject;

    public GameObject currentGameObject
    {
        get { return m_currentGameObject; }
        set { m_currentGameObject = value; }
    }

    public bool destroyObject = false;

    private List<TileMap.TileMapObject> openList;
    private List<TileMap.TileMapObject> closedList;

    Color red = new Color(255, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        m_tileMap = new TileMap(m_mapWidth, m_mapHeight, m_cellSize);


        m_tileMap.setTileMapVisual(tileMapVisual);

          MainCamera.Setup(() => new Vector3(25, 25, -10), () => zoom);
       // MainCamera.SetCameraFollowPos(() => new Vector3(25, 25, -10));

    }

    void Update()
    {
        if(PauseMenu.GamePaused == false)
        {
            handleZoom();
        }
        
        if (PauseMenu.GamePaused == false && WorldEditor.WorldEditorActive == true)
        {
            if (Input.GetMouseButton(0))
            {
                if (m_currentTileSprite != TileMap.TileMapObject.TileMapSprite.None)
                {
                    Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    vec.z = 0f;

                    m_tileMap.setTileMapSprite(vec, m_currentTileSprite);
                }

                if (destroyObject == false)
                {
                    if (m_currentGameObject.tag != "Default")
                    {
                        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        vec.z = 0f;

                        TileMap.TileMapObject tilemapObject = m_tileMap.GetTileMapObject(vec);

                        if (tilemapObject != null && tilemapObject.containsObject == false)
                        {
                            tilemapObject.passable = false;
                            tilemapObject.containsObject = true;

                            Vector3 objectpos = m_tileMap.GetGridMap().worldPos(tilemapObject.x, tilemapObject.y);

                            GameObject currentObjectSpawn = Instantiate(m_currentGameObject, objectpos, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    GameObject objectAtMouse = GetObjectAtMousePos();

                    if(objectAtMouse != null)
                    {
                        Destroy(objectAtMouse);
                    }
                }

            }

            if (Input.GetMouseButtonDown(1))
            {
                Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                vec.z = 0f;

                m_tileMap.GetGridMap().getXY(vec, out int x, out int y);

                List<TileMap.TileMapObject> path = getPathAStar(0, 0, x, y);

                if (path != null)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Debug.DrawLine(new Vector3(path[i].x + 0.5f, path[i].y + 0.5f), new Vector3(path[i + 1].x + 0.5f, path[i + 1].y + 0.5f), red, 100f);

                        Debug.Log(path[i].x + " " + path[i].y);
                    }
                }

            }
        }
    }


    public void setTileMapSprite(TileMap.TileMapObject.TileMapSprite mapSprite)
    {
        currentTileSprite = mapSprite;
    }


    public List<TileMap.TileMapObject> getPathAStar(int startX, int startY, int endX, int endY)
    {
       TileMap.TileMapObject startNode = getObject(startX, startY);
       TileMap.TileMapObject endNode = getObject(endX, endY); 
        
       
       openList = new List<TileMap.TileMapObject> { startNode };
       closedList = new List<TileMap.TileMapObject>();

        for(int x = 0; x < m_tileMap.GetGridMap().getWidth(); x++)
        {
            for(int y = 0; y < m_tileMap.GetGridMap().getHeight(); y++)
            {
                TileMap.TileMapObject pathNode = m_tileMap.GetGridMap().getGridObject(x, y);

                pathNode.m_pathCost = int.MaxValue;
                pathNode.calFcost();
                pathNode.m_previous = null;

            }
        }

        startNode.m_pathCost = 0;
        startNode.m_hueristic = calculateHeuristic(startNode, endNode);
        startNode.calFcost();

        while(openList.Count > 0)
        {
            TileMap.TileMapObject currentNode = lowestPathNode(openList);

            if(currentNode == endNode)
            {
                return calPath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (TileMap.TileMapObject neighbourNode in getNeighBours(currentNode))
            {

                if(closedList.Contains(neighbourNode))
                {
                    continue;
                }

                int tentativeGCost = currentNode.m_pathCost + calculateHeuristic(currentNode, neighbourNode);

                if(tentativeGCost < neighbourNode.m_pathCost)
                {
                    neighbourNode.m_previous = currentNode;
                    neighbourNode.m_pathCost = tentativeGCost;
                    neighbourNode.m_hueristic = calculateHeuristic(neighbourNode, endNode);
                    neighbourNode.calFcost();

                    if(openList.Contains(neighbourNode) == false)
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }

        }

        return null;

    }

    private List<TileMap.TileMapObject> getNeighBours(TileMap.TileMapObject Node)
    {
        List<TileMap.TileMapObject> neighbourNodes = new List<TileMap.TileMapObject>();


        if(Node.x - 1 >= 0)
        {
            //left 
            neighbourNodes.Add(getObject(Node.x - 1, Node.y));

            //left down
            if(Node.y - 1 >= 0)
            {
                neighbourNodes.Add(getObject(Node.x - 1, Node.y - 1));
            }

            //left up
            if(Node.y + 1 < m_tileMap.GetGridMap().getHeight())
            {
                neighbourNodes.Add(getObject(Node.x - 1, Node.y + 1));
            }

        }

        if(Node.x + 1 < m_tileMap.GetGridMap().getWidth())
        {
            //right
            neighbourNodes.Add(getObject(Node.x + 1, Node.y));

            //right down
            if (Node.y - 1 >= 0)
            {
                neighbourNodes.Add(getObject(Node.x + 1, Node.y - 1));
            }

            //right up
            if (Node.y + 1 < m_tileMap.GetGridMap().getHeight())
            {
                neighbourNodes.Add(getObject(Node.x + 1, Node.y + 1));
            }

        }

        //down
        if(Node.y - 1 >= 0)
        {
            neighbourNodes.Add(getObject(Node.x, Node.y - 1));
        }

        //up
        if (Node.y + 1 < m_tileMap.GetGridMap().getHeight())
        {
            neighbourNodes.Add(getObject(Node.x, Node.y + 1));
        }

        return neighbourNodes;
    }


    private TileMap.TileMapObject getObject(int t_x, int t_y)
    {
        return m_tileMap.GetGridMap().getGridObject(t_x, t_y);
    }


    private List<TileMap.TileMapObject> calPath(TileMap.TileMapObject endNode)
    {
        List<TileMap.TileMapObject> PathToNode = new List<TileMap.TileMapObject>();

        PathToNode.Add(endNode);

        TileMap.TileMapObject currentNode = endNode;

        while (currentNode.m_previous != null)
        {
            PathToNode.Add(currentNode.m_previous);

            currentNode = currentNode.m_previous;
        }

        PathToNode.Reverse();

        return PathToNode;
    }

    private int calculateHeuristic(TileMap.TileMapObject nodeA , TileMap.TileMapObject nodeB)
    {
        int xDistance = Mathf.Abs(nodeA.getX() - nodeB.getX());
        int yDistance = Mathf.Abs(nodeA.getY() - nodeB.getY());

        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT * remaining;
    }

    private TileMap.TileMapObject lowestPathNode(List<TileMap.TileMapObject> t_pathnode)
    {
        TileMap.TileMapObject lowestNode = t_pathnode[0];

        for(int i = 1; i < t_pathnode.Count; i++)
        {
            if(t_pathnode[i].m_fCost < lowestNode.m_fCost)
            {
                lowestNode = t_pathnode[i];
            }

        }

        return lowestNode;

    }


    private void handleZoom()
    {
        float zoomChangeAmount = 80f;

        if(Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomChangeAmount * Time.deltaTime * 10f;
        }
        
        if(Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomChangeAmount * Time.deltaTime * 10f;
        }

        zoom = Mathf.Clamp(zoom, 20f, 300f);
    }

    GameObject GetObjectAtMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit2 = Physics2D.Raycast(mousePos, Camera.main.transform.position - mousePos, 0);
              

        if (hit2.collider != null)
        {
           return hit2.transform.gameObject;
           
        }
        else
        {
           return null;
        }
    }
}
