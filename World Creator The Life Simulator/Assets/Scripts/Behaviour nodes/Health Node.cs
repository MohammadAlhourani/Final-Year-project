using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : BNode
{

    private Character m_character;
    private float m_threshold;


    public HealthNode(Character t_character, float t_threshold)
    {
        this.m_character = t_character;
        this.m_threshold = t_threshold;
    }

    public override NodeState Evaluate()
    {
        m_character.stats.nodesEvaluatedincrease();
        m_character.stats.conditionsCheckedIncrease();

        if (m_character.currentHealth <= m_threshold)
        {
            return NodeState.Success;
        }
       
        return NodeState.Failure;
    }
}
