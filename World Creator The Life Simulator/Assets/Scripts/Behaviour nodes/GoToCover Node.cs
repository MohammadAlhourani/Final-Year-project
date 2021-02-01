using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Transform coverSpot = m_origin.GetBestCover();

        if(coverSpot != null)
        {
            Debug.Log("go to cover failed");

            Vector3 direction = coverSpot.position - m_origin.transform.position;

            direction = Vector3.Normalize(direction) * m_speed;

            m_origin.velocity = direction;

            m_origin.transform.position += direction;

            return NodeState.Running;
        }
        else
        {
            return NodeState.Failure;
        }        
    }
}
