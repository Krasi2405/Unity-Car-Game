using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    public event System.EventHandler OnDeath;
    public event System.EventHandler OnHeal;
    public event System.EventHandler OnDamage;
    
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    private bool isDead = false;

    void Awake () {
        currentHealth = maxHealth;
    }


    public void TakeDamage(float damage)
    {
        if(isDead) { return; }
        Debug.Log($"Take {damage}");

        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Debug.Log("die");
            currentHealth = 0;
            OnDeath?.Invoke(this, System.EventArgs.Empty);
            isDead = true;
        }
        OnDamage?.Invoke(this, System.EventArgs.Empty);
    }


    public void Heal(float healAmount)
    {
        if (isDead) { return; }

        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHeal?.Invoke(this, System.EventArgs.Empty);
    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }


    public float GetMaxHealth()
    {
        return maxHealth;
    }


    public void SetMaxHealth(float newMaxHealth, bool resetCurrentHealth = false)
    {
        if (isDead) { return; }

        maxHealth = newMaxHealth;
        if(resetCurrentHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
