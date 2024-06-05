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
    private int maxStamina =100;
    private int currentAvatarIndex;
    private Health _currentHealth;
    private Health _maxHealth;

    private void Start()
    {
        currentStamina = 100;
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
        UpdateAvatarImage();
        _currentHealth = GetComponent<Health>();
        _maxHealth = GetComponent<Health>();
        currentHealth = _currentHealth.currentHealth;
        maxHealth = _maxHealth.maxHealth;
    }

    private void UpdateHealthBar()
    {
        int healthIndex = Mathf.FloorToInt(currentHealth / (maxHealth / healthSprites.Length));
        healthBar.sprite = healthSprites[Mathf.Clamp(healthIndex, 0, healthSprites.Length - 1)];
    }

    private void UpdateStaminaBar()
    {
        int staminaIndex = Mathf.FloorToInt(currentStamina / (maxStamina / staminaSprites.Length));
        staminaBar.sprite = staminaSprites[Mathf.Clamp(staminaIndex, 0, staminaSprites.Length - 1)];
    }

    private void UpdateAvatarImage()
    {
        int avatarIndex = Mathf.FloorToInt(currentHealth / (maxHealth / avatarSprites.Length));
        avatarImage.sprite = avatarSprites[Mathf.Clamp(avatarIndex, 0, avatarSprites.Length - 1)];
    }
}