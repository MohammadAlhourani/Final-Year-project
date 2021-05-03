using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//states of the Finite state machine
public enum EnemyState
{
    Wander,
    Chase,
    Attack,
    Cover
}

//enemy that uses the FSM behaviour tree
public class FSMEnemy : Character
{
    //range which the enemy will begin to attack its target
    [SerializeField] private float m_attackRange = 0;

    //the enemies target to attck
    [SerializeField] private GameObject m_target;

    //the characters vision cone
    [SerializeField] private FieldOfView m_visionCone;

    //list of gameobjects in the characters vision cone
    //and around the character
    private List<GameObject> m_gameObjects;

    //the characters current state
    private EnemyState m_state = EnemyState.Wander;

    //root node of the wander behaviour
    private BNode WanderTopNode;

    //root node of the chase behaviour
    private BNode chaseTopNode;

    //root node of the attck behaviour
    private BNode attackTopNode;

    //root node of the cover behaviour
    private BNode coverTopNode;

    ChaseNode m_chase;
    AttackNode m_attack;

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
            Transitions();
            detectObjects();

            switch (m_state)
            {
                case EnemyState.Attack:
                    {
                        attackTopNode.Evaluate();
                        break;
                    }
                case EnemyState.Chase:
                    {
                        chaseTopNode.Evaluate();
                        break;
                    }
                case EnemyState.Cover:
                    {
                        coverTopNode.Evaluate();
                        break;
                    }
                case EnemyState.Wander:
                    {
                        WanderTopNode.Evaluate();
                        break;
                    }
                default:
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 10);
                        break;
                    }
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
        ////Cover

        GetCoverNode getCoverNode = new GetCoverNode(m_target.transform, this);

        GoToCoverNode goToCoverNode = new GoToCoverNode(this, m_speed);

        HealNode Heal = new HealNode(20, this, m_healthRegenRate);

        Sequence CoverSequence = new Sequence(new List<BNode> { getCoverNode, goToCoverNode, Heal });

        coverTopNode = new Selector(new List<BNode> { CoverSequence });

        ////chase
        m_chase = new ChaseNode(m_target.transform, this, m_speed);

        Sequence ChaseSequence = new Sequence(new List<BNode> { m_chase });

        chaseTopNode = new Selector(new List<BNode> { ChaseSequence });

        ////wander
        WanderNode wander = new WanderNode(this, m_speed);

        Sequence WanderSequence = new Sequence(new List<BNode> {  wander });

        WanderTopNode = new Selector(new List<BNode> { WanderSequence });

        ////attack
        m_attack = new AttackNode(m_target.transform, this);

        Sequence AttackSequence = new Sequence(new List<BNode> { m_attack });

        attackTopNode = new Selector(new List<BNode> { AttackSequence });

    }

    //gets the abjects around the charcter and in the vision cone 
    private void detectObjects()
    {

        m_visionCone.setDirection(m_direction);
        m_visionCone.setOrigin(transform.position);

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
                m_target = gameobject;
                
                m_chase.setTarget(m_target.transform);
               
                m_attack.setTarget(m_target.transform);

                break;
            }

        }
    }

    //transitions for the FSM to swap between states
    private void Transitions()
    {
        float distance = Vector3.Distance(m_target.transform.position, gameObject.transform.position);

        switch (m_state)
        {
            case EnemyState.Attack:
                {
                    stats.conditionsCheckedIncrease(3);

                    if (m_currentHealth <= m_lowHealthThreshold)
                    {
                        m_state = EnemyState.Cover;
                    }

                    if(distance > m_attackRange &&
                        distance < m_detectionRange)
                    {
                        m_state = EnemyState.Chase;
                    }

                    if(m_target == null || distance > m_detectionRange)
                    {
                        m_state = EnemyState.Wander;
                    }

                    break;
                }
            case EnemyState.Chase:
                {
                    stats.conditionsCheckedIncrease(3);

                    if (m_currentHealth <= m_lowHealthThreshold)
                    {
                        m_state = EnemyState.Cover;
                    }

                    if (distance < m_attackRange)
                    {
                        m_state = EnemyState.Attack;
                    }

                    if(m_target == null || distance > m_detectionRange)
                    {
                        m_state = EnemyState.Wander;
                    }

                    break;
                }
            case EnemyState.Cover:
                {
                    stats.conditionsCheckedIncrease(3);

                    if (distance > m_attackRange &&
                        distance < m_detectionRange)
                    {
                        m_state = EnemyState.Chase;
                    }

                    if (distance < m_attackRange)
                    {
                        m_state = EnemyState.Attack;
                    }

                    if (m_currentHealth > m_lowHealthThreshold)
                    {
                        m_state = EnemyState.Wander;
                    }

                    break;
                }
            case EnemyState.Wander:
                {
                    stats.conditionsCheckedIncrease();

                    if (distance > m_attackRange &&
                        distance < m_detectionRange)
                    {
                        m_state = EnemyState.Chase;
                    }


                    break;
                }
            default:
                {
                    break;
                }
        }

    }


}
