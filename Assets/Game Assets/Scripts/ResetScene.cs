using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class ResetScene : MonoBehaviour
{
    // Method to be called when button is clicked
    public void RestartGame()
    {
        // Get the current scene index/name and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        // Optional: If you have paused time anywhere in your game
        Time.timeScale = 1f;
    }
}