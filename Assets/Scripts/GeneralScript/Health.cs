using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public abstract void TakeDamage(float damage);
    public abstract void TakeHeal(Medkit.MedkitType medkitType);
    
    protected void Die()
    {
        Destroy(gameObject);
    }
}