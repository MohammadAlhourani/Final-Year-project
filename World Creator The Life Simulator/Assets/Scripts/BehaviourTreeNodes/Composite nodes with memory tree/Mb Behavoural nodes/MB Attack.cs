using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBAttack : MBNode
{
    private Transform m_target;
    private Character m_origin;

    private float timer = 1.5f;

    public MBAttack(Transform t_target, Character t_origin)
    {
        this.m_target = t_target;
        this.m_origin = t_origin;

        m_type = NodeType.Action;
    }

    public override NodeState Evaluate()
    {
        m_origin.stats.nodesEvaluatedincrease();

        Vector3 direction = m_target.position - m_origin.transform.position;

        direction = Vector3.Normalize(direction);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            BulletRaycast.EnemyShoot(m_origin.transform.position, direction);

            m_origin.stats.actionsPerformedIncrease();

            timer = 1.5f;
        }

        m_origin.velocity = direction;

        return NodeState.Running;
    }

    public void setTarget(Transform t_target)
    {
        this.m_target = t_target;
    }
}
