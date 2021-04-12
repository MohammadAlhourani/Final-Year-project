using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{

    [SerializeField] private Text NodesEvalText;
    [SerializeField] private Text ConditionsCheckText;
    [SerializeField] private Text ActionsPerformText;


    public void InspectVanilla(Enemy t_character)
    {
        TreeStats charStats = t_character.stats;


        NodesEvalText.text = "" + charStats.getNodes();
        ConditionsCheckText.text = "" + charStats.getConditions();
        ActionsPerformText.text = "" + charStats.getActions();
    }

    public void InspectFSM(FSMEnemy t_character)
    {
        TreeStats charStats = t_character.stats;


        NodesEvalText.text = "" + charStats.getNodes();
        ConditionsCheckText.text = "" + charStats.getConditions();
        ActionsPerformText.text = "" + charStats.getActions();
    }

    public void InspectCNM(CNMEnemy t_character)
    {
        TreeStats charStats = t_character.stats;


        NodesEvalText.text = "" + charStats.getNodes();
        ConditionsCheckText.text = "" + charStats.getConditions();
        ActionsPerformText.text = "" + charStats.getActions();
    }
}
