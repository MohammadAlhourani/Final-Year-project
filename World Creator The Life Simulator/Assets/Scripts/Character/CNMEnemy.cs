using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy that use the Composite nodes with memeory Behaviour tree
public class CNMEnemy : Character
{
    //range which the enemy will begin to attack its target
    [SerializeField] private float m_attackRange = 0;

    //the enemies target to attck
    [SerializeField] private GameObject target;

    //the charcters vision cone
    [SerializeField] private FieldOfView visionCone;

    //list of gameobjects in the charcters vision cone
    //and around the character
    private List<GameObject> m_gameObjects;

    MBRange m_Range;
    MBChase m_Chase;
    MBRange m_attackRangeNode;
    MBAttack m_attack;

    //rootnode of the behaviour tree
    MBTopNode m_topNode;


    //start function called on the first frame
    public override void OnStarting()
    {
        m_gameObjects = new List<GameObject>();
        m_topNode = new MBTopNode();

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

            m_topNode.Evaluate();

            if (m_topNode.GetNodeState() == NodeState.Failure)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 10);
            }

            visionCone.setDirection(m_direction);
            visionCone.setOrigin(transform.position);
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
        MBHealth healthNode = new MBHealth(this, m_lowHealthThreshold, m_topNode , 1);

        //inverted health checks if health is above threshhold
        MemoryInverter invertHealth = new MemoryInverter(healthNode, m_topNode);

        //Cover

        MBGetCover getCoverNode = new MBGetCover(target.transform, this);

        MBGoToCover goToCoverNode = new MBGoToCover(this, m_speed);

        MBHeal Heal = new MBHeal(20, this, m_healthRegenRate);

        MemorySequence CoverSequence = new MemorySequence(new List<MBNode> { healthNode, getCoverNode, goToCoverNode, Heal }, m_topNode);

        //chase
        m_Range = new MBRange(m_detectionRange, target.transform, gameObject.transform, this, m_topNode, 2);

        m_Chase = new MBChase(target.transform, this, m_speed);

        MemorySequence ChaseSequence = new MemorySequence(new List<MBNode> { invertHealth, m_Range, m_Chase }, m_topNode);

        //wander
        MBRange wanderRange = new MBRange(m_detectionRange, target.transform, gameObject.transform, this, m_topNode, 2);

        MemoryInverter invertWanderRange = new MemoryInverter(wanderRange, m_topNode);

        MBWander wander = new MBWander(this, m_speed);

        MemorySequence WanderSequence = new MemorySequence(new List<MBNode> { invertWanderRange, wander }, m_topNode);

        //attack
        m_attackRangeNode = new MBRange(m_attackRange, target.transform, gameObject.transform, this, m_topNode, 4);

        m_attack = new MBAttack(target.transform, this);

        MemorySequence AttackSequence = new MemorySequence(new List<MBNode> { m_attackRangeNode, m_attack }, m_topNode);

        m_topNode.setNodes(new List<MBNode> { CoverSequence, AttackSequence, ChaseSequence, WanderSequence });        
    }

    //gets the abjects around the charcter and in the vision cone 
    private void detectObjects()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, m_detectionRange);

        m_gameObjects.Clear();

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject != gameObject)
            {
                m_gameObjects.Add(hitColliders[i].gameObject);
            }
        }

        foreach (var gameobject in m_gameObjects)
        {
            if (gameobject.CompareTag("Human"))
            {
                target = gameobject;

                m_Range.setTarget(target.transform);
                m_Chase.setTarget(target.transform);
                m_attackRangeNode.setTarget(target.transform);
                m_attack.setTarget(target.transform);


                break;
            }

        }
    }
}
