using TMPro;
using UnityEngine;

public class HealthInventory : MonoBehaviour
{
    [SerializeField] private int MedkitCount = 0;

    [SerializeField] private TextMeshProUGUI MedkitText;

    [SerializeField] private PlayerHP playerHealth; // Скрипт здоровья игрока

    private void Start()
    {
        LoadInventory();
        UpdateUI();
    }

    public void AddMedkit(int amount)
    {
        MedkitCount += amount;
        SaveInventory();
        UpdateUI();
    }

    private void UpdateUI()
    {
        MedkitText.text = MedkitCount.ToString();
    }

    private void SaveInventory()
    {
        PlayerPrefs.SetInt("MedkitCount", MedkitCount);
    }

    private void LoadInventory()
    {
        MedkitCount = PlayerPrefs.GetInt("MedkitCount", 0);
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
            UseMedkit();
        }

    }

    private void UseMedkit()
    {
        if (MedkitCount > 0)
        {
            MedkitCount--;
            playerHealth.TakeHeal(Medkit.HEALPOINT); // Лечение игрока
        }
        SaveInventory();
        UpdateUI();
    } 

}
