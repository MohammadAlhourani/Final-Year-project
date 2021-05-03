using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for a UI healthbar to display a characters health amount
public class UnitHealthBar : MonoBehaviour
{
    //background for the healthbar
    private GameObject redBar;

    //the max amount of health
    float maxHealth;

    //the current amount of health displayed
    float currentHealth;

    //the health bars starting rotaiion
    private Quaternion startingRotation;

    // Start is called before the first frame update
    void Start()
    {
        redBar = transform.GetChild(1).gameObject;

        startingRotation = transform.rotation;
    }

    //ensures the health bar keeps its staring position and rotation
    //compared to the parent object
    void LateUpdate()
    {
        transform.rotation = startingRotation;
        transform.position =  transform.parent.gameObject.transform.position - new Vector3(0f, 5f, 0f);
    }

    //sets the current health
    public void setCurrentHealth(float t_health)
    {
        currentHealth = t_health;

        redBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1.0f, 1.0f);
    }

    //sets the max amoutn of health
    public void setMaxHealth(float t_health)
    {
        maxHealth = t_health;
        currentHealth = t_health;

        redBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1.0f, 1.0f);
    }
}
