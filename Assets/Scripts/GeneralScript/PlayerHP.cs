using UnityEngine;

public class PlayerHP : Health
{
    public  PlayerHealth playerHealth;

    private void Start()
    {
        maxHealth = playerHealth.maxHealth;
        currentHealth = playerHealth.currentHealth;
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        playerHealth.currentHealth = currentHealth;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}