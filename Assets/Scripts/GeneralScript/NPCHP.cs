using UnityEngine;

public class NPCHP : Health
{

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public override void TakeHeal(int healPoints)
    {
        throw new System.NotImplementedException();
    }
}