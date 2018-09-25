using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    

    [SerializeField]
    private ParticleSystem smoke;

    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField] // TODO: REMOVE
    private float currentHealth;


    void Start () {
        currentHealth = maxHealth;
        
        smoke = GetComponentInChildren<ParticleSystem>();
        smoke.Stop();
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SmokeState();
        UpdateHealthBar();
    }


    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        SmokeState();
        UpdateHealthBar();
    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }


    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealthBar(HealthBar hpBar)
    {
        healthBar = hpBar;
    }


    private void UpdateHealthBar()
    {
        if(healthBar)
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }


    private void SmokeState()
    {

        if (currentHealth <= maxHealth / 4)
        {
            // TODO: Add burn effects.
            smoke.Play();
        }
        else if (currentHealth <= maxHealth / 2)
        {
            smoke.Play();
        }
        else
        {
            smoke.Stop();
        }
    }
}
