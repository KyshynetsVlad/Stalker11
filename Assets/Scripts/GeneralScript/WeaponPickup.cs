using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon newWeapon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController1 playerController = other.GetComponent<PlayerController1>();
        if (playerController != null)
        {
            Inventory playerInventory = playerController.inventory;
            if (playerInventory != null)
            {
                playerInventory.PickUpWeapon(newWeapon);
                Destroy(gameObject); 
            }
        }
    }
}
