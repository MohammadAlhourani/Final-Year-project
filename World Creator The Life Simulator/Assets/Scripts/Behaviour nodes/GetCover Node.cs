using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoverNode : BNode
{
    private Cover[] m_cover;
    private Transform m_target;
    private Character m_origin;

    public GetCoverNode(Transform t_target, Character t_origin)
    {
        this.m_target = t_target;
        this.m_origin = t_origin;

        this.m_cover = m_origin.GetCoverAround();
    }

    public override NodeState Evaluate()
    {
        m_cover = m_origin.GetCoverAround();

        if (m_cover.Length > 0)
        {
            Transform bestSpot = FindBestCover();

            m_origin.SetBestCover(bestSpot);

            if (bestSpot != null)
            {
                return NodeState.Success;
            }
            else
            {
                return NodeState.Failure;
            }
        }
        else
        {
            return NodeState.Failure;
        }
                      
    }

    private Transform FindBestCover()
    {
        float minAngle = 90;

        Transform bestSpot = null;


        for(int i = 0; i < m_cover.Length; i++)
        {
            Transform bestSpotinCover = FindBestSpotInCover(m_cover[i], ref minAngle);

            if(bestSpotinCover != null)
            {
                bestSpot = bestSpotinCover;
            }
        }

        return bestSpot;
    }

    private Transform FindBestSpotInCover(Cover t_cover, ref float t_minAngle)
    {
        Transform[] availableSpots = t_cover.GetCoverSpots();

        Transform bestSpot = null;

        for (int i = 0; i < availableSpots.Length; i++)
        {
            Vector2 Direction = m_target.position - availableSpots[i].position;

            if (checkIfSpotIsValid(availableSpots[i]))
            {
                float angle = Vector2.Angle(availableSpots[i].up, Direction);

                if (angle < t_minAngle)
                {
                    t_minAngle = angle;
                    bestSpot = availableSpots[i];
                }

            }
        }

        return bestSpot;
    }

    private bool checkIfSpotIsValid(Transform t_spot)
    {      

        Vector2 direction = m_target.position - t_spot.position;

        RaycastHit2D hit2d = Physics2D.Raycast(t_spot.position, direction);

        if (hit2d != null)
        {
            if(hit2d.collider.transform != m_target)
            {
                return true;
            }
        }

        return false;
    }
}
