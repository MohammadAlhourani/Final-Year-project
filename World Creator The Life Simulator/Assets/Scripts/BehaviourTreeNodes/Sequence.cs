using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the sequence composite node
//node evaluates children in order 
//if any child returns failure this node will return failure
//if all children retun success this node will return success
public class Sequence : BNode
{
    //list of  children nodes
    protected List<BNode> nodes = new List<BNode>();


    //constructer for the node
    public Sequence(List<BNode> t_nodes)
    {
        this.nodes = t_nodes;
    }

    //evaluates all children nodes
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
        // sets the nodestate to running if a child is still running
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
