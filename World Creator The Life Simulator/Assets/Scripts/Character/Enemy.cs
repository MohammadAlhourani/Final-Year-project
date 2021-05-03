using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic enemy uses the Vanilla Behaviour tree 
public class Enemy : Character
{
    //rootnode of the behaviour tree
    private BNode topNode;
    
    //range which the enemy will begin to attack its target
    [SerializeField] private float m_attackRange = 0;

    //the enemies target to attck
    [SerializeField] private GameObject target;

    //the characters vision cone
    [SerializeField] private FieldOfView m_visionCone;  

    //list of gameobjects in the characters vision cone
    //and around the character
    private List<GameObject> m_gameObjects;


    RangeNode Range;
    ChaseNode Chase;
    RangeNode attackRange;
    AttackNode attack;

    //start function called on the first frame
    public override void OnStarting()
    {
        m_gameObjects = new List<GameObject>();        

        constructBehaviourTree();
    }

    //the update function called every frame
    public override void OnUpdate()
    {      
        //if the characters health is above 0 the behaviour tree will run
        //otherwise the charcater is considered "Dead"
        if (currentHealth > 0)
        {
            detectObjects();

            topNode.Evaluate();

            if (topNode.GetNodeState() == NodeState.Failure)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 10);
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 10);
        }
    }

    //builds the characters behaviour tree
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
        Range = new RangeNode(m_detectionRange, target.transform, gameObject.transform, this);

        Chase = new ChaseNode(target.transform, this, m_speed);

        Sequence ChaseSequence = new Sequence(new List<BNode> { invertHealth, Range, Chase });

        //wander
        RangeNode wanderRange = new RangeNode(m_detectionRange, target.transform, gameObject.transform, this);

        Inverter invertWanderRange = new Inverter(wanderRange);

        WanderNode wander = new WanderNode(this, m_speed);

        Sequence WanderSequence = new Sequence(new List<BNode> { invertWanderRange, wander  });

        //attack
        attackRange = new RangeNode(m_attackRange, target.transform, gameObject.transform, this);

        attack = new AttackNode(target.transform, this);

        Sequence AttackSequence = new Sequence(new List<BNode> { attackRange, attack });


        //root node
        topNode = new Selector(new List<BNode> {CoverSequence, AttackSequence, ChaseSequence, WanderSequence });
    }

    //gets the abjects around the charcter and in the vision cone 
    private void detectObjects()
    {
        m_visionCone.setDirection(m_direction);
        m_visionCone.setOrigin(transform.position);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, m_detectionRange);

        m_gameObjects.Clear();

        for(int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject != gameObject)
            {
                m_gameObjects.Add(hitColliders[i].gameObject);
            }
        }

        foreach(var gameobject in m_visionCone.GetGameObjects())
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
