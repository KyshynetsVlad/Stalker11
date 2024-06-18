using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public VectorValue playerPosition;
    public PlayerHealth health;
    public WeaponData weaponData;
    public void NewGame()
    {
        ResetScriptableObjects();
        SceneManager.LoadScene("Location1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ResetScriptableObjects()
    {
        if (playerPosition != null)
        {
            playerPosition.Reset();
        }
        else
        {
            Debug.LogError("Player position is not assigned in the inspector.");
        }
        if (health != null)
        {
            health.Reset();
        }
        else
        {
            Debug.LogError("Health is not assigned in the inspector.");
        }
        if (weaponData != null)
        {
            weaponData.Reset();
        }
        else
        {
            Debug.LogError("Weapon Data is not assigned in the inspector.");
        }

    }
}