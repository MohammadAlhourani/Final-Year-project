using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : BNode
{
    private Transform m_target;
    private Character m_origin;

    private float m_speed;

    public ChaseNode(Transform t_target , Character t_origin , float t_speed = 1)
    {
        this.m_target = t_target;
        this.m_origin = t_origin;
        this.m_speed = t_speed;
    }

    public override NodeState Evaluate()
    {
        if (m_target != null)
        {
            Vector3 direction = m_target.position - m_origin.transform.position;

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

    public void setTarget(Transform t_target)
    {
        this.m_target = t_target;
    }
}
