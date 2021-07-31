using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth = 1;
    public bool hasRegen = false;
    public float regenTime = 2.0f;
    private float currentRegTime;
    public float regenAmount = 1.0f;

    public Health parentHealth;
    public bool parentAddHealth;


    public bool hasDied = false;


    public UnityEvent deathEvent;

    public float subEventThreshold = 1f;
    public UnityEvent subtractEvent;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool HasDied()
    {
        return hasDied;
    }

    public void AddHealth(float add)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + add);
    }

    public void SetHealth(float value)
    {
        currentHealth = Mathf.Min(maxHealth, value);
    }

    public float GetHealth() {
        return currentHealth;
    }
    public void SubtractHealth(float minus)
    {
        currentHealth -= minus;
        parentHealth?.SubtractHealth(minus);

        if (minus >= subEventThreshold)
        {
            subtractEvent?.Invoke();
        }
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
        hasDied = false;
    }

    public void Kill()
    {
        currentHealth = 0;
    }

    private void Update()
    {
        currentRegTime += Time.deltaTime;

        if (currentRegTime > regenTime && hasRegen)
        {
            AddHealth(regenAmount);
            currentRegTime = 0;
        }


        if (currentHealth <= 0 && !hasDied)
        {
            deathEvent?.Invoke();
            hasDied = true;
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Player Killed");
    }
}
