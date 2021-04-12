using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNMEnemy : Character
{

    [SerializeField] private float m_attackRange = 0;
    [SerializeField] private GameObject target;

    [SerializeField] private FieldOfView visionCone;

    private List<GameObject> m_gameObjects;

    MBRange m_Range;
    MBChase m_Chase;
    MBRange m_attackRangeNode;
    MBAttack m_attack;

    MBTopNode m_topNode;

    public override void OnStarting()
    {
        m_gameObjects = new List<GameObject>();
        m_topNode = new MBTopNode();

        constructBehaviourTree();
    }

    public override void OnUpdate()
    {
        detectObjects();


        m_topNode.Evaluate();

        if (m_topNode.GetNodeState() == NodeState.Failure)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 10);
        }

        visionCone.setDirection(m_velocity);
        visionCone.setOrigin(transform.position);
    }

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
        MBRange wanderRange = new MBRange(m_detectionRange, target.transform, gameObject.transform, this, m_topNode, 3);

        MemoryInverter invertWanderRange = new MemoryInverter(wanderRange, m_topNode);

        MBWander wander = new MBWander(this, m_speed);

        MemorySequence WanderSequence = new MemorySequence(new List<MBNode> { invertWanderRange, wander }, m_topNode);

        //attack
        m_attackRangeNode = new MBRange(m_attackRange, target.transform, gameObject.transform, this, m_topNode, 4);

        m_attack = new MBAttack(target.transform, this);

        MemorySequence AttackSequence = new MemorySequence(new List<MBNode> { m_attackRangeNode, m_attack }, m_topNode);

        m_topNode.setNodes(new List<MBNode> { CoverSequence, AttackSequence, ChaseSequence, WanderSequence });        
    }


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
