using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private HealthInventory healthInventory;
    public const int HEALPOINT = 3; 

    public void SetHealthInventory(HealthInventory HealthInventory)
    {
        healthInventory = HealthInventory;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (healthInventory != null)
            {
                healthInventory.AddMedkit(1);
                Destroy(gameObject);
            }
        }
    }
}
