using UnityEngine;

public enum HealthPickupType
{
    Small,
    Large
}

public class HealthPickup : MonoBehaviour
{
    public HealthPickupType pickupType;
    public float healAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddHealthPickup(this);
                Destroy(gameObject);
            }
        }
    }
}
