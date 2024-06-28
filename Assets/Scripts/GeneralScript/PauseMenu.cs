using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public VectorValue playerPosition;
    public PlayerHealth health;
    public WeaponData weaponData;

    public GameObject pauseMenuUI;
    private bool isPaused = false;


    public void Pauses()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Restart()
    {
        ResetScriptableObjects();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Location1");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
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