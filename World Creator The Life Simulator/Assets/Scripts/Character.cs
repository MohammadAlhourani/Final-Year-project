using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] public float lowHealthThreshold;   
    [SerializeField] private float healthRegenRate;

    protected float m_currentHealth;

    protected float currentHealth
    {
        get { return m_currentHealth; }
        set { m_currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }


    public float GetCurrentHealth()
    {
      return m_currentHealth;
    }

    private void Start()
    {
        currentHealth = startingHealth;
        OnStarting();
    }

    private void Update()
    {
        OnUpdate();
    }

    public abstract void OnStarting();
    public abstract void OnUpdate();
}
