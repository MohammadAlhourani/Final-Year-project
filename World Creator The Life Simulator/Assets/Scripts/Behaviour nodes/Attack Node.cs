using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : BNode
{
    private Transform m_target;
    private Character m_origin;

    public AttackNode(Transform t_target, Character t_origin)
    {
        this.m_target = t_target;
        this.m_origin = t_origin;
    }

    public override NodeState Evaluate()
    {
        Vector3 direction = m_target.position - m_origin.transform.position;

        direction = Vector3.Normalize(direction);

        m_origin.velocity = direction;

        return NodeState.Running;
    }

    public void setTarget(Transform t_target)
    {
        this.m_target = t_target;
    }
}
