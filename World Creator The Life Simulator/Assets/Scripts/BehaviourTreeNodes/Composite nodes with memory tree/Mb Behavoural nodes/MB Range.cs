using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBRange : MBNode
{
    private float m_range;
    private Transform m_target;
    private Transform m_origin;
    private int m_ID;
    private MBTopNode m_topnode;
    private Character m_character;


    public MBRange(float t_range, Transform t_target, Transform t_origin, Character t_character, MBTopNode t_topnode, int t_Id = 2 )
    {
        this.m_range = t_range;
        this.m_target = t_target;
        this.m_origin = t_origin;
        this.m_character = t_character;
        this.m_topnode = t_topnode;
        this.m_ID = t_Id;


        m_type = NodeType.Question;
    }



    public override NodeState Evaluate()
    {
        m_character.stats.nodesEvaluatedincrease();
        m_character.stats.conditionsCheckedIncrease();

        if (m_target != null)
        {
            float distance = Vector3.Distance(m_target.position, m_origin.position);

            DrawEllipse(m_origin.position, Vector3.forward, Vector3.up, m_range, m_range, 30, new Color(255, 0, 0), 0.1f);
            if (distance <= m_range)
            {
                m_topnode.setNodeState(m_ID, NodeState.Success);
                return NodeState.Success;
            }
        }

        m_topnode.setNodeState(m_ID, NodeState.Failure);
        return NodeState.Failure;
    }

    public void setTarget(Transform t_target)
    {
        this.m_target = t_target;
    }

    private static void DrawEllipse(Vector3 pos, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, Color color, float duration = 0)
    {
        float angle = 0f;
        Quaternion rot = Quaternion.LookRotation(forward, up);
        Vector3 lastPoint = Vector3.zero;
        Vector3 thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }
    }
}
