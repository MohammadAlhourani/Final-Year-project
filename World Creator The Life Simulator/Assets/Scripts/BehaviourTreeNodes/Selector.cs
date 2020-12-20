using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BNode
{
    protected List<BNode> nodes = new List<BNode>();


    public Selector(List<BNode> t_nodes)
    {
        this.nodes = t_nodes;
    }


    public override NodeState Evaluate()
    {

        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    m_nodeState = NodeState.Running;
                    return m_nodeState;
                case NodeState.Success:
                    m_nodeState = NodeState.Success;
                    return m_nodeState;
                case NodeState.Failure:
                    break;
                default:
                    break;
            }

        }

        m_nodeState = NodeState.Failure;

        return m_nodeState;
    }
}
