using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private BNode topNode;

    
    [SerializeField] private float m_attackRange = 0;
    [SerializeField] private GameObject target;

    private List<GameObject> m_gameObjects;

    RangeNode Range;
    ChaseNode Chase;
    RangeNode attackRange;
    AttackNode attack;


    public override void OnStarting()
    {
        m_gameObjects = new List<GameObject>();

        constructBehaviourTree();
    }

    public override void OnUpdate()
    {
        detectObjects();

        topNode.Evaluate();

        if (topNode.GetNodeState() == NodeState.Failure)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0 , 10);
        }       
    }

    private void constructBehaviourTree()
    {
        //health node checks if health is below threshold
        HealthNode healthNode = new HealthNode(this, m_lowHealthThreshold);

        //inverted health checks if health is above threshhold
        Inverter invertHealth = new Inverter(healthNode);

        //Cover

        GetCoverNode getCoverNode = new GetCoverNode(target.transform, this);

        GoToCoverNode goToCoverNode = new GoToCoverNode(this, m_speed);

        HealNode Heal = new HealNode(20, this, m_healthRegenRate);

        Sequence CoverSequence = new Sequence(new List<BNode> { healthNode , getCoverNode , goToCoverNode , Heal});

        //chase
        Range = new RangeNode(m_detectionRange, target.transform, gameObject.transform);

        Chase = new ChaseNode(target.transform, this, m_speed);

        Sequence ChaseSequence = new Sequence(new List<BNode> { invertHealth, Range, Chase });

        //wander
        RangeNode wanderRange = new RangeNode(m_detectionRange, target.transform, gameObject.transform);

        Inverter invertWanderRange = new Inverter(wanderRange);

        WanderNode wander = new WanderNode(this, m_speed);

        Sequence WanderSequence = new Sequence(new List<BNode> { invertWanderRange, wander  });

        //attack
        attackRange = new RangeNode(m_attackRange, target.transform, gameObject.transform);

        attack = new AttackNode(target.transform, this);

        Sequence AttackSequence = new Sequence(new List<BNode> { attackRange, attack });


        //root
        topNode = new Selector(new List<BNode> {CoverSequence, AttackSequence, ChaseSequence, WanderSequence });
    }

  
    private void detectObjects()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, m_detectionRange);

        m_gameObjects.Clear();

        for(int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject != gameObject)
            {
                m_gameObjects.Add(hitColliders[i].gameObject);
            }
        }

        foreach(var gameobject in m_gameObjects)
        { 
            if(gameobject.CompareTag("Human"))
            {
                target = gameobject;

                Range.setTarget(target.transform);
                Chase.setTarget(target.transform);
                attackRange.setTarget(target.transform);
                attack.setTarget(target.transform);


                break;
            }

        }
    }
}
