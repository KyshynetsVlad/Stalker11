using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "ScriptableObjects/PlayerHealth", order = 2)]
public class PlayerHealth : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;

    public void Reset()
    {
        maxHealth = 100;
        currentHealth = 100;
    }
}