using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//root node of the CNM behaviour tree 
public class MBTopNode 
{
    //list of the children nodes
    private List<MBNode> nodes = new List<MBNode>();

    //dictionary to hold statuses of the question nodes
    private Dictionary<int, NodeState> nodeStatus;

    //nodes current state
    private NodeState m_nodeState;

    //constructer
    public MBTopNode()
    {     
        nodeStatus = new Dictionary<int, NodeState>();
    }

    //gets the current node state
    public NodeState GetNodeState()
    {
        return m_nodeState;
    }

    //checks if an id is in the dictionary returning 
    //default if it isnt
    public NodeState GetnodeTypeState(int t_ID)
    {
        if (nodeStatus.ContainsKey(t_ID))
        {
            return nodeStatus[t_ID];
        }
        else
        {
           return default(NodeState);
        }
    }

    //empties the dictionary
    public void clearNodeStatus()
    {
        nodeStatus.Clear();
    }

    //adds to the dictionary
    public void setNodeState(int t_ID, NodeState t_state)
    {
        nodeStatus.Add(t_ID, t_state);
    }

    //evaluates the child nodes
    public NodeState Evaluate()
    {
        nodeStatus.Clear();

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

    //sets the child nodes
    public void setNodes(List<MBNode> t_nodes)
    {
        this.nodes = t_nodes;
    }
}
