using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//health node behaviour

public class HealthNode : BNode
{
    //characters health to be evaluated
    private Character m_character;

    //threshhold for low health
    private float m_threshold;

    //constructor
    public HealthNode(Character t_character, float t_threshold)
    {
        this.m_character = t_character;
        this.m_threshold = t_threshold;
    }

    //evaluates the node
    //checks if the health is below the treshold returning success if it is
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
