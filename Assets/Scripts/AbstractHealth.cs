using UnityEngine;
using System.Collections;

public abstract class AbstractHealth : MonoBehaviour, Health
{
    public int initialHealth = 1;
    private int currentHealth;

    void Start()
    {
        currentHealth = initialHealth;
    }

    public void TakeDamage(GameObject origin, int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            if (currentHealth <= 0)
            {
                OnHealthDepleted();
            }
            else
            {
                OnDamageTaken();
            }
        }
    }

    public int getMaxHealth()
    {
        return initialHealth;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    protected void OnDamageTaken()
    {

    }

    protected abstract void OnHealthDepleted();
}