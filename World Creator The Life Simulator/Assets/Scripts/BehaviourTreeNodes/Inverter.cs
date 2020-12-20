using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : BNode
{
    protected BNode node;


    public Inverter(BNode t_node)
    {
        this.node = t_node;
    }


    public override NodeState Evaluate()
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

        return m_nodeState;
    }
}
