using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBWander : MBNode
{
    //the character using the behaviour
    private Character m_origin;

    //chatacter speed
    private float m_speed;

    //amoutn of time for the wander direction to change
    private float startingTime = 0.5f;

    //the timer for changing the diretion
    private float timer;

    //randomized direction
    private Vector3 m_direction;

    //constructor for the node
    public MBWander(Character t_origin, float t_speed = 1)
    {
        this.m_origin = t_origin;
        this.m_speed = t_speed;

        //giving direction a random point to look in
        m_direction = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
        m_direction = Vector3.Normalize(m_direction);

        timer = startingTime;

        m_type = NodeType.Action;
    }

    public override NodeState Evaluate()
    {
        m_origin.stats.nodesEvaluatedincrease();
        m_origin.stats.actionsPerformedIncrease();

        //timer for chaning the direction
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            //random rotating the vector between 90 degree cone 
            m_direction = rotate(Random.Range(-45.0f, 45.0f), m_direction);
            m_direction = Vector3.Normalize(m_direction);

            timer = startingTime;
        }

        //ensuring character doesnt go out of map bounds
        boundary();

        //ray from the leftmost point of the character
        Vector3 LeftRay = m_origin.transform.position;
        //ray from the rightmost point of the character
        Vector3 RightRay = m_origin.transform.position;

        LeftRay.x -= 2.7f;

        RightRay.x += 2.7f;

        //racasts for the 3 point in front , to the left and right of the character
        RaycastHit2D hit2d = Physics2D.Raycast(m_origin.transform.position, Vector2.up, 5);
        RaycastHit2D lefthit2d = Physics2D.Raycast(LeftRay, Vector2.up, 5);
        RaycastHit2D righthit2d = Physics2D.Raycast(RightRay, Vector2.up, 5);

        //if the front raycast hit something
        if (hit2d.collider != null)
        {
            //ensuring the thing we hit is the character
            if (hit2d.transform != m_origin.transform)
            {
                //drawing the raycast if it hits something in front is colored red
                Debug.DrawLine(m_origin.transform.position, hit2d.point, new Color(255, 0, 0));

                //add the raycast normal to the direction with repulsion force
                m_direction += (Vector3)hit2d.normal * 10f;

            }
        }

        //if the left raycast hit something
        if (lefthit2d.collider != null)
        {
            //ensuring the thing we hit is the character
            if (lefthit2d.transform != m_origin.transform)
            {
                //drawing the raycast if it hits something left ray is colored green
                Debug.DrawLine(LeftRay, lefthit2d.point, new Color(0, 255, 0));

                //add the raycast normal to the direction with repulsion force
                m_direction += (Vector3)lefthit2d.normal * 10f;

            }
        }

        //if the right raycast hit something
        if (righthit2d.collider != null)
        {
            //ensuring the thing we hit is the character
            if (righthit2d.transform != m_origin.transform)
            {
                //drawing the raycast if it hits something right ray is colored blue
                Debug.DrawLine(RightRay, righthit2d.point, new Color(0, 0, 255));

                //add the raycast normal to the direction with repulsion force
                m_direction += (Vector3)righthit2d.normal * 10f;

            }
        }

        //setting the velocity to the direction this is to rotate the 
        //characters sprite to match the direction it is moving
        m_origin.velocity = m_direction;

        //moving the charcter
        m_origin.transform.position += m_direction * m_speed * Time.deltaTime;

        //return currently runnung this node
        return NodeState.Running;

    }

    //rotating a vector by degrees
    private Vector3 rotate(float t_degrees, Vector3 t_vector)
    {
        float radians = t_degrees * (3.14f / 180.0f);

        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        t_vector.x = ((t_vector.x * cos) - (t_vector.y * sin));
        t_vector.y = ((t_vector.x * sin) + (t_vector.y * cos));

        return t_vector;
    }

    //boundary of the map
    //if the charcter is going outside of the map
    //flip the direction
    private void boundary()
    {
        Vector3 futurePos = m_origin.transform.position + (m_direction * 50);

        if (futurePos.y > m_origin.getBoundary().k_north)
        {
            m_direction = m_direction * -1;
        }
        else if (futurePos.y < m_origin.getBoundary().k_south)
        {
            m_direction = m_direction * -1;
        }
        else if (futurePos.x > m_origin.getBoundary().k_east)
        {
            m_direction = m_direction * -1;
        }
        else if (futurePos.x < m_origin.getBoundary().k_west)
        {
            m_direction = m_direction * -1;
        }
    }
}
