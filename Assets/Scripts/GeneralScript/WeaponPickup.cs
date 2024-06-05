using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController1 playerController = other.GetComponent<PlayerController1>();
            if (playerController != null)
            {
                playerController.PickUpWeapon(weapon);
                Destroy(gameObject); 
            }
        }
    }
}