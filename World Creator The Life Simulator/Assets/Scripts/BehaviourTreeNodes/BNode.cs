using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public abstract class BNode 
{
    protected NodeState m_nodeState;


    public NodeState GetNodeState()
    {
        return m_nodeState;
    }


    public abstract NodeState Evaluate();
}


public enum NodeState
{
    Running,
    Success,
    Failure
}
