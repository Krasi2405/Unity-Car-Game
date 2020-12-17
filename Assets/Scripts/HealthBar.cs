using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    
    [SerializeField]
    private HealthSystem healthSystem = null;

    [SerializeField]
    private RectTransform bar = null;

    [SerializeField]
    private Image barImage = null;

    [SerializeField]
    private GameObject separatorContainer = null;

    [SerializeField]
    private GameObject separatorStartObject = null;

    [SerializeField]
    private bool isHorizontal = true;

    private static int HEALTH_PER_SEPARATOR = 100;

    public void Setup(HealthSystem newHealthSystem)
    {
        healthSystem = newHealthSystem;
        healthSystem.OnDamage += HealthSystem_OnHealthChange;
        healthSystem.OnHeal += HealthSystem_OnHealthChange;
        UpdateHealthBar();
        SetupSeparators();
    }


    private void HealthSystem_OnHealthChange(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float currentHealth = healthSystem.GetCurrentHealth();
        float maxHealth = healthSystem.GetMaxHealth();
        float healthBarFilledPercent = currentHealth / maxHealth;

        Vector3 scale = isHorizontal ?
            new Vector3(healthBarFilledPercent, 1, 1)
            :
            new Vector3(1, healthBarFilledPercent, 1);

        ChangeColor(healthBarFilledPercent);
        bar.transform.localScale = scale;
    }


    private void ChangeColor(float healthBarFilledPercent)
    {
        if (healthBarFilledPercent < 0)
        {
            healthBarFilledPercent = 0;
        }
        
        if (healthBarFilledPercent <= 0.25f)
        {
            barImage.color = Color.red;
        }
        else if (healthBarFilledPercent <= 0.5f)
        {
            barImage.color = Color.yellow;
        }
        else
        {
            barImage.color = Color.green;
        }
    }

    private void SetupSeparators()
    {
        float health = healthSystem.GetMaxHealth();
        int separatorCount = Mathf.RoundToInt(health / HEALTH_PER_SEPARATOR);
        float distancePerSeparator = isHorizontal ? bar.rect.width / separatorCount : bar.rect.height / separatorCount;

        for (int i = 1; i < separatorCount; i++)
        {
            GameObject separator = Instantiate(separatorStartObject, separatorContainer.transform);
            Vector3 separatorPosition = separator.transform.position;
            if (isHorizontal)
            {
                separatorPosition.x += i * distancePerSeparator;
            }
            else
            {
                separatorPosition.y += i * distancePerSeparator;
            }
            separator.transform.position = separatorPosition;
        }
    }
}
