using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
//using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class TCAGameController : MonoBehaviour
{
    private bool gameGoing;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TMP_Text scoreText;
    public List<Ball> balls;
    private bool[] ballsThrown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.gameObject.SetActive(false);
        restartButton.SetActive(false);

        ballsThrown = new bool[balls.Count];

        gameGoing = true;
    }

    void Update()
    {
        foreach (Ball ball in balls)
        {
            if (ball.Thrown)
            {
                ballsThrown[balls.IndexOf(ball)] = true;
            }
        }

        if (ballsThrown.All(b => b == true) && gameGoing)
        {
            gameGoing = false;
            EndGame();
        }
    }

    private void EndGame()
    {
        gameGoing = false;
        TinCanStatus.TallyScore();

        scoreText.SetText(TinCanStatus.Score.ToString() + "/30");
        scoreText.gameObject.SetActive(true);
        restartButton.SetActive(true);
    }

    public void ResetGame()
    {
        // Clean up
        scoreText.gameObject.SetActive(false);
        restartButton.SetActive(false);

        // self
        ballsThrown = new bool[balls.Count];
        scoreText.gameObject.SetActive(false);

        // cans
        TinCanStatus.ResetCans();

        // Balls
        foreach (Ball ball in balls)
        {
            ball.ResetPos();
        }

        gameGoing = true;
    }
}
