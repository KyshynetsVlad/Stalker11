using System;

public class PlayerHP : Health
{
    public  PlayerHealth playerHealth;

    private void Start()
    {
        maxHealth = playerHealth.maxHealth;
        currentHealth = playerHealth.currentHealth;
    }

    public override void TakeHeal(Medkit.MedkitType medkitType)
    {
        // Получаем процент восстановления здоровья из типа аптечки
        float healPercentage = (float)medkitType / 100.0f;

        // Рассчитываем количество здоровья, которое будет восстановлено
        float healAmount = maxHealth * healPercentage;

        // Восстанавливаем здоровье
        currentHealth += healAmount;

        // Проверяем, чтобы текущее здоровье не превышало максимальное
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
        Destroy(gameObject);
    }
}