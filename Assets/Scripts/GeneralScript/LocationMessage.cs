using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LocationMessage : MonoBehaviour
{
    public GameObject messagePanel;
    public TextMeshProUGUI locationText;
    public Button okButton;

    private void Start()
    {
        // Підписатися на подію натискання кнопки
        okButton.onClick.AddListener(HideLocationMessage);

        // Показати повідомлення при завантаженні сцени
        ShowLocationMessage();
    }

    private void ShowLocationMessage()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SetLocationMessage(currentSceneName);
        messagePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideLocationMessage()
    {
        messagePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void SetLocationMessage(string locationName)
    {
        locationText.text = $"Ви знаходитеся на локації {locationName}";
    }
}