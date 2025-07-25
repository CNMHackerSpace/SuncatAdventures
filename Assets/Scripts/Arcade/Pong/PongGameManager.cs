using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PongGameManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text playerScoreText;
    public TMP_Text aiScoreText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverMessage;
    public PongAIPaddle aiPaddle;
    public GameObject startPanel;

    [Header("Game Settings")]
    public int winningScore = 3;

    [Header("Ball Spawning")]
    public GameObject ballPrefab;
    public Transform ballSpawnParent;

    [Header("Game UI")]
    public GameObject pongCanvas;


    private int playerScore = 0;
    private int aiScore = 0;
    private bool gameStarted = false;


    public static PongGameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        // Pause the game at the beginning
        Time.timeScale = 0f;

        // Show the start panel if it's assigned
        if (startPanel != null)
            startPanel.SetActive(true);

        // Hide the game over panel (just in case)
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Reset scores
        playerScore = 0;
        aiScore = 0;

        if (playerScoreText != null) playerScoreText.text = "0";
        if (aiScoreText != null) aiScoreText.text = "0";
    }


    public void StartGame()
    {
        if (gameStarted)
            return;

        gameStarted = true;

        Debug.Log("Game Started");
        if (startPanel != null)
            startPanel.SetActive(false);

        Time.timeScale = 1f;
        StartCoroutine(SpawnBallWithDelay(1f)); 

    }



    public void PlayerScores()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();

        if (!CheckForGameOver())
        {
            StartCoroutine(SpawnBallWithDelay(1f));

        }
    }

    public void AIScores()
    {
        aiScore++;
        aiScoreText.text = aiScore.ToString();

        if (!CheckForGameOver())
        {
            StartCoroutine(SpawnBallWithDelay(1f));

        }
    }

    private bool CheckForGameOver()
    {
        if (playerScore >= winningScore)
        {
            EndGame("You Win!");
            return true;
        }
        else if (aiScore >= winningScore)
        {
            EndGame("Game Over");
            return true;
        }

        return false;
    }

    private void EndGame(string message)
    {
        if (gameOverMessage != null)
        {
            gameOverMessage.text = message;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f; // Pause the game
    }

    public void PlayAgain()
    {
        Debug.Log("PlayAgain called!");

        Time.timeScale = 1f; // Unpause the game

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        playerScore = 0;
        aiScore = 0;

        if (playerScoreText != null) playerScoreText.text = "0";
        if (aiScoreText != null) aiScoreText.text = "0";

        StartCoroutine(SpawnBallWithDelay(1f));

    }


    public void QuitGame()
    {
        Debug.Log("QuitGame called!");

        Time.timeScale = 1f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (pongCanvas != null)
            pongCanvas.SetActive(false);
    }



    private void SpawnNewBall()
    {
        if (ballPrefab != null)
        {
            GameObject ball = Instantiate(ballPrefab, ballSpawnParent);
            RectTransform ballRect = ball.GetComponent<RectTransform>();
            ballRect.localPosition = Vector3.zero;

            // Update the AI paddle with the new ball reference
            if (aiPaddle != null)
            {
                aiPaddle.ball = ballRect;
            }
        }
    }

    private IEnumerator SpawnBallWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnNewBall();
    }


}
