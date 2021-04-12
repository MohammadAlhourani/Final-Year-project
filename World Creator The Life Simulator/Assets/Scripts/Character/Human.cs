using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Character
{

    private BNode topNode;


    public override void OnStarting()
    {
        //constructBehaviourTree();
    }

    public override void OnUpdate()
    {
        //topNode.Evaluate();

        //if(topNode.GetNodeState() == NodeState.Failure)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        //}
    }

    private void constructBehaviourTree()
    {
        HealthNode healthNode = new HealthNode(this, m_lowHealthThreshold);

        Sequence fleeSequence = new Sequence(new List<BNode> { healthNode });

        topNode = new Selector(new List<BNode> { fleeSequence });
    }
}
