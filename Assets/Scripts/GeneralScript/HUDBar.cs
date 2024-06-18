using UnityEngine;
using UnityEngine.UI;

public class HUDBar : MonoBehaviour
{
    public Image healthBar;
    public Image staminaBar;
    public Image avatarImage;

    public Sprite[] healthSprites;
    public Sprite[] staminaSprites;
    public Sprite[] avatarSprites;

    private float currentHealth;
    private float maxHealth;
    private int currentStamina;
    private int maxStamina = 100;
    private Health playerHealth;

    private void Start()
    {
        currentStamina = 100;

        // Ініціалізація компонентів Health
        playerHealth = GetComponent<Health>();
        if (playerHealth != null)
        {
            currentHealth = playerHealth.currentHealth;
            maxHealth = playerHealth.maxHealth;
        }
        else
        {
            Debug.LogError("Player Health component is not assigned.");
        }
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            currentHealth = playerHealth.currentHealth;
            maxHealth = playerHealth.maxHealth;

            UpdateHealthBar();
            UpdateStaminaBar();
            UpdateAvatarImage();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthSprites.Length > 0)
        {
            int healthIndex = Mathf.FloorToInt(currentHealth / (maxHealth / healthSprites.Length));
            healthBar.sprite = healthSprites[Mathf.Clamp(healthIndex, 0, healthSprites.Length - 1)];
        }
    }

    private void UpdateStaminaBar()
    {
        if (staminaSprites.Length > 0)
        {
            int staminaIndex = Mathf.FloorToInt(currentStamina / (maxStamina / staminaSprites.Length));
            staminaBar.sprite = staminaSprites[Mathf.Clamp(staminaIndex, 0, staminaSprites.Length - 1)];
        }
    }

    private void UpdateAvatarImage()
    {
        if (avatarSprites.Length > 0)
        {
            int avatarIndex = Mathf.FloorToInt(currentHealth / (maxHealth / avatarSprites.Length));
            avatarImage.sprite = avatarSprites[Mathf.Clamp(avatarIndex, 0, avatarSprites.Length - 1)];
        }
    }
}