using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryInverter : MBNode
{
    protected MBNode node;

    private MBTopNode topNode;


    public MemoryInverter(MBNode t_node ,  MBTopNode t_topNode)
    {
        this.node = t_node;

        this.topNode = t_topNode;
    }

    public override NodeState Evaluate()
    {
        NodeState currentState = topNode.GetnodeTypeState(node.getId());

        if (currentState == null)
        {
            switch (this.node.Evaluate())
            {
                case NodeState.Running:
                    m_nodeState = NodeState.Running;
                    break;
                case NodeState.Success:
                    m_nodeState = NodeState.Failure;
                    break;
                case NodeState.Failure:
                    m_nodeState = NodeState.Success;
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
                    break;
                case NodeState.Success:
                    m_nodeState = NodeState.Failure;
                    break;
                case NodeState.Failure:
                    m_nodeState = NodeState.Success;
                    break;
                default:
                    break;
            }
        }

        return m_nodeState;
    }
}
