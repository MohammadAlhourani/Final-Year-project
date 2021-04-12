using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStats : MonoBehaviour
{
    int nodesEvaluated = 0;

    int ConditionsChecked = 0;

    int actionsPerformed = 0;


    public void nodesEvaluatedincrease(int num = 1)
    {
        nodesEvaluated += num;
    }

    public void conditionsCheckedIncrease(int num = 1)
    {
        ConditionsChecked += num;
    }

    public void actionsPerformedIncrease(int num = 1)
    {
        actionsPerformed += num;
    }

    public int getNodes()
    {
        return nodesEvaluated;
    }

    public int getConditions()
    {
        return ConditionsChecked;
    }

    public int getActions()
    {
        return actionsPerformed;
    }
}
