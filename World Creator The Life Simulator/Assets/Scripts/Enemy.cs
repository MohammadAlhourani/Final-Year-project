using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private BNode topNode;

    [SerializeField] private float detectionRange = 0;
    [SerializeField] private GameObject target;

    private List<GameObject> m_gameObjects;

    public override void OnStarting()
    {
        m_gameObjects = new List<GameObject>();

        constructBehaviourTree();
    }

    public override void OnUpdate()
    {
        detectObjects();

        topNode.Evaluate();

        if (topNode.GetNodeState() == NodeState.Failure)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }

        DrawEllipse(gameObject.transform.position, Vector3.forward, Vector3.up, detectionRange, detectionRange, 30, new Color(255, 0, 0), 0.1f);       
    }

    private void constructBehaviourTree()
    {
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold);

        Inverter invertHealth = new Inverter(healthNode);

        RangeNode Range = new RangeNode(detectionRange, target.transform, gameObject.transform);

        ChaseNode Chase = new ChaseNode(target.transform, gameObject.transform, 0.1f);

        Sequence ChaseSequence = new Sequence(new List<BNode> { invertHealth, Range, Chase });

        topNode = new Selector(new List<BNode> { ChaseSequence });
    }

    private static void DrawEllipse(Vector3 pos, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, Color color, float duration = 0)
    {
        float angle = 0f;
        Quaternion rot = Quaternion.LookRotation(forward, up);
        Vector3 lastPoint = Vector3.zero;
        Vector3 thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }
    }

    private void detectObjects()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, detectionRange);

        m_gameObjects.Clear();

        for(int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject != gameObject)
            {
                m_gameObjects.Add(hitColliders[i].gameObject);
            }
        }

        foreach(var gameobject in m_gameObjects)
        { 
            if(gameobject.CompareTag("Human"))
            {
                target = gameobject;

                constructBehaviourTree();
                break;
            }

        }
    }
}
