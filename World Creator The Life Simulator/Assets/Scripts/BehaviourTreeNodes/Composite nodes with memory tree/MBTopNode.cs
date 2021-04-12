using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MBTopNode 
{
    private List<MBNode> nodes = new List<MBNode>();

    private Dictionary<int, NodeState> nodeStatus;

    private NodeState m_nodeState;

    public MBTopNode()
    {     
        nodeStatus = new Dictionary<int, NodeState>();
    }

    public NodeState GetNodeState()
    {
        return m_nodeState;
    }

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

    public void clearNodeStatus()
    {
        nodeStatus.Clear();
    }

    public void setNodeState(int t_ID, NodeState t_state)
    {
        nodeStatus.Add(t_ID, t_state);
    }


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

    public void setNodes(List<MBNode> t_nodes)
    {
        this.nodes = t_nodes;
    }
}
