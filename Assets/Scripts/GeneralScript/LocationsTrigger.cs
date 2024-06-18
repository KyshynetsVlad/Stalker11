using UnityEngine.SceneManagement;
using UnityEngine;

public class LocationsTrigger : MonoBehaviour
{
    public GameObject textLocation;  // Текст для відображення повідомлення про перехід
    public int nextLocation;  // Номер наступної сцени
    public VectorValue valuePlayer;  // ScriptableObject для збереження позиції гравця
    public PlayerHealth playerHealth;
    public WeaponData weaponData;
    public Vector2 spawnPosition;  // Позиція для спауна гравця в новій сцені
    private bool onTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textLocation.SetActive(true);
            onTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textLocation.SetActive(false);
            onTrigger = false;
        }
    }

    private void Update()
    {
        if (onTrigger && Input.GetKeyDown(KeyCode.C))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Збереження позиції гравця перед переходом
                if (valuePlayer != null)
                {
                    valuePlayer.initialValue = spawnPosition;
                }
                else
                {
                    Debug.LogError("valuePlayer is not assigned in the inspector.");
                }

                // Збереження здоров'я гравця
                Health health = player.GetComponent<Health>();
                if (health != null && playerHealth != null)
                {
                    playerHealth.currentHealth = health.currentHealth;
                    playerHealth.maxHealth = health.maxHealth;
                }
                else
                {
                    Debug.LogError("Health component or playerHealth is not assigned or found.");
                }

                // Збереження даних про зброю
                PlayerController1 playerController = player.GetComponent<PlayerController1>();
                if (playerController != null)
                {
                    Inventory inventory = playerController.inventory;
                    if (inventory != null && weaponData != null)
                    {
                        weaponData.weapons = inventory.weapons;
                        weaponData.currentWeaponIndex = inventory.currentWeaponIndex;
                    }
                    else
                    {
                        Debug.LogError("Inventory or weaponData is not assigned or found.");
                    }
                }
                else
                {
                    Debug.LogError("PlayerController1 component is not found.");
                }

                // Завантаження наступної сцени
                SceneManager.LoadScene(nextLocation);
            }
            else
            {
                Debug.LogError("Player not found.");
            }
        }
    }
}