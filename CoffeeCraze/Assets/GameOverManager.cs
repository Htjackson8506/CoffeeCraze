using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    private void Start()
    {
        // Make sure game over screen is hidden at start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        // Show game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        
        // Optional: pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Reset time scale
        Time.timeScale = 1f;

        // Set a flag in PlayerPrefs to indicate a restart
        PlayerPrefs.SetInt("GameRestarted", 1);
        PlayerPrefs.Save();
        
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}