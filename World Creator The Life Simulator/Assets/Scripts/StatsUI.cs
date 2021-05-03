using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//stats of a behaviour tree
public class StatsUI : MonoBehaviour
{

    //UI texts to display the stats visually
    [SerializeField] private Text NodesEvalText;
    [SerializeField] private Text ConditionsCheckText;
    [SerializeField] private Text ActionsPerformText;

    //gets the stats of a vanilla behaviour tree
    public void InspectVanilla(Enemy t_character)
    {
        TreeStats charStats = t_character.stats;


        NodesEvalText.text = "" + charStats.getNodes();
        ConditionsCheckText.text = "" + charStats.getConditions();
        ActionsPerformText.text = "" + charStats.getActions();
    }

    //gets the stats of FSM behaviour tree
    public void InspectFSM(FSMEnemy t_character)
    {
        TreeStats charStats = t_character.stats;


        NodesEvalText.text = "" + charStats.getNodes();
        ConditionsCheckText.text = "" + charStats.getConditions();
        ActionsPerformText.text = "" + charStats.getActions();
    }

    //gets the stats of a CNM behaviour tree
    public void InspectCNM(CNMEnemy t_character)
    {
        TreeStats charStats = t_character.stats;


        NodesEvalText.text = "" + charStats.getNodes();
        ConditionsCheckText.text = "" + charStats.getConditions();
        ActionsPerformText.text = "" + charStats.getActions();
    }
}
