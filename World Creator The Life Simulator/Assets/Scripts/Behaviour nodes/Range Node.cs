using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : BNode
{

    private float m_range;
    private Transform m_target;
    private Transform m_origin;

    public RangeNode(float t_range , Transform t_target, Transform t_origin)
    {
        this.m_range = t_range;
        this.m_target = t_target;
        this.m_origin = t_origin;
    }



   public override NodeState Evaluate()
   {
        if (m_target != null)
        {
            float distance = Vector3.Distance(m_target.position, m_origin.position);

            if (distance <= m_range)
            {
                return NodeState.Success;
            }
        }

        return NodeState.Failure;
   }

}
