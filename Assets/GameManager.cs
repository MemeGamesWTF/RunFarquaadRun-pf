using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject startPanel; // Start panel
    public GameObject gameOverPanel; // Game Over panel
    public Text scoreText; // Score text during the game
    public Text finalScoreText; // Final score text on Game Over

    [Header("Gameplay Elements")]
    public GameObject player; // Reference to the player object
    public GameObject spawner; // Reference to the spawner

    private float survivalTime = 0f; // Time the player survives
    private bool isGameActive = false; // Tracks if the game is active
[DllImport("__Internal")]
  private static extern void SendScore(int score, int game);
    private void Start()
    {
        // Show the start panel and hide the game panels
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        scoreText.gameObject.SetActive(false);

        // Disable player and spawner initially
        player.SetActive(false);
        spawner.SetActive(false);
    }

    private void Update()
    {
        // Update the score only when the game is active
        if (isGameActive)
        {
            survivalTime += Time.deltaTime;
            scoreText.text = "Score: " + Mathf.FloorToInt(survivalTime).ToString();
        }
    }

    public void StartGame()
    {
        // Hide the start panel and enable game elements
        startPanel.SetActive(false);
        player.SetActive(true);
        spawner.SetActive(true);
        scoreText.gameObject.SetActive(true);

        // Reset game state
        survivalTime = 0f;
        isGameActive = true;
    }

    public void GameOver()
    {
        // End the game
        isGameActive = false;

        // Show the Game Over panel and hide score UI
        gameOverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);

        // Display the final score
        finalScoreText.text = "Score: " + Mathf.FloorToInt(survivalTime).ToString();

        // Disable player and spawner
        player.SetActive(false);
        spawner.SetActive(false);

        SendScore(Mathf.FloorToInt(survivalTime), 59);
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
