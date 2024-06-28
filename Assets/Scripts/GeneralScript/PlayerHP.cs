using UnityEngine;

public class PlayerHP : Health
{
    public PlayerHealth playerHealth;

    [SerializeField]private PauseMenu pauseMenu; //to resetLvl;

    private void Start()
    {
        maxHealth = playerHealth.maxHealth;
        currentHealth = playerHealth.currentHealth;
    }

    public override void TakeHeal(int healPoints)
    {
        currentHealth += maxHealth * 0.125f * (float)healPoints;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Обновляем здоровье в playerHealth
        playerHealth.currentHealth = currentHealth;
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
        pauseMenu.Restart();
        currentHealth = 100;
        Destroy(gameObject);
    }
}