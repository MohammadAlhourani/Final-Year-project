using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BNode
{

    protected List<BNode> nodes = new List<BNode>();


    public Sequence(List<BNode> t_nodes)
    {
        this.nodes = t_nodes;
    }


    public override NodeState Evaluate()
    {
        bool isAnyChildRunning = false;
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    isAnyChildRunning = true;
                    break;
                case NodeState.Success:
                    break;
                case NodeState.Failure:
                    m_nodeState = NodeState.Failure;
                    return m_nodeState;
                default:
                    break;           
            }

        }

        if(isAnyChildRunning == true)
        {
            m_nodeState = NodeState.Running;
        }
        else
        {
            m_nodeState = NodeState.Success;
        }

        return m_nodeState;
    }
}
