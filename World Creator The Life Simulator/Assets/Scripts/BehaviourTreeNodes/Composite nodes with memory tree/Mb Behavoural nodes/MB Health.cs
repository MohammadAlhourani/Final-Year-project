using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBHealth : MBNode
{
    private Character m_character;
    private float m_threshold;
    private int m_ID;
    private MBTopNode m_topnode;

    public MBHealth(Character t_character, float t_threshold, MBTopNode t_topnode, int t_Id = 1)
    {
        this.m_character = t_character;
        this.m_threshold = t_threshold;
        this.m_topnode = t_topnode;
        this.m_ID = t_Id;

        m_type = NodeType.Question;
    }

    public override NodeState Evaluate()
    {
        m_character.stats.nodesEvaluatedincrease();
        m_character.stats.conditionsCheckedIncrease();

        if (m_character.currentHealth <= m_threshold)
        {
            m_topnode.setNodeState(m_ID, NodeState.Success);
            return NodeState.Success;
        }

        m_topnode.setNodeState(m_ID, NodeState.Failure);
        return NodeState.Failure;
    }
}
