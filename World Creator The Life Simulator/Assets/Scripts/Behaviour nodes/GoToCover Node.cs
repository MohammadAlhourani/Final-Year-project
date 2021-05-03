using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//go to cover behaviour
//pathfind the character to the best cover spot 
//and moves the character towards it
public class GoToCoverNode : BNode
{
    private Character m_origin;

    private float m_speed;

    public GoToCoverNode(Character t_origin, float t_speed = 1)
    {
        this.m_origin = t_origin;
        this.m_speed = t_speed;
    }

    public override NodeState Evaluate()
    {
        m_origin.stats.nodesEvaluatedincrease();
        m_origin.stats.actionsPerformedIncrease();

        Transform coverSpot = m_origin.GetBestCover();

        float distanceToSpot = Vector3.Distance(m_origin.transform.position, coverSpot.position);

        if (distanceToSpot <= 2)
        {
            return NodeState.Success;
        }

        if (coverSpot != null)
        {

            if (m_origin.path.Count == 0)
            {
                m_origin.pathFind(coverSpot.position);
            }           


            int layerMask = ~(LayerMask.GetMask("Enemy"));

            Vector3 LeftRay = m_origin.transform.position;
            Vector3 RightRay = m_origin.transform.position;


            LeftRay.x -= 2.7f;

            RightRay.x += 2.7f;

            if (Vector3.Distance(m_origin.path[0].GetWorldPos(), m_origin.transform.position) < 1)
            {
                m_origin.path.RemoveAt(0);
            }

            if (m_origin.path.Count > 0)
            {
                Debug.Log(m_origin.path[0].GetWorldPos());
                Vector3 direction = m_origin.path[0].GetWorldPos() - m_origin.transform.position;    

                direction = Vector3.Normalize(direction);               

                m_origin.velocity = direction;

                m_speed = 10;

                m_origin.transform.position += direction * m_speed * Time.deltaTime;

                if (distanceToSpot <= 2)
                {
                    return NodeState.Success;
                }

                return NodeState.Running;
            }
            else
            {
                return NodeState.Failure;
            }


        }
        else
        {
            return NodeState.Failure;
        }
        
    }
}
