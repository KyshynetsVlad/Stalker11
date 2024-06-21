using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthInventory : MonoBehaviour
{
    [SerializeField] private int smallMedkitCount = 0;
    [SerializeField] private int largeMedkitCount = 0;

    [SerializeField] private TextMeshProUGUI smallMedkitText;
    [SerializeField] private TextMeshProUGUI largeMedkitText;

    [SerializeField] private PlayerHP playerHealth; // Скрипт здоровья игрока

    private void Start()
    {
        //playerHealth = GetComponent<PlayerHP>();
        LoadInventory();
        UpdateUI();
    }

    public void AddSmallMedkit(int amount)
    {
        smallMedkitCount += amount;
        SaveInventory();
        UpdateUI();
    }

    public void AddLargeMedkit(int amount)
    {
        largeMedkitCount += amount;
        SaveInventory();
        UpdateUI();
    }

    private void UpdateUI()
    {
        smallMedkitText.text = smallMedkitCount.ToString();
        largeMedkitText.text = largeMedkitCount.ToString();
    }

    private void SaveInventory()
    {
        PlayerPrefs.SetInt("SmallMedkitCount", smallMedkitCount);
        PlayerPrefs.SetInt("LargeMedkitCount", largeMedkitCount);
    }

    private void LoadInventory()
    {
        smallMedkitCount = PlayerPrefs.GetInt("SmallMedkitCount", 0);
        largeMedkitCount = PlayerPrefs.GetInt("LargeMedkitCount", 0);
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }

    private void OnDisable()
    {
        SaveInventory();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSmallMedkit();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            UseLargeMedkit();
        }

    }

    private void UseLargeMedkit()
    {
        if (largeMedkitCount > 0)
        {
            largeMedkitCount--;
            playerHealth.TakeHeal(Medkit.MedkitType.Large); // Лечение игрока
        }
        SaveInventory();
        UpdateUI();
    }

    private void UseSmallMedkit()
    {
        if (smallMedkitCount > 0)
        {
            smallMedkitCount--;
            playerHealth.TakeHeal(Medkit.MedkitType.Small); // Лечение игрока
        }
        SaveInventory();
        UpdateUI();
    } 

}
