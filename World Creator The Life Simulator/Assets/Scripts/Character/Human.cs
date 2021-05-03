using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Character
{

    


    public override void OnStarting()
    {
       
    }

    public override void OnUpdate()
    {
        Movement();
        Aim();
    }

    private void Movement()
    {
        if(Input.GetButton("W"))
        {

            if (transform.position.y + m_speed < getBoundary().k_north)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + m_speed * Time.deltaTime, 0);
            }
        }

        if (Input.GetButton("A"))
        {
            if (transform.position.x - m_speed > getBoundary().k_west)
            {
                transform.position = new Vector3(transform.position.x - m_speed * Time.deltaTime, transform.position.y, 0);
            }
        }

        if (Input.GetButton("S"))
        {
            if (transform.position.y - m_speed > getBoundary().k_south)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - m_speed * Time.deltaTime, 0);
            }
        }

        if (Input.GetButton("D"))
        {
            if (transform.position.x + m_speed < getBoundary().k_east)
            {
                transform.position = new Vector3(transform.position.x + m_speed * Time.deltaTime, transform.position.y, 0);
            }
        }
    }

    private void Aim()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        vec.z = 0;

        m_direction = vec;

        if (Input.GetMouseButtonDown(1))
        {
            BulletRaycast.PlayerShoot(transform.position, vec);
        }
    }
}
