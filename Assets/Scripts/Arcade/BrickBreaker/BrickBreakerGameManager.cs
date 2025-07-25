using UnityEngine;
using UnityEngine.UI;

public class BrickBreakerGameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject ballPrefab;
    public RectTransform ballSpawnPoint;
    public RectTransform ballParent;

    public GameObject brickPrefab;
    public RectTransform spawnArea;
    public BrickManager brickManager;

    public RectTransform paddle;  
    public Vector2 paddleStartPosition = new Vector2(0f, 40f);

    public GameObject brickBreakerCanvas; 

    public void PlayAgain()
    {
        //Clear all bricks
        foreach (Brick brick in FindObjectsByType<Brick>(FindObjectsSortMode.None))
        {
            Destroy(brick.gameObject);
        }

        //Destroy any existing balls
        foreach (BallController ball in FindObjectsByType<BallController>(FindObjectsSortMode.None))
        {
            Destroy(ball.gameObject);
        }

        //Reset paddle position
        if (paddle != null)
        {
            paddle.localPosition = paddleStartPosition;
        }

        //Regenerate bricks
        brickManager.GenerateBricks();

        //Spawn new ball
        GameObject newBall = Instantiate(ballPrefab, ballParent);
        RectTransform ballRect = newBall.GetComponent<RectTransform>();
        ballRect.localPosition = ballSpawnPoint.localPosition;

        // Assign the game over panel to the new ball
        BallController ballController = newBall.GetComponent<BallController>();
        if (ballController != null)
        {
            ballController.gameOverPanel = gameOverPanel;
        }


        //Hide game over panel
        gameOverPanel.SetActive(false);
    }

    public void QuitGame()
    {
        if (brickBreakerCanvas != null)
        {
            brickBreakerCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("brickBreakerCanvas not assigned in GameManager.");
        }
    }

    public static int GetBrickCount()
    {
        return FindObjectsByType<Brick>(FindObjectsSortMode.None).Length;
    }

}
