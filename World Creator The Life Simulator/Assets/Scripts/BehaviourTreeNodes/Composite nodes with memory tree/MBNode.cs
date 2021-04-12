using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public abstract class MBNode 
{
    protected NodeState m_nodeState;

    protected int m_ID;

    protected NodeType m_type;

    public int getId()
    {
        return m_ID;
    }

    public NodeType getType()
    {
        return m_type;
    }

    public NodeState GetNodeState()
    {
        return m_nodeState;
    }

    public abstract NodeState Evaluate();
}

public enum NodeType
{
    Action,
    Question
}
