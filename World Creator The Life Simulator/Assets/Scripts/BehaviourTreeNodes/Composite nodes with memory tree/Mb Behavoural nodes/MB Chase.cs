using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBChase : MBNode
{
    private Transform m_target;
    private Character m_origin;

    private float m_speed;

    public MBChase(Transform t_target, Character t_origin, float t_speed = 1)
    {
        this.m_target = t_target;
        this.m_origin = t_origin;
        this.m_speed = t_speed;

        m_type = NodeType.Action;
    }

    public override NodeState Evaluate()
    {
        m_origin.stats.nodesEvaluatedincrease();        

        if (m_target != null)
        {
            m_origin.stats.actionsPerformedIncrease();

            Vector3 direction = m_target.position - m_origin.transform.position;

            direction = Vector3.Normalize(direction);

            m_origin.velocity = direction;

            m_origin.transform.position += direction * m_speed * Time.deltaTime;

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
