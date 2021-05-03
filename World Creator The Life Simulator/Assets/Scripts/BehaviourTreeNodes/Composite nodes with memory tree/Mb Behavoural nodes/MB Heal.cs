using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//heal behaviour node
//starts a heal over time for the character
public class MBHeal : MBNode
{
    private float m_regenAmount = 0;
    private float m_amountToHeal = 0;
    private Character m_origin;

    public MBHeal(float t_amount, Character t_origin, float t_regen = 1)
    {
        this.m_amountToHeal = t_amount;
        this.m_regenAmount = t_regen;
        this.m_origin = t_origin;

        m_type = NodeType.Action;
    }

    public override NodeState Evaluate()
    {
        m_origin.stats.nodesEvaluatedincrease();
        m_origin.stats.actionsPerformedIncrease();

        m_origin.StartCoroutine(HealOverTimeCoroutine(m_amountToHeal, m_regenAmount, m_origin));

        return NodeState.Success;
    }

    private IEnumerator HealOverTimeCoroutine(float t_healAmount, float t_regenRate, Character t_origin)
    {
        float amountHealed = 0;

        while (amountHealed < t_healAmount)
        {
            t_origin.currentHealth = t_origin.currentHealth + t_regenRate;

            amountHealed += t_regenRate;

            yield return new WaitForSeconds(1f);
        }

        yield break;

    }
}
