using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBar : MonoBehaviour
{
    private GameObject redBar;

    float maxHealth;

    float currentHealth;

    private Quaternion startingRotation;

    // Start is called before the first frame update
    void Start()
    {
        redBar = transform.GetChild(1).gameObject;

        startingRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = startingRotation;
        transform.position =  transform.parent.gameObject.transform.position - new Vector3(0f, 5f, 0f);
    }

    public void setCurrentHealth(float t_health)
    {
        currentHealth = t_health;

        redBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1.0f, 1.0f);
    }

    public void setMaxHealth(float t_health)
    {
        maxHealth = t_health;
        currentHealth = t_health;

        redBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1.0f, 1.0f);
    }
}
