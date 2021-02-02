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
        m_direction = Vector3.Normalize(m_direction);

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
            m_direction = Vector3.Normalize(m_direction);          

            timer = startingTime;
        }

        boundary();

        Vector3 LeftRay = m_origin.transform.position;
        Vector3 RightRay = m_origin.transform.position;

        LeftRay.x -= 2.7f;

        RightRay.x += 2.7f;

        RaycastHit2D hit2d = Physics2D.Raycast(m_origin.transform.position, Vector2.up, 5);
        RaycastHit2D lefthit2d = Physics2D.Raycast(LeftRay, Vector2.up, 5);
        RaycastHit2D righthit2d = Physics2D.Raycast(RightRay, Vector2.up, 5);

        if (hit2d.collider != null)
        {
            if (hit2d.transform != m_origin.transform)
            {               
                Debug.DrawLine(m_origin.transform.position, hit2d.point, new Color(255, 0, 0));
                m_direction += (Vector3)hit2d.normal * 10f;
            }
        }

        if (lefthit2d.collider != null)
        {
            if (lefthit2d.transform != m_origin.transform)
            {
                
                Debug.DrawLine(LeftRay, lefthit2d.point, new Color(0, 255, 0));
                m_direction += (Vector3)lefthit2d.normal * 10f;
            }
        }

        if (righthit2d.collider != null)
        {
            if (righthit2d.transform != m_origin.transform)
            {
                
                Debug.DrawLine(RightRay, righthit2d.point, new Color(0, 0, 255));
                m_direction += (Vector3)righthit2d.normal * 10f;
            }
        }

       

        m_origin.velocity = m_direction;

        m_origin.transform.position += m_direction * m_speed * Time.deltaTime;

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
