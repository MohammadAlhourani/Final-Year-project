using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Wander,
    Chase,
    Attack,
    Cover
}


public class FSMEnemy : Character
{
    [SerializeField] private float m_attackRange = 0;
    [SerializeField] private GameObject m_target;

    private List<GameObject> m_gameObjects;

    private EnemyState m_state = EnemyState.Wander;

    private BNode WanderTopNode;

    private BNode chaseTopNode;

    private BNode attackTopNode;

    private BNode coverTopNode;

    ChaseNode m_chase;
    AttackNode m_attack;

    public override void OnStarting()
    {
        m_gameObjects = new List<GameObject>();

        constructBehaviourTree();
    }

    public override void OnUpdate()
    {
        Transitions();

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
                m_target = gameobject;
                
                m_chase.setTarget(m_target.transform);
               
                m_attack.setTarget(m_target.transform);

                break;
            }

        }
    }

    private void Transitions()
    {
        float distance = Vector3.Distance(m_target.transform.position, gameObject.transform.position);

        switch (m_state)
        {
            case EnemyState.Attack:
                {
                    if(m_currentHealth <= m_lowHealthThreshold)
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
