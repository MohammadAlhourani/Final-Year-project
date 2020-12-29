using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : BNode
{
    private Transform m_target;
    private Transform m_oirgin;

    private float m_speed;

    public ChaseNode(Transform t_target , Transform t_origin , float t_speed = 1)
    {
        this.m_target = t_target;
        this.m_oirgin = t_origin;
        this.m_speed = t_speed;
    }

    public override NodeState Evaluate()
    {
        Vector3 direction = m_target.position - m_oirgin.position;

        direction = Vector3.Normalize(direction) * m_speed;

        m_oirgin.position += direction;

        return NodeState.Running;
    }
}
