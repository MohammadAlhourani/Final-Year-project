using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//sequence node with memory
public class MemorySequence : MBNode
{
    //list of the children nodes
    protected List<MBNode> nodes = new List<MBNode>();

    //the behaviour trees top node
    private MBTopNode topNode;

    //constructor
    public MemorySequence(List<MBNode> t_nodes , MBTopNode t_topNode)
    {
        this.nodes = t_nodes;

        this.topNode = t_topNode;
    }

    //evaluates the child nodes
    public override NodeState Evaluate()
    {
        bool isAnyChildRunning = false;
        foreach (var node in nodes)
        {
            //checks if the node is a question node 
            //checks if the node is in the root nodes dictionary
            //evaluate the node if it isnt
            if (node.getType() == NodeType.Question)
            {
                NodeState currentState = topNode.GetnodeTypeState(node.getId());

                if (currentState == default(NodeState))
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
                else
                {
                    switch (currentState)
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
            }
            else
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
        }

        

        if (isAnyChildRunning == true)
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
