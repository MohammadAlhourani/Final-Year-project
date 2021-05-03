using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to create and display a vision cone using raycasts
//
public class FieldOfView : MonoBehaviour
{
    //mesh for the visual representation of the vision cone
    Mesh mesh;

    //the field of view
    float fov;

    //the starting angle of the vision cone
    float startingAngle;

    //starting point of the vision cone
    Vector3 origin;

    //gameobjetc in the vision cone
    List<GameObject> gameObjects;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        origin = Vector3.zero;

        fov = 90f;

        gameObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        int rayCount = 25;

        float currentAngle = startingAngle;

        float angleIncrease = fov / rayCount;

        float viewDistance = 50f;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];

        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexindex = 1;
        int triangleindex = 0;

        gameObjects.Clear();

        for (int i = 0; i <= rayCount; i++)
        {           

            Vector3 vertex = Vector3.zero;

            int layerMask = ~(LayerMask.GetMask("Enemy"));

            RaycastHit2D raycastHit = Physics2D.Raycast(origin, GetVector3Fromangle(currentAngle), viewDistance, layerMask);


            if (raycastHit.collider == null)
            {
                vertex = origin + GetVector3Fromangle(currentAngle) * viewDistance;
            }
            else
            {
               vertex = raycastHit.point;

               gameObjects.Add(raycastHit.collider.gameObject);                
            }

            vertices[vertexindex] = vertex;

            if (i > 0)
            {
                triangles[triangleindex + 0] = 0;
                triangles[triangleindex + 1] = vertexindex - 1;
                triangles[triangleindex + 2] = vertexindex;

                triangleindex += 3;
            }

            vertexindex++;
            currentAngle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    //gets a vector3 from an angle
    public Vector3 GetVector3Fromangle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);

        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    //gets and angle from a vector
    public float getAngleFromVector(Vector3 vector)
    {
        vector = vector.normalized;

        float rotation = ((Mathf.Atan2(-vector.x, vector.y)) * (180.0f / 3.14f) + 180);

        return rotation;
    }

    //sets the origin of the vision cone
    public void setOrigin(Vector3 t_origin)
    {
        origin = t_origin;
    }

    //sets the direction of the vision cone
    public void setDirection(Vector3 t_direction)
    {
        startingAngle = getAngleFromVector(t_direction) - fov / 2f;
    }

    //gets the list of game objects
    public List<GameObject> GetGameObjects()
    {
        return gameObjects;
    }
}
