using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//selector node that uses memory
//selector will not evaluate the same condition node twice
public class MemorySelector : MBNode
{
    
    //list of the nodes children
    protected List<MBNode> nodes = new List<MBNode>();

    //the behaviour trees root node
    private MBTopNode topNode;

    //constructer
    public MemorySelector(List<MBNode> t_nodes, MBTopNode t_topNode)
    {
        this.nodes = t_nodes;

        this.topNode = t_topNode;
    }

    //evaluates the child nodes
    public override NodeState Evaluate()
    {

        foreach (var node in nodes)
        {
            //if the node is a question 
            //check if its id is in the root nodes dictionary
            //otherwise evaluate it
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
