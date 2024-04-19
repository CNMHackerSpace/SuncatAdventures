using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField] int score;
    [SerializeField] TMP_Text scoreLabel;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        scoreLabel.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void ChangeScore( int change)
    {
        score = score + change;
    }
}
