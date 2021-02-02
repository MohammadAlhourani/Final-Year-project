using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCoverNode : BNode
{
    private Character m_origin;

    private float m_speed;

    public GoToCoverNode(Character t_origin, float t_speed = 1)
    {
        this.m_origin = t_origin;
        this.m_speed = t_speed;
    }

    public override NodeState Evaluate()
    {
        Transform coverSpot = m_origin.GetBestCover();

        if(coverSpot != null)
        {
            Vector3 LeftRay = m_origin.transform.position;
            Vector3 RightRay = m_origin.transform.position;
               

            LeftRay.x -= 2.7f;

            RightRay.x += 2.7f;

            Vector3 direction = coverSpot.position - m_origin.transform.position;

            direction = Vector3.Normalize(direction);

            RaycastHit2D hit2d = Physics2D.Raycast(m_origin.transform.position, -Vector2.up, 5);
            RaycastHit2D lefthit2d = Physics2D.Raycast(LeftRay, -Vector2.up, 5);
            RaycastHit2D righthit2d = Physics2D.Raycast(RightRay, -Vector2.up, 5);

            if (hit2d.collider != null)
            {
                if(hit2d.transform != m_origin.transform)
                {
                    //Adding raycast normal + repulsion force
                    Debug.DrawLine(m_origin.transform.position, hit2d.point, new Color(255, 0, 0));
                    direction += (Vector3)hit2d.normal * 10f;
                }
            }

            if (lefthit2d.collider != null)
            {
                if (lefthit2d.transform != m_origin.transform)
                {
                    Debug.DrawLine(LeftRay, lefthit2d.point, new Color(0, 255, 0));
                    direction += (Vector3)lefthit2d.normal * 10f;
                }
            }

            if (righthit2d.collider != null)
            {
                if (righthit2d.transform != m_origin.transform)
                {
                    Debug.DrawLine(RightRay, righthit2d.point, new Color(0, 0, 255));
                    direction += (Vector3)righthit2d.normal * 10f;
                }
            }

            float distanceToSpot = Vector3.Distance(m_origin.transform.position, coverSpot.position);

            m_origin.velocity = direction;

            m_origin.transform.position += direction * m_speed * Time.deltaTime;

            if( distanceToSpot <= 5)
            {
                return NodeState.Success;
            }

            return NodeState.Running;
        }
        else
        {
            return NodeState.Failure;
        }        
    }
}
