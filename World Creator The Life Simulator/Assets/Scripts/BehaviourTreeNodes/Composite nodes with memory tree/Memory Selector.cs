using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySelector : MBNode
{
    protected List<MBNode> nodes = new List<MBNode>();

    private MBTopNode topNode;

    public MemorySelector(List<MBNode> t_nodes, MBTopNode t_topNode)
    {
        this.nodes = t_nodes;

        this.topNode = t_topNode;
    }

    public override NodeState Evaluate()
    {

        foreach (var node in nodes)
        {
            if (node.getType() == NodeType.Question)
            {
                NodeState currentState = topNode.GetnodeTypeState(node.getId());

                if (currentState == default(NodeState))
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
                else
                {
                    switch (currentState)
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
            }
            else
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

        }

        m_nodeState = NodeState.Failure;

        return m_nodeState;
    }
}
