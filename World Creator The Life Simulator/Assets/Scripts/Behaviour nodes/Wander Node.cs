using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderNode : BNode
{

    private Character m_origin;

    private float m_speed;

    private float startingTime = 0.5f;

    private float timer;

    private Vector3 m_direction;

    public WanderNode(Character t_origin, float t_speed = 1)
    {
        this.m_origin = t_origin;
        this.m_speed = t_speed;

        m_direction = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
        m_direction = Vector3.Normalize(m_direction) * m_speed;

        timer = startingTime;
    }

    public override NodeState Evaluate()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            m_direction = rotate(Random.Range(-45.0f, 45.0f), m_direction);
            m_direction = Vector3.Normalize(m_direction) * m_speed;          

            timer = startingTime;
        }

        boundary();

        m_origin.velocity = m_direction;

        m_origin.transform.position += m_direction;

        return NodeState.Running;

    }

    private Vector3 rotate(float t_degrees, Vector3 t_velocity)
    {
        float radians = t_degrees * (3.14f / 180.0f); 

        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        t_velocity.x = ((t_velocity.x * cos) - (t_velocity.y * sin));
        t_velocity.y = ((t_velocity.x * sin) + (t_velocity.y * cos));

        return t_velocity;
    }


    private void boundary()
    {
        Vector3 futurePos = m_origin.transform.position + (m_direction * 50);

        if (futurePos.y > m_origin.getBoundary().k_north)
        {
            m_direction = m_direction * -1;
        }
        else if(futurePos.y < m_origin.getBoundary().k_south)
        {
            m_direction = m_direction * -1;
        }
        else if (futurePos.x > m_origin.getBoundary().k_east)
        {
            m_direction = m_direction * -1;
        }
        else if(futurePos.x < m_origin.getBoundary().k_west)
        {
            m_direction = m_direction * -1;
        }

        
    }
}
