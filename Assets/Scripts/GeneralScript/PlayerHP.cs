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
        // �������� ������� �������������� �������� �� ���� �������
        float healPercentage = (float)medkitType / 100.0f;

        // ������������ ���������� ��������, ������� ����� �������������
        float healAmount = maxHealth * healPercentage;

        // ��������������� ��������
        currentHealth += healAmount;

        // ���������, ����� ������� �������� �� ��������� ������������
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // ��������� �������� � playerHealth
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