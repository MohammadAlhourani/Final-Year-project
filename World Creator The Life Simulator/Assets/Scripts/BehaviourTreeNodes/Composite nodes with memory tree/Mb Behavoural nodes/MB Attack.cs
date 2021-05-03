using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attack behaviour node
public class MBAttack : MBNode
{
    //the target to attack
    private Transform m_target;

    //the character attacking
    private Character m_origin;

    //timer between attacks
    private float timer = 1.5f;

    //constructor
    public MBAttack(Transform t_target, Character t_origin)
    {
        this.m_target = t_target;
        this.m_origin = t_origin;

        m_type = NodeType.Action;
    }

    //evaluates the node
    //gets the direction between the origin and the target
    //and shoots a raycast in that direction
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

    //sets the target of the node
    public void setTarget(Transform t_target)
    {
        this.m_target = t_target;
    }
}
