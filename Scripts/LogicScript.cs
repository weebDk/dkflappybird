using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScore;
    public GameObject gameOverScreen;
    public TextMeshProUGUI countdownText; // Added for the countdown display
    public int savedBestScore = 0;

    private AudioSource audioSource; // Reference to the AudioSource
    public AudioClip gameOverSound; // Sound for flapping
    private void Start()
    {
        // Game Start
        //Time.timeScale = 0; // Pause the game
        // Load the best score from PlayerPrefs
        int savedBestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScore.text = savedBestScore.ToString();

        // Start the countdown before the game begins
        StartCoroutine(StartCountdown());

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check for the back button press in mobile mode
        if (IsBackButtonPressed())
        {
            GoToLobby();
        }
    }

    private bool IsBackButtonPressed()
    {
        // Check for back button press on Android or Escape key on other platforms
        if (Application.platform == RuntimePlatform.Android)
        {
            // Handle Android back button or gesture
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                if (activity.Call<bool>("isTaskRoot"))
                {
                    return Input.GetKeyDown(KeyCode.Escape);
                }
            }
        }
        else
        {
            // Handle Escape key for other platforms
            return Input.GetKeyDown(KeyCode.Escape);
        }

        return false;
    }

    private IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true); // Show the countdown text

        for (int i = 3; i > 0; i--)
        {
            //Time.timeScale = 0; // pause the game
            countdownText.text = i.ToString(); // Update the countdown text
            yield return new WaitForSeconds(1); // Wait for 1 second
        }

        countdownText.text = "Go!"; // Display "Go!" at the end of the countdown
        yield return new WaitForSeconds(1); // Wait for 1 second

        countdownText.gameObject.SetActive(false); // Hide the countdown text
        // Add any logic to start the game here
        Time.timeScale = 1; // resume the game
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void restartGame()
    {
        Time.timeScale = 1; // resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        // Check if the current score is higher than the best score
        int savedBestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (playerScore > savedBestScore)
        {
            PlayerPrefs.SetInt("BestScore", playerScore); // Save the new best score
            savedBestScore = playerScore;
            bestScore.text = playerScore.ToString(); // Update the UI
        }
        else
        {
            bestScore.text = savedBestScore.ToString(); // Show the saved best score
        }
        // Play the game over sound
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound); // Play the sound
        }
        // Show the game over screen
        gameOverScreen.SetActive(true);
        Time.timeScale = 0; // Pause the game
        PlayerPrefs.Save(); // Save PlayerPrefs
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene("LobbyScene"); // Replace "LobbyScene" with the actual name of your lobby scene
    }
}
